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
    readonly Regex hostNameRegex;

    const string REG_KEY = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
    
    public Form1()
    {
      InitializeComponent();
      regKeyNameRegex = new Regex(@"^([^@]+)@(\d+):(.*)$");
      hexStringRegex = new Regex(@"^0x([0-9a-f]+)$");
      // recognize hostnames and IPv4 dotted-decimal notation
      hostNameRegex = new Regex(@"\b([A-Za-z][A-Za-z0-9.-]+)|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})\b");
      knownHosts = new List<KnownHost>();
      //this.rescan();
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

      updateKnownHostStats();
    }

    private void updateKnownHostStats()
    {
      int numTotal = 0, numUnknownHosts = 0;
      var sb = new StringBuilder();
      foreach (var knownHost in knownHosts)
      {
        numTotal++;
        if (knownHost.hostIsUnknown())
          numUnknownHosts++;
      }
      sb.AppendFormat("Known Hosts: {0} Unknown Hosts: {1}", numTotal, numUnknownHosts);
      toolStripStatusLabel2.Text = sb.ToString();
    }

    private void readOpenSshKnownHostsFile(string filename)
    {
      var file = new StreamReader(filename);
      string line;
      while ((line = file.ReadLine()) != null)
      {
        var knownHost = KnownHost.fromOpenSshKnownHosts(line);
        knownHosts.Add(knownHost);
        listView1.Items.Add(knownHost.listViewItem);
      }
      updateKnownHostStats();
    }

    // https://msdn.microsoft.com/en-us/library/ms996467.aspx
    private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
    {
      this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
      this.listView1.Sort();
    }

    /* TODO what if this item isn't from the registry? */
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
      updateKnownHostStats();
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

    private void readOpenSSHKnownhostsFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
        readOpenSshKnownHostsFile(openFileDialog1.FileName);
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      // TODO search; solve hashes

      /* Try to solve hashes */
      foreach (var knownHost in knownHosts)
      {
        if (knownHost.trySolvingHashedHost(textBox1.Text))
        {
          knownHost.listViewItem.BackColor = System.Drawing.Color.Yellow;
          var s = new StringBuilder();
          s.Append("Found ");
          s.Append(textBox1.Text);
          toolStripStatusLabel1.Text = s.ToString();
        }
        else
        {
          knownHost.listViewItem.BackColor = System.Drawing.SystemColors.Window;
        }
      }
      updateKnownHostStats();
    }

    private void identifyHostsFromFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (openFileDialog2.ShowDialog() == DialogResult.OK)
        identifyHostsFromFile(openFileDialog2.FileName);
    }

    /* Read plausible hostnames from a file and perform dictionary attack against loaded known_hosts records */
    private void identifyHostsFromFile(string filename)
    {
      var file = new StreamReader(filename);
      string line;
      var attemptedHosts = new HashSet<string>();
      while ((line = file.ReadLine()) != null)
      {
        foreach (var match in hostNameRegex.Matches(line))
        {
          var host = match.ToString();
          if (!attemptedHosts.Contains(host))
          {
            foreach (var knownHost in knownHosts)
              knownHost.trySolvingHashedHost(host);
            attemptedHosts.Add(host);
          }
        }
      }
      updateKnownHostStats();
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