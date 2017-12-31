namespace WindowsFormsApplication2
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rescanRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.readOpenSSHKnownhostsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.identifyHostsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.iP4RangeAttackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.onlineDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.listView1 = new System.Windows.Forms.ListView();
      this.hostColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.hostKeyTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.hostKeyFingerprintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label1 = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
      this.importIntoRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(830, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rescanRegistryToolStripMenuItem,
            this.readOpenSSHKnownhostsFileToolStripMenuItem,
            this.identifyHostsFromFileToolStripMenuItem,
            this.exportToFileToolStripMenuItem,
            this.exportToClipboardToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // rescanRegistryToolStripMenuItem
      // 
      this.rescanRegistryToolStripMenuItem.Name = "rescanRegistryToolStripMenuItem";
      this.rescanRegistryToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.rescanRegistryToolStripMenuItem.Text = "&Re-scan Registry";
      this.rescanRegistryToolStripMenuItem.Click += new System.EventHandler(this.rescanRegistryToolStripMenuItem_Click);
      // 
      // readOpenSSHKnownhostsFileToolStripMenuItem
      // 
      this.readOpenSSHKnownhostsFileToolStripMenuItem.Name = "readOpenSSHKnownhostsFileToolStripMenuItem";
      this.readOpenSSHKnownhostsFileToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.readOpenSSHKnownhostsFileToolStripMenuItem.Text = "Read &OpenSSH known_hosts File";
      this.readOpenSSHKnownhostsFileToolStripMenuItem.Click += new System.EventHandler(this.readOpenSSHKnownhostsFileToolStripMenuItem_Click);
      // 
      // identifyHostsFromFileToolStripMenuItem
      // 
      this.identifyHostsFromFileToolStripMenuItem.Name = "identifyHostsFromFileToolStripMenuItem";
      this.identifyHostsFromFileToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.identifyHostsFromFileToolStripMenuItem.Text = "&Identify Hosts From File";
      this.identifyHostsFromFileToolStripMenuItem.ToolTipText = "Read in a file (e.g. ~/.bash_history, ~/.ssh/config) containing hostnames to use " +
    "in a dictionary attack to unhash hostnames";
      this.identifyHostsFromFileToolStripMenuItem.Click += new System.EventHandler(this.identifyHostsFromFileToolStripMenuItem_Click);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.exportToFileToolStripMenuItem.Text = "E&xport to File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
      // 
      // exportToClipboardToolStripMenuItem
      // 
      this.exportToClipboardToolStripMenuItem.Name = "exportToClipboardToolStripMenuItem";
      this.exportToClipboardToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.exportToClipboardToolStripMenuItem.Text = "Export to &Clipboard";
      this.exportToClipboardToolStripMenuItem.Click += new System.EventHandler(this.exportToClipboardToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iP4RangeAttackToolStripMenuItem,
            this.importIntoRegistryToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "&Edit";
      // 
      // iP4RangeAttackToolStripMenuItem
      // 
      this.iP4RangeAttackToolStripMenuItem.Name = "iP4RangeAttackToolStripMenuItem";
      this.iP4RangeAttackToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.iP4RangeAttackToolStripMenuItem.Text = "IP&4 Range Attack";
      this.iP4RangeAttackToolStripMenuItem.ToolTipText = "Specify a range of IPv4 addresses to try against hashed hosts";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineDocumentationToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // onlineDocumentationToolStripMenuItem
      // 
      this.onlineDocumentationToolStripMenuItem.Name = "onlineDocumentationToolStripMenuItem";
      this.onlineDocumentationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.onlineDocumentationToolStripMenuItem.Text = "&Online Documentation";
      // 
      // checkForUpdatesToolStripMenuItem
      // 
      this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
      this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.aboutToolStripMenuItem.Text = "&About";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "Windows Registry Files|*.reg|Text files|*.txt|All files|*.*";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 422);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Location = new System.Drawing.Point(82, 3);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(824, 20);
      this.textBox1.TabIndex = 6;
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // listView1
      // 
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostColumnHeader,
            this.hostKeyTypeHeader,
            this.hostKeyFingerprintHeader});
      this.tableLayoutPanel1.SetColumnSpan(this.listView1, 2);
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listView1.Location = new System.Drawing.Point(3, 23);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(903, 376);
      this.listView1.TabIndex = 4;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick_1);
      this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
      // 
      // hostColumnHeader
      // 
      this.hostColumnHeader.Text = "Host";
      this.hostColumnHeader.Width = 106;
      // 
      // hostKeyTypeHeader
      // 
      this.hostKeyTypeHeader.Text = "Key Type";
      this.hostKeyTypeHeader.Width = 71;
      // 
      // hostKeyFingerprintHeader
      // 
      this.hostKeyFingerprintHeader.Text = "Key Fingerprint";
      this.hostKeyFingerprintHeader.Width = 648;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(73, 20);
      this.label1.TabIndex = 5;
      this.label1.Text = "Search/Solve";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // statusStrip1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
      this.statusStrip1.Location = new System.Drawing.Point(0, 402);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(909, 20);
      this.statusStrip1.TabIndex = 7;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(115, 15);
      this.toolStripStatusLabel1.Text = "PuTTY Known Hosts";
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(93, 15);
      this.toolStripStatusLabel2.Text = "No known hosts";
      // 
      // openFileDialog2
      // 
      this.openFileDialog2.FileName = "openFileDialog2";
      this.openFileDialog2.Filter = "All Files|*.*";
      // 
      // importIntoRegistryToolStripMenuItem
      // 
      this.importIntoRegistryToolStripMenuItem.Name = "importIntoRegistryToolStripMenuItem";
      this.importIntoRegistryToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.importIntoRegistryToolStripMenuItem.Text = "&Import into Registry";
      this.importIntoRegistryToolStripMenuItem.Click += new System.EventHandler(this.importIntoRegistryToolStripMenuItem_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(830, 446);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Putty Known Hosts";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rescanRegistryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem onlineDocumentationToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToClipboardToolStripMenuItem;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem readOpenSSHKnownhostsFileToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader hostColumnHeader;
    private System.Windows.Forms.ColumnHeader hostKeyTypeHeader;
    private System.Windows.Forms.ColumnHeader hostKeyFingerprintHeader;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripMenuItem identifyHostsFromFileToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem iP4RangeAttackToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importIntoRegistryToolStripMenuItem;
  }
}

