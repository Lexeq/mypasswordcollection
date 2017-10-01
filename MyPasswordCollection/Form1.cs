using System;
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
        private const string DEFAULTFILENAME = "passwords.m";

        private PasswordCollection _passwords;

        private string DefaultFilePath
        {
            get { return Path.Combine(Application.StartupPath, DEFAULTFILENAME); }
        }

        private bool UIEnabled
        {
            set
            {
                btnAdd.Enabled = value;
                btnRemove.Enabled = value;
                changeMatserPasswordToolStripMenuItem.Enabled = value;
                deletePasswordCollectionToolStripMenuItem.Enabled = value;
                deleteAllPasswordsToolStripMenuItem.Enabled = value;
            }
        }

        private SearchHelper _searcher;

        private PasswordCollection PasswordList
        {
            get { return _passwords; }

            set
            {
                if (_passwords != null)
                    _passwords.ListChanged -= _passwords_ListChanged;

                _passwords = value;
                accauntInfo1.Item = null;

                if (_passwords != null)
                {
                    _searcher = new SearchHelper(_passwords);
                    _passwords.ListChanged += _passwords_ListChanged;
                    listBox1.DataSource = _passwords;
                    listBox1.DisplayMember = "Site";
                    _passwords.ResetBindings();
                    UIEnabled = true;
                }
                else
                {
                    UIEnabled = false;
                    listBox1.DataSource = null;
                    _searcher = null;
                }

            }
        }
        
        public Form1()
        {
            InitializeComponent();
            UIEnabled = false;
            accauntInfo1.ItemEdited += new EventHandler(accauntInfo1_ItemEdited);
            accauntInfo1.EditingCanceled += new EventHandler(accauntInfo1_EditingCanceled);
            openFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.InitialDirectory = Application.StartupPath;
        }

        void accauntInfo1_EditingCanceled(object sender, EventArgs e)
        {
            UpdateAccauntInfoStatus();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var defaultPath = DefaultFilePath;
            if (File.Exists(defaultPath))
            {
                InitPasswordCollection(defaultPath);
            }
        }

        private void _passwords_ListChanged(object sender, ListChangedEventArgs e)
        {
            _searcher.Reset();
            var list = (BindingList<PasswordItem>)sender;
            if (list.Count == 0)
            {
                accauntInfo1.Item = null;
                listBox1.SelectedIndex = -1;
            }
            UpdateAccauntInfoStatus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
            accauntInfo1.Item = new PasswordItem();
            accauntInfo1.EditMode = true;
            UpdateAccauntInfoStatus();
            accauntInfo1.Focus();
        }

        private void accauntInfo1_ItemEdited(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                PasswordList.Add(accauntInfo1.Item);
                listBox1.SelectedIndex = PasswordList.Count - 1;
                UpdateAccauntInfoStatus();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PasswordList != null)
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    accauntInfo1.Item = PasswordList[listBox1.SelectedIndex];
                }
            }
            UpdateAccauntInfoStatus();
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
                InitPasswordCollection(saveFileDialog.FileName, true);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "MyPasswordCollection files (*.m)|*.m";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                InitPasswordCollection(openFileDialog.FileName);
            }
        }

        private void InitPasswordCollection(string path, bool createNew = false)
        {
            using (InputPasswordForm form = new InputPasswordForm(createNew))
            {
                bool repeatflag = true;
                while (repeatflag)
                {
                    form.ShowDialog();
                    if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            if(File.Exists(path) && createNew)
                                File.Delete(saveFileDialog.FileName);
                            PasswordList = new PasswordCollection(path, form.Result);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                var res = MessageBox.Show("Delete password for " + PasswordList[listBox1.SelectedIndex].Site, "Are you sure?", MessageBoxButtons.YesNo);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    PasswordList.RemoveAt(listBox1.SelectedIndex);
                }
            }
            listBox1_SelectedIndexChanged(this, null);
        }

        private void deletePasswordCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("All passwords and file will be removed.", "Are you sure?", MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                var path = PasswordList.FilePath;
                PasswordList.RaiseListChangedEvents = false;
                PasswordList.Clear();
                PasswordList = null;
                File.Delete(path);
            }
        }

        private void UpdateAccauntInfoStatus()
        {
            accauntInfo1.Enabled = listBox1.SelectedIndex >= 0 || accauntInfo1.EditMode;
        }

        private void changeMatserPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (InputPasswordForm form = new InputPasswordForm(true))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PasswordList.ChangePassword(form.Result);
                }
            }
        }

        private void deleteAllPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Do you really want to delete all password?", "Are you sure?", MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                if (PasswordList != null)
                    PasswordList.Clear();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var i = _searcher.FindNext();
            if (i >= 0) listBox1.SelectedIndex = i;
            else _searcher.Reset();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            _searcher.Reset();
            _searcher.SearchString = tbSearch.Text;
            var i = _searcher.FindNext();
            if (i >= 0) listBox1.SelectedIndex = i;
        }
    }
}
