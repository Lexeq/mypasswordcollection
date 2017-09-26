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
        private bool confirmation;

        public string Result { get; set; }

        public InputPasswordForm(bool confirmation)
        {
            InitializeComponent();
            this.confirmation = confirmation;
            if (!confirmation)
            {
                lblConfirm.Visible = false;
                tbConfirm.Visible = false;
                Size = new Size(Size.Width, Size.Height - 20);
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = !cbShowPassword.Checked;
            tbConfirm.UseSystemPasswordChar = !cbShowPassword.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (confirmation && tbPassword.Text != tbConfirm.Text)
            {
                MessageBox.Show("Check new passwords, they must be similar");
                return;
            }
            Result = tbPassword.Text;
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

        private void InputPasswordForm_Shown(object sender, EventArgs e)
        {
            tbPassword.Focus();
            tbPassword.SelectAll();
        }
    }
}
