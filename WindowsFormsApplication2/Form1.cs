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

namespace WindowsFormsApplication2
{
  public partial class Form1 : Form
  {
    Regex regKeyNameRegex;
    KnownHost[] knownHosts;

    public Form1()
    {
      InitializeComponent();
      regKeyNameRegex = new Regex(@"^([^@]+)@(\d+):(.*)$");
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
            parts[2] = pubKey;
          }
        }
        catch (Exception)
        {

          throw;
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