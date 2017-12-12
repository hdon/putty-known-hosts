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
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.onlineDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.listView1 = new System.Windows.Forms.ListView();
      this.hostColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.hostKeyTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.hostKeyFingerprintHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
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
      this.rescanRegistryToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.rescanRegistryToolStripMenuItem.Text = "&Re-scan Registry";
      this.rescanRegistryToolStripMenuItem.Click += new System.EventHandler(this.rescanRegistryToolStripMenuItem_Click);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.exportToFileToolStripMenuItem.Text = "E&xport to File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
      // 
      // exportToClipboardToolStripMenuItem
      // 
      this.exportToClipboardToolStripMenuItem.Name = "exportToClipboardToolStripMenuItem";
      this.exportToClipboardToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.exportToClipboardToolStripMenuItem.Text = "Export to &Clipboard";
      this.exportToClipboardToolStripMenuItem.Click += new System.EventHandler(this.exportToClipboardToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
      // listView1
      // 
      this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostColumnHeader,
            this.hostKeyTypeHeader,
            this.hostKeyFingerprintHeader});
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listView1.Location = new System.Drawing.Point(0, 24);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(830, 422);
      this.listView1.TabIndex = 1;
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
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(830, 446);
      this.Controls.Add(this.listView1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Putty Known Hosts";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
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
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader hostColumnHeader;
    private System.Windows.Forms.ColumnHeader hostKeyTypeHeader;
    private System.Windows.Forms.ColumnHeader hostKeyFingerprintHeader;
    private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToClipboardToolStripMenuItem;
  }
}

