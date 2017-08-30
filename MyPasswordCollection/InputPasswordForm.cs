using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPasswordCollection
{
    public partial class InputPasswordForm : Form
    {
        public string Result { get; private set; }

        public InputPasswordForm()
        {
            InitializeComponent();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbInput.UseSystemPasswordChar = !cbShowPassword.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Result = tbInput.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void InputPasswordForm_Shown(object sender, EventArgs e)
        {
            tbInput.Focus();
            tbInput.SelectAll();
        }
    }
}
