using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
  public partial class Form2 : Form
  {
    public bool ignoreFutureErrors { 
      get;
      private set;
    }

    public Form2(string message, int lineNumber, string badLine)
    {
      InitializeComponent();

      var msg1 = new StringBuilder();
      msg1.AppendFormat("An error was encountered parsing your input on line {0}", lineNumber);
      label1.Text = msg1.ToString();

      textBox1.Text = badLine;

      textBox2.Text = message;
    }

    /* TODO is this idiomatic of Windows Forms? Is there better way? */
    public new DialogResult ShowDialog()
    {
      var result = base.ShowDialog();
      ignoreFutureErrors = ignoreFutureErrorsCheckBox.Checked;
      return result;
    }
  }
}
