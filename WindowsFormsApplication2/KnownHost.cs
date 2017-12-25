using System;
using System.Linq;
using BinaryWriter = System.IO.BinaryWriter;
using Encoding = System.Text.Encoding;
using ListViewItem = System.Windows.Forms.ListViewItem;
using MD5 = System.Security.Cryptography.MD5;
using MemoryStream = System.IO.MemoryStream;
using Regex = System.Text.RegularExpressions.Regex;
using Registry = Microsoft.Win32.Registry;
using RegistryKey = Microsoft.Win32.RegistryKey;
using StringBuilder = System.Text.StringBuilder;
using HMACSHA1 = System.Security.Cryptography.HMACSHA1;

class KnownHost
{
  string keyType;
  string host;
  byte[] hostHashSalt;
  byte[] hostHash;
  string opensshLine;
  string opensshBase64KeyStr;

  int port;
  /* Hold onto our source data; useful if we fail to grok it */
  public string windowsRegistryName;
  public string windowsRegistryValue;
  bool parseError;
  /* Maintain a writable handle to the registry */
  const string REG_KEY = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
  public static RegistryKey rkeySshHostKeys;

  static readonly Regex regKeyNameRegex;
  static readonly Regex hexStringRegex;
  static KnownHost()
  {
    regKeyNameRegex = new Regex(@"^([^@]+)@(\d+):(.*)$");
    hexStringRegex = new Regex(@"^0x([0-9a-f]+)$");
    rkeySshHostKeys = Registry.CurrentUser.OpenSubKey(REG_KEY, true);
  }

  /* TODO Is this pattern a good idea? Lazily-evaluated properties that instantiate and assign the first time you access them? */
  private ListViewItem _listViewItem;
  public ListViewItem listViewItem
  {
    private set { _listViewItem = value; }
    get
    {
      if (_listViewItem == null)
      {
        if (this.host != null)
          _listViewItem = new ListViewItem(this.host + ":" + this.port);
        else
          _listViewItem = new ListViewItem("<unknown host>");
        _listViewItem.SubItems.Add(this.keyType);
        _listViewItem.SubItems.Add(this.keyHash);
        _listViewItem.Tag = this;
      }
      return _listViewItem;
    }
  }

  /* Where did this known host come from? e.g. registry, file */
  public const string FROM_REGISTRY = "registry";
  private string _source;
  public string source
  {
    private set { _source = value; }
    get { return _source; }
  }

  private string _keyHash;
  public string keyHash
  {
    private set { _keyHash = value; }
    get
    {
      if (_keyHash == null)
      {
        if (this.windowsRegistryValue != null)
        {
          using (MD5 md5Hash = MD5.Create())
          {
            var decodedParts = DecodePuttyRegPubKey(windowsRegistryValue);
            var exponent = decodedParts.Item1;
            var modulus = decodedParts.Item2;
            // NOTE pubKeyFormatted is analogous to opensshBase64KeyStr
            var pubKeyFormatted = formatPublicKeyParts("ssh-rsa", exponent, modulus);
            _keyHash = GetMd5Hash(md5Hash, pubKeyFormatted);
          }
        }
        else if (opensshBase64KeyStr != null)
        {
          using (MD5 md5Hash = MD5.Create())
          {
            var pubKeyFormatted = Convert.FromBase64String(opensshBase64KeyStr);
            _keyHash = GetMd5Hash(md5Hash, pubKeyFormatted);
          }
        }
      }
      return _keyHash;
    }
  }

  public bool trySolvingHashedHost(string host)
  {
    if (this.host != null)
      return this.host.Equals(host);
    using (var hmac = new HMACSHA1(hostHashSalt))
    {
      if (hmac.ComputeHash(Encoding.ASCII.GetBytes(host)).SequenceEqual(hostHash))
      {
        this.host = host;
        this.listViewItem.SubItems[0].Text = host;
        return true;
      }
    }
    return false;
  }

  private KnownHost() { }
  public static KnownHost fromReg(String name)
  {
    var rval = new KnownHost();
    rval.source = FROM_REGISTRY;
    var match = regKeyNameRegex.Match(name);
    if (!match.Success)
    {
      rval.parseError = true;
      return rval;
    }
    rval.keyType = match.Groups[1].Value;
    rval.port = int.Parse(match.Groups[2].Value);
    rval.host = match.Groups[3].Value;

    rval.windowsRegistryName = name;
    rval.windowsRegistryValue = (String)rkeySshHostKeys.GetValue(name);
    return rval;
  }
  /* TODO remove? */
  public static KnownHost fromParts(String host, int port, String keyType, string keyData)
  {
    var rval = new KnownHost();
    rval.host = host;
    rval.port = port;
    rval.keyType = keyType;
    rval.windowsRegistryValue = keyData;
    return rval;
  }
  public static KnownHost fromOpenSshKnownHosts(string line)
  {
    /* for details on openssh known_hosts format, see:
     * http://man.openbsd.org/sshd.8#SSH_KNOWN_HOSTS_FILE_FORMAT
     */
    var rval = new KnownHost();
    rval.opensshLine = line;
    var pieces = line.Split(' ').Where(s => s.Length > 0).ToArray();
    string marker = null, hostnames, comment = null;
    switch (pieces.Length)
    {
      case 3:
        hostnames = pieces[0];
        rval.keyType = pieces[1];
        rval.opensshBase64KeyStr = pieces[2];
        break;
      case 4:
        if (pieces[0][0] == '@')
        {
          marker = pieces[0];
          hostnames = pieces[1];
          rval.keyType = pieces[2];
          rval.opensshBase64KeyStr = pieces[3];
        }
        else
        {
          hostnames = pieces[0];
          rval.keyType = pieces[1];
          rval.opensshBase64KeyStr = pieces[2];
          comment = pieces[3];
        }
        break;
      case 5:
        marker = pieces[0];
        hostnames = pieces[1];
        rval.keyType = pieces[2];
        rval.opensshBase64KeyStr = pieces[3];
        comment = pieces[4];
        break;
      default:
        throw new Exception("invalid openssh-format known_hosts line: wrong number of space-separated fields");
    }
    if (marker != null && marker[0] != '@')
      throw new Exception("invalid openssh-format known_hosts line: markers must begin with '@'");
    
    /* "hostnames" is probably hashed */
    if (hostnames[0] == '|')
    {
      var hostHashPieces = hostnames.Split('|');
      if (hostHashPieces.Length != 4)
        throw new Exception(@"found ""|"" but not the right number");
      rval.hostHashSalt = Convert.FromBase64String(hostHashPieces[2]);
      rval.hostHash = Convert.FromBase64String(hostHashPieces[3]);
    }
    else
      rval.host = hostnames;
    
    return rval;
  }

  public void deleteFromRegistry()
  {
    rkeySshHostKeys.DeleteValue(windowsRegistryName);
  }

  Tuple<byte[], byte[]> DecodePuttyRegPubKey(string pubkey)
  {
    var regKeyParts = pubkey.Split(',');
    if (regKeyParts.Length != 2)
      throw new Exception("expected putty reg pubkey to contain one comma");
    return Tuple.Create(
      hexToBytes(regKeyParts[0])
    , hexToBytes(regKeyParts[1])
    );
  }

  static byte[] hexToBytes(string hex)
  {
    var match = hexStringRegex.Match(hex);
    if (!match.Success)
      throw new Exception("expected hex string to begin with 0x");
    hex = match.Groups[1].Value;
    var decodedLen = hex.Length / 2 + hex.Length % 2;
    var decoded = new byte[decodedLen];
    int i = 0;
    if (hex.Length % 2 == 1)
    {
      decoded[i++] = byte.Parse(hex.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
    }
    while (i < decodedLen)
    {
      int pos = i * 2 - hex.Length % 2;
      var ss = hex.Substring(pos, 2);
      decoded[i++] = byte.Parse(ss, System.Globalization.NumberStyles.HexNumber);
    }
    return decoded;
  }

  static byte[] formatPublicKeyParts(string keyType, byte[] exponent, byte[] modulus)
  {
    using (MemoryStream memStream = new MemoryStream())
    {
      var bw = new BinaryWriter(memStream);

      writeInt(bw, keyType.Length);
      bw.Write(Encoding.ASCII.GetBytes(keyType));
      writeInt(bw, exponent.Length);
      bw.Write(exponent);
      // XXX TODO is this correct? i've observed this byte from public key files recorded by openssh's ssh-keygen. no idea what it's for.
      writeInt(bw, modulus.Length + 1);
      bw.Write((byte)0);
      bw.Write(modulus);
      return memStream.ToArray();
    }
  }

  static void writeInt(BinaryWriter w, int i)
  {
    byte[] tmp = BitConverter.GetBytes((int)i);
    Array.Reverse(tmp);
    w.Write(tmp);
  }

  // thanks https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
  static string GetMd5Hash(MD5 md5Hash, string input)
  {
    return GetMd5Hash(md5Hash, Encoding.ASCII.GetBytes(input));
  }
  static string GetMd5Hash(MD5 md5Hash, byte[] input)
  {
    byte[] data = md5Hash.ComputeHash(input);
    StringBuilder sBuilder = new StringBuilder();
    for (int i = 0; i < data.Length; i++)
    {
      if (i != 0)
        sBuilder.Append(':');
      sBuilder.Append(data[i].ToString("x2"));
    }
    return sBuilder.ToString();
  }
}