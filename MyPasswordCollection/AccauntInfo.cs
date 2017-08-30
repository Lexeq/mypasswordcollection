﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPasswordCollection
{
    public partial class AccauntInfo : UserControl
    {
        private bool _editEnabled;

        private PasswordItem _item;

        public PasswordItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                tbSite.Text = value.Site;
                tbLogin.Text = value.Login;
                tbPassword.Text = value.Password;
                EditMode = false;
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

        public event EventHandler Edited;

        public AccauntInfo()
        {
            InitializeComponent();
            EditMode = false;
            tbLogin.Click += tb_Click;
            tbPassword.Click += tb_Click;
            tbSite.Click += tb_Click;
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
            Item = Item;
            EditMode = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var item = Item;
            item.Site = tbSite.Text;
            item.Login = tbLogin.Text;
            item.Password = tbPassword.Text;
            Item = item;
            EditMode = false;
            if (Edited != null) Edited(this, EventArgs.Empty);
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

    }
}