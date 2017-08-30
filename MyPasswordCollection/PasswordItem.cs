using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string Site { get;  set; }

        public string Login { get;  set; }

        public string Password { get; set; }

    }
}
