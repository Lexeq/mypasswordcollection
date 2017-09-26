using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MyPasswordCollection
{
    public class PasswordItem : INotifyPropertyChanged
    {
        private string _site;

        private string _login;

        private string _password;

        public string Site
        {
            get { return _site; }
            set
            {
                _site = value;
                OnPropertyChanged("Site");
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PasswordItem() : this("", "", "") { }

        public PasswordItem(string site, string login, string password)
        {
            SetNewValues(site, login, password);
        }

        public virtual void SetNewValues(string site, string login, string password)
        {
            _site = site;
            _login = login;
            _password = password;
            OnPropertyChanged(null);
        }

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
        }
    }
}
