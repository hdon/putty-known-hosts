namespace WindowsFormsApplication2
{
  partial class Form2
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.ignoreFutureErrorsCheckBox = new System.Windows.Forms.CheckBox();
      this.noButton = new System.Windows.Forms.Button();
      this.yesButton = new System.Windows.Forms.Button();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.textBox2, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 5;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(620, 173);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(614, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Location = new System.Drawing.Point(3, 16);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(614, 20);
      this.textBox1.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(3, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(614, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Would you like to continue loading after this line?";
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.ignoreFutureErrorsCheckBox);
      this.flowLayoutPanel1.Controls.Add(this.noButton);
      this.flowLayoutPanel1.Controls.Add(this.yesButton);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 81);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(614, 115);
      this.flowLayoutPanel1.TabIndex = 3;
      // 
      // ignoreFutureErrorsCheckBox
      // 
      this.ignoreFutureErrorsCheckBox.AutoSize = true;
      this.ignoreFutureErrorsCheckBox.Location = new System.Drawing.Point(3, 3);
      this.ignoreFutureErrorsCheckBox.Name = "ignoreFutureErrorsCheckBox";
      this.ignoreFutureErrorsCheckBox.Size = new System.Drawing.Size(119, 17);
      this.ignoreFutureErrorsCheckBox.TabIndex = 0;
      this.ignoreFutureErrorsCheckBox.Text = "&Ignore Future Errors";
      this.ignoreFutureErrorsCheckBox.UseVisualStyleBackColor = true;
      // 
      // noButton
      // 
      this.noButton.DialogResult = System.Windows.Forms.DialogResult.No;
      this.noButton.Location = new System.Drawing.Point(128, 3);
      this.noButton.Name = "noButton";
      this.noButton.Size = new System.Drawing.Size(75, 23);
      this.noButton.TabIndex = 1;
      this.noButton.Text = "&No";
      this.noButton.UseVisualStyleBackColor = true;
      // 
      // yesButton
      // 
      this.yesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.yesButton.Location = new System.Drawing.Point(209, 3);
      this.yesButton.Name = "yesButton";
      this.yesButton.Size = new System.Drawing.Size(75, 23);
      this.yesButton.TabIndex = 2;
      this.yesButton.Text = "&Yes";
      this.yesButton.UseVisualStyleBackColor = true;
      // 
      // textBox2
      // 
      this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox2.Location = new System.Drawing.Point(3, 42);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBox2.Size = new System.Drawing.Size(614, 20);
      this.textBox2.TabIndex = 4;
      // 
      // Form2
      // 
      this.AcceptButton = this.yesButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.noButton;
      this.ClientSize = new System.Drawing.Size(620, 173);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "Form2";
      this.Text = "Parse Error";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.CheckBox ignoreFutureErrorsCheckBox;
    private System.Windows.Forms.Button noButton;
    private System.Windows.Forms.Button yesButton;
    private System.Windows.Forms.TextBox textBox2;
  }
}