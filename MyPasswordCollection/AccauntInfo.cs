using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPasswordCollection
{
    public sealed partial class AccauntInfo : UserControl
    {
        private bool _editEnabled;

        private PasswordItem _item;

        public PasswordItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                EditMode = false;

                if (_item == null)
                {
                    tbSite.Text = string.Empty;
                    tbLogin.Text = string.Empty;
                    tbPassword.Text = string.Empty;
                }
                else
                {
                    tbSite.Text = value.Site;
                    tbLogin.Text = value.Login;
                    tbPassword.Text = value.Password;
                }
            }
        }

        public bool EditMode
        {
            set
            {
                _editEnabled = value;
                tbLogin.ReadOnly = !_editEnabled;
                tbPassword.ReadOnly = !_editEnabled;
                tbSite.ReadOnly = !_editEnabled;
                btnSave.Visible = _editEnabled;
                btnCancel.Visible = _editEnabled;
                btnSiteToClipboard.Visible = !_editEnabled;
                btnLoginToClipboard.Visible = !_editEnabled;
                btnPasswordToClipboard.Visible = !_editEnabled;
                btnEdit.Visible = !_editEnabled;
            }
            get
            {
                return _editEnabled;
            }
        }

        public event EventHandler ItemEdited;
        public event EventHandler EditingCanceled;

        public AccauntInfo()
        {
            InitializeComponent();
            EditMode = false;
            tbLogin.Click += tb_Click;
            tbPassword.Click += tb_Click;
            tbSite.Click += tb_Click;
        }

        private void CancelEditing()
        {
            Item = _item;
            EditMode = false;
            OnEditingCanceled(EventArgs.Empty);
        }

        private void SaveItem()
        {
            Item.SetNewValues(tbSite.Text, tbLogin.Text, tbPassword.Text);
            EditMode = false;
            OnItemEdited(EventArgs.Empty);
        }

        void tb_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && !EditMode)
                tb.SelectAll();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode = true;
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = !cbShowPassword.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelEditing();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        private void btnSiteToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSite.Text))
                Clipboard.SetText(tbSite.Text);
        }

        private void btnLoginToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text))
                Clipboard.SetText(tbLogin.Text);
        }

        private void btnPasswordToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPassword.Text))
                Clipboard.SetText(tbPassword.Text);
        }

        private void OnItemEdited(EventArgs e)
        {
            var ev = ItemEdited;
            if (ev != null)
                ev(this, e);
        }

        private void OnEditingCanceled(EventArgs e)
        {
            var ev = EditingCanceled;
            if (ev != null)
                ev(this, e);
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (sender == tbSite || sender == tbLogin)
                {
                    this.ProcessTabKey(true);
                }
                else
                {
                    SaveItem();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                CancelEditing();
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
    }
}
