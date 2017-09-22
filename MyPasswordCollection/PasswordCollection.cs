using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace MyPasswordCollection
{
    class PasswordCollection : BindingList<PasswordItem>
    {
        private PasswordCrypt Crypter;

        public string FilePath { get; private set; }

        public PasswordCollection(string path, string password)
        {
            FilePath = path;
            Crypter = new PasswordCrypt(path, password);
            if (!File.Exists(path))
                Crypter.Save(this);
            else
            {
                var restore = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                foreach (var item in Crypter.Load())
                {
                    Add(item);
                }
                RaiseListChangedEvents = restore;
            }
        }

        public void ChangePassword(string newPassword)
        {
            Crypter = new PasswordCrypt(FilePath, newPassword);
            Crypter.Save(this);
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            Crypter.Save(this);
            base.OnListChanged(e);
        }
    }
}
