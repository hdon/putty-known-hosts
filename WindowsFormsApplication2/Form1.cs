using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApplication2
{
  public partial class Form1 : Form
  {
    List<KnownHost> knownHosts;

    /* TODO static? */
    readonly Regex regKeyNameRegex;
    readonly Regex hexStringRegex;

    const string REG_KEY = "Software\\SimonTatham\\PuTTY\\SshHostKeys";

    public Form1()
    {
      InitializeComponent();
      regKeyNameRegex = new Regex(@"^([^@]+)@(\d+):(.*)$");
      hexStringRegex = new Regex(@"^0x([0-9a-f]+)$");
      knownHosts = new List<KnownHost>();
      this.rescan();
    }

    private void rescanRegistryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.rescan();
    }
    
    private void rescan()
    {
      knownHosts.Clear();
      listView1.Items.Clear();
      System.Console.Write("Hello, World!\n");
      if (KnownHost.rkeySshHostKeys == null)
      {
        MessageBox.Show("Could not locate registry key HKEY_CURRENT_USER\\Software\\SimonTatham\\PuTTY\\SshHostKeys. Are you sure you have installed PuTTY?");
        return;
      }
      string[] subkeys = KnownHost.rkeySshHostKeys.GetValueNames();
      if (subkeys.Length == 0)
      {
        System.Console.Write("No SshHostKeys key found in registry\n");
        return;
      }
      System.Console.Write("found {0} subkeys\n", subkeys.Length);
      var malformedKeys = new String[0];
      foreach (var subkey in subkeys)
      {
        var knownHost = KnownHost.fromReg(subkey);
        knownHosts.Add(knownHost);
        listView1.Items.Add(knownHost.listViewItem);
      }
    }


    // https://msdn.microsoft.com/en-us/library/ms996467.aspx
    private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
    {
      this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
      this.listView1.Sort();
    }

    private void listView1_KeyDown(object sender, KeyEventArgs ev)
    {
      if (Keys.Delete == ev.KeyCode)
      {
        try
        {
          deletedSelectedItems();
        }
        catch (Exception err)
        {
          MessageBox.Show(err.Message);
          rescan();
        }
      }
    }

    private void deletedSelectedItems()
    {
      var items = listView1.SelectedItems;
      foreach (ListViewItem item in items)
      {
        var knownHost = (KnownHost) item.Tag;
        knownHost.deleteFromRegistry();
        listView1.Items.Remove(knownHost.listViewItem);
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void exportToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Clipboard.SetDataObject((object)exportToString());
    }

    private string exportToString()
    {
      var sb = new StringBuilder();
      sb.Append("Windows Registry Editor Version 5.00\r\n\r\n[HKEY_CURRENT_USER\\Software\\SimonTatham\\PuTTY\\SshHostKeys]\r\n");
      foreach (var knownHost in knownHosts)
      {
        sb.AppendFormat("\"{0}\"=\"{1}\"\r\n"
        , knownHost.windowsRegistryName
        , knownHost.windowsRegistryValue
        );
      }
      sb.Append("\r\n");
      return sb.ToString();
    }

    private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      
    }
  }

  // https://msdn.microsoft.com/en-us/library/ms996467.aspx
  class ListViewItemComparer : IComparer
  {
    private int col;
    public ListViewItemComparer()
    {
      col = 0;
    }
    public ListViewItemComparer(int column)
    {
      col = column;
    }
    public int Compare(object x, object y)
    {
      int returnVal = -1;
      returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
      ((ListViewItem)y).SubItems[col].Text);
      return returnVal;
    }
  }

  class KnownHost
  {
    string keyType;
    string host;
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
          _listViewItem = new ListViewItem(this.host + ":" + this.port);
          _listViewItem.SubItems.Add(this.keyType);
          _listViewItem.SubItems.Add(this.keyHash);
          _listViewItem.Tag = this;
        }
        return _listViewItem;
      }
    }

    private string _keyHash;
    public string keyHash
    {
      private set { _keyHash = value; }
      get
      {
        if (_keyHash == null && this.windowsRegistryValue != null)
        {
          using (MD5 md5Hash = MD5.Create())
          {
            var decodedParts = DecodePuttyRegPubKey(windowsRegistryValue);
            var exponent = decodedParts.Item1;
            var modulus = decodedParts.Item2;
            var pubKeyFormatted = formatPublicKeyParts("ssh-rsa", exponent, modulus);
            _keyHash = GetMd5Hash(md5Hash, pubKeyFormatted);
          }
        }
        return _keyHash;
      }
    }

    private KnownHost() {}
    public static KnownHost fromReg(String name)
    {
      var rval = new KnownHost();
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
    public static KnownHost fromParts(String host, int port, String keyType, string keyData)
    {
      var rval = new KnownHost();
      rval.host = host;
      rval.port = port;
      rval.keyType = keyType;
      rval.windowsRegistryValue = keyData;
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
}