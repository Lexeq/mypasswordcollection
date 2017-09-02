using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MyPasswordCollection
{
    public struct PasswordItem
    {
        public PasswordItem(string site, string login, string password)
            :this()
        {
            Site = site;
            Login = login;
            Password = password;
        }
        public string Site { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }
    }
}
