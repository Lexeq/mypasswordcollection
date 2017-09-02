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
            this.StartPosition = FormStartPosition.CenterParent;
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

        private void InputPasswordForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOk_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                this.btnCancel_Click(this, EventArgs.Empty);
            else if (e.Control == true && e.KeyCode == Keys.A)
            {
                tbInput.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
