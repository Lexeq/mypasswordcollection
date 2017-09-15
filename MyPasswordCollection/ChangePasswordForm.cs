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
    public partial class ChangePasswordForm : Form
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbOldPassword.UseSystemPasswordChar = !cbShowPassword.Checked;
            tbNewPassword1.UseSystemPasswordChar = !cbShowPassword.Checked;
            tbNewPassword2.UseSystemPasswordChar = !cbShowPassword.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbNewPassword1.Text != tbNewPassword2.Text)
            {
                MessageBox.Show("Check new passwords, they must be similar");
                return;
            }
            OldPassword = tbOldPassword.Text;
            NewPassword = tbNewPassword1.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void InputPasswordForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOk_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
