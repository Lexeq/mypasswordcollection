using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace MyPasswordCollection
{
    class PasswordCrypt
    {
        private const int HEADER = 537413279;

        private byte[] _key;
        private byte[] _iv;

        public PasswordCrypt(string password)
        {
            GetKeyAndIVFromPassword(password, ref _key, ref _iv);
        }

        public void Save(string path, IEnumerable<PasswordItem> items)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                if (string.IsNullOrEmpty(path))
                    throw new ArgumentException("path");

                fs.Write(BitConverter.GetBytes(HEADER), 0, 4);
                fs.Write(_iv, 0, 16);

                List<byte> bytes = new List<byte>();
                foreach (var item in items)
                {
                    bytes.AddRange(PasswordItemToBytes(item));
                }
                var encbytes = EncryptBytes(bytes.ToArray(), _key, _iv);
                fs.Write(encbytes, 0, encbytes.Length);
            }
        }

        public IList<PasswordItem> LoadAndDecrypt(string path)
        {
            var ebytes = File.ReadAllBytes(path);

            if (BitConverter.ToInt32(TakeRange(ebytes, 0, 4), 0) != HEADER)
                throw new InvalidDataException("Неверный файл или файл поврежден");

            List<PasswordItem> list = new List<PasswordItem>();

            var iv = TakeRange(ebytes, 4, 16);
            var dbytes = DecryptBytes(TakeRange(ebytes, 20, ebytes.Length - 20), _key, iv);

            for (int i = 0; i < dbytes.Length; )
            {
                int siteLen = BitConverter.ToInt32(TakeRange(dbytes, i, 4), 0);
                int logLen = BitConverter.ToInt32(TakeRange(dbytes, i+=4, 4), 0);
                int pasLen = BitConverter.ToInt32(TakeRange(dbytes, i+=4, 4), 0);

                string site = Encoding.Unicode.GetString(TakeRange(dbytes, i+=4, siteLen));
                string login = Encoding.Unicode.GetString(TakeRange(dbytes, i+=siteLen, logLen));
                string pas = Encoding.Unicode.GetString(TakeRange(dbytes, i+= logLen, pasLen));

                list.Add(new PasswordItem(site, login, pas));

                i += pasLen;
            }

            return list;
        }

        public void GetKeyAndIVFromPassword(string password, ref byte[] key, ref byte[] iv)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes =
                new Rfc2898DeriveBytes(password, new byte[] { 101, 025, 158, 159, 044, 111, 222, 092 });
            key = rfc2898DeriveBytes.GetBytes(32);
            iv = rfc2898DeriveBytes.GetBytes(16);
        }

        private byte[] TakeRange(byte[] source, int start, int length)
        {
            byte[] res = new byte[length];
            Array.Copy(source, start, res, 0, res.Length);
            return res;
        }

        private byte[] PasswordItemToBytes(PasswordItem item)
        {
            var sbts = Encoding.Unicode.GetBytes(item.Site);
            var lbts = Encoding.Unicode.GetBytes(item.Login);
            var pbts = Encoding.Unicode.GetBytes(item.Password);

            List<byte> bts = new List<byte>(12 + sbts.Length + lbts.Length + pbts.Length);

            bts.AddRange(BitConverter.GetBytes(sbts.Length));
            bts.AddRange(BitConverter.GetBytes(lbts.Length));
            bts.AddRange(BitConverter.GetBytes(pbts.Length));
            bts.AddRange(sbts);
            bts.AddRange(lbts);
            bts.AddRange(pbts);

            return bts.ToArray();
        }

        private byte[] EncryptBytes(byte[] data, byte[] key, byte[] IV)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        private byte[] DecryptBytes(byte[] data, byte[] key, byte[] IV)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] decr;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var msDecrypt = new MemoryStream(data))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            csDecrypt.CopyTo(ms);
                            decr = ms.ToArray();
                        }
                    }
                }
            }

            return decr;
        }
    }
}
