using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
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
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        File.WriteAllText(saveFileDialog1.FileName, exportToString());
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
}