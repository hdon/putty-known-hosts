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
    KnownHost[] knownHosts;

    /* TODO static? */
    Regex regKeyNameRegex;
    Regex hexStringRegex;

    public Form1()
    {
      InitializeComponent();
      regKeyNameRegex = new Regex(@"^([^@]+)@(\d+):(.*)$");
      hexStringRegex = new Regex(@"^0x([0-9a-f]+)$");
      this.rescan();
      // courtesy https://msdn.microsoft.com/en-us/library/ms996467.aspx
      // dunno why i can't hook it up using designer...
      // this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_columnClick);
    }

    private void rescanRegistryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.rescan();
    }
    
    private void rescan()
    {
      this.knownHosts = null;
      System.Console.Write("Hello, World!\n");
      RegistryKey rkeySshHostKeys = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\SshHostKeys");
      if (rkeySshHostKeys == null)
      {
        MessageBox.Show("Could not locate registry key HKEY_CURRENT_USER\\Software\\SimonTatham\\PuTTY\\SshHostKeys. Are you sure you have installed PuTTY?");
        return;
      }
      string[] subkeys = rkeySshHostKeys.GetValueNames();
      if (subkeys.Length == 0)
      {
        System.Console.Write("No SshHostKeys key found in registry\n");
        return;
      }
      System.Console.Write("found {0} subkeys\n", subkeys.Length);
      var malformedKeys = new String[0];
      foreach (var subkey in subkeys)
      {
        var parts = new String[3];
        try
        {
          var match = this.regKeyNameRegex.Match(subkey);
          if (!match.Success)
          {
            malformedKeys[malformedKeys.Length] = subkey;
            continue;
          }
          var keyType = match.Groups[1].Value;
          var hostPort = match.Groups[2].Value;
          var hostName = match.Groups[3].Value;
          parts[0] = hostName + ":" + hostPort;
          parts[1] = keyType;

          var pubKey = (String)rkeySshHostKeys.GetValue(subkey);
          if (pubKey == null)
          {
            // XXX
            System.Console.Write("could not retrieve public key");
          }
          else
          {
            using (MD5 md5Hash = MD5.Create())
            {
              var decodedParts = DecodePuttyRegPubKey(pubKey);
              var exponent = decodedParts.Item1;
              var modulus = decodedParts.Item2;
              var pubKeyFormatted = formatPublicKeyParts("ssh-rsa", exponent, modulus);
              string hash = GetMd5Hash(md5Hash, pubKeyFormatted);
              parts[2] = hash;
            }
          }
        }
        catch (Exception e)
        {
          parts[2] = e.Message;
        }

        /* TODO this feels silly */
        if (parts[0] == null)
          parts[0] = "parse error";
        if (parts[1] == null)
          parts[1] = "parse error";
        if (parts[2] == null)
          parts[2] = "parse error";

        var li = new ListViewItem(parts[0]);
        li.SubItems.Add(parts[1]);
        li.SubItems.Add(parts[2]);
        listView1.Items.Add(li);
      }
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

    byte[] hexToBytes(string hex)
    {
      var match = this.hexStringRegex.Match(hex);
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

    byte[] formatPublicKeyParts(string keyType, byte[] exponent, byte[] modulus)
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

    // https://msdn.microsoft.com/en-us/library/ms996467.aspx
    private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
    {
      this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
      this.listView1.Sort();
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
    String host;
    String key;
    public KnownHost(String host, String key)
	  {
      this.host = host;
      this.key = key;
	  }
  }
}