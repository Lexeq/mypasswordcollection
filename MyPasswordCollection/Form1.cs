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
        //TODO:
        //Fix cpf hotkeys
        //Fix wrong old pass behaviour
        //clear after delete last 
        private BindingList<PasswordItem> _passwords;

        private PasswordCrypt _crypter;

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
                if (_passwords != null)
                    _passwords.ListChanged -= _passwords_ListChanged;

                _passwords = value;

                if (_passwords != null)
                {
                    listBox1.DataSource = value;
                    listBox1.DisplayMember = "Site";

                    UIEnabled = true;
                    _passwords.ListChanged += _passwords_ListChanged;
                }
                else
                {
                    UIEnabled = false;
                }
            }
        }

        private PasswordCrypt Crypter
        {
            get
            {
                return _crypter;
            }
            set
            {
                accauntInfo1.Item = new PasswordItem();
                _crypter = value;
                PasswordList = _crypter == null ? null : new BindingList<PasswordItem>(value.LoadOrCreate());
            }
        }

        public Form1()
        {
            InitializeComponent();
            UIEnabled = false;
            accauntInfo1.EditCompleted += new EventHandler(accauntInfo1_EditCompleted);
            accauntInfo1.EditCanceled += new EventHandler(accauntInfo1_EditCanceled);
        }

        private void accauntInfo1_EditCanceled(object sender, EventArgs e)
        {
            SetAccInfoEnabled();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var defaultPath = Path.Combine(Application.StartupPath, "passwords.m");
            if (File.Exists(defaultPath))
            {
                InitCrypter(defaultPath);
            }
        }

        private void _passwords_ListChanged(object sender, ListChangedEventArgs e)
        {
            var list = (BindingList<PasswordItem>)sender;
            if (list.Count == 0)
                accauntInfo1.Item = new PasswordItem();
            Crypter.Save((BindingList<PasswordItem>)sender);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
            accauntInfo1.EditMode = true;
            accauntInfo1.Item = new PasswordItem();
            SetAccInfoEnabled();
        }

        private void accauntInfo1_EditCompleted(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                PasswordList[listBox1.SelectedIndex] = accauntInfo1.Item;
            }
            else
            {
                PasswordList.Add(accauntInfo1.Item);
                listBox1.SelectedIndex = PasswordList.Count - 1;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PasswordList != null)
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    accauntInfo1.EditMode = false;
                    accauntInfo1.Item = PasswordList[listBox1.SelectedIndex];
                }
            }
            else
            {
                accauntInfo1.Item = new PasswordItem();
            }
            SetAccInfoEnabled();
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
                if (File.Exists(saveFileDialog.FileName))
                    File.Delete(saveFileDialog.FileName);
                InitCrypter(saveFileDialog.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "MyPasswordCollection files (*.m)|*.m";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                InitCrypter(openFileDialog.FileName);
            }
        }

        private void InitCrypter(string path)
        {
            using (InputPasswordForm pasform = new InputPasswordForm())
            {
                bool repeatflag = true;
                while (repeatflag)
                {
                    pasform.ShowDialog();
                    if (pasform.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            Crypter = new PasswordCrypt(path, pasform.Result);
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
                var res = MessageBox.Show("Delete password for " + PasswordList[listBox1.SelectedIndex].Site, "Are you shure?", MessageBoxButtons.YesNo);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    PasswordList.RemoveAt(listBox1.SelectedIndex);
                }
            }
        }

        private void deletePasswordCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("All passwords will be removed.", "Are you sure?", MessageBoxButtons.YesNo);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                File.Delete(Crypter.FilePath);
                Crypter = null;
            }
        }

        private void SetAccInfoEnabled()
        {
            accauntInfo1.Enabled = listBox1.SelectedIndex >= 0 || accauntInfo1.EditMode;
        }

        private void changeMatserPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ChangePasswordForm form = new ChangePasswordForm())
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    var path = Crypter.FilePath;
                    try
                    {
                        PasswordCrypt oldcr = new PasswordCrypt(path, form.OldPassword);
                        var items = oldcr.LoadOrCreate();
                        File.Delete(path);
                        var newcr = new PasswordCrypt(path, form.NewPassword);
                        newcr.Save(items);
                    }
                    catch (CryptographicException)
                    {
                        MessageBox.Show("Failed to change password: Check password.");
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Failed to change password: File unavaible.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to change password: General exeption", ex.GetType().Name);
                    }

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
    }
}
