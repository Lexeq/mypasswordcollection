﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace MyPasswordCollection
{
    public partial class Form1 : Form
    {

        private BindingList<PasswordItem> _passwords;

        private bool UIEnabled
        {
            set
            {
                accauntInfo1.Enabled = value;
                btnAdd.Enabled = value;
                btnRemove.Enabled = value;
                changeMatserPasswordToolStripMenuItem.Enabled = value;
                deletePasswordCollectionToolStripMenuItem.Enabled = value;

            }
        }

        private BindingList<PasswordItem> PasswordList
        {
            get { return _passwords; }
            set
            {
                if (value != null)
                {
                    _passwords = value;
                    listBox1.DataSource = value;
                    listBox1.DisplayMember = "Site";
                    UIEnabled = true;
                }
                else
                {
                    UIEnabled = false;
                }
            }
        }

        private PasswordCrypt _crypter;


        public Form1()
        {
            InitializeComponent();
            UIEnabled = false;
        }

        private void LoadPasswords(string path, string password)
        {
            _crypter = new PasswordCrypt(password);
            var ps = _crypter.LoadAndDecrypt(path);
            PasswordList = new BindingList<PasswordItem>(ps);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            accauntInfo1.Item = new PasswordItem();
            accauntInfo1.EditMode = true;
            listBox1.SelectedIndex = -1;
        }

        private void accauntInfo1_Edited(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {
                PasswordList.RemoveAt(listBox1.SelectedIndex);
            }
            PasswordList.Add((sender as AccauntInfo).Item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = "supapasss";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                accauntInfo1.Item = PasswordList[listBox1.SelectedIndex];
            else
                accauntInfo1.Item = new PasswordItem();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var defaultPath = Path.Combine(Application.StartupPath, "passwords.m");
            if (File.Exists(defaultPath))
            {
                using (InputPasswordForm form = new InputPasswordForm())
                {
                    form.StartPosition = FormStartPosition.CenterParent;
                    bool repeatflag = true;
                    while (repeatflag)
                    {
                        form.ShowDialog();
                        if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                LoadPasswords(defaultPath, form.Result);
                                repeatflag = false;
                            }
                            catch (Exception ex)
                            {
                                var dr = MessageBox.Show("Decryption failed. Try again?", ex.GetType().Name, MessageBoxButtons.YesNo);
                                if (dr == System.Windows.Forms.DialogResult.No)
                                    repeatflag = false;
                            }

                        }
                        else
                        {
                            repeatflag = false;
                        }
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "MyPasswordCollection files (*.m)|*.m";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (InputPasswordForm pasform = new InputPasswordForm())
                {
                    var res = pasform.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        _crypter = new PasswordCrypt(pasform.Result);
                        PasswordList = new BindingList<PasswordItem>();      
                        _crypter.Save(saveFileDialog.FileName, PasswordList);
                    }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "MyPasswordCollection files (*.m)|*.m";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (InputPasswordForm pasform = new InputPasswordForm())
                {
                    var res = pasform.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        _crypter = new PasswordCrypt(pasform.Result);
                        PasswordList = new BindingList<PasswordItem>(_crypter.LoadAndDecrypt(openFileDialog.FileName));
                    }
                }
            }
        }
    }
}