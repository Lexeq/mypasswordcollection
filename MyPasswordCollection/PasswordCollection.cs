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

    class SearchHelper
    {
        private IList<PasswordItem> _collection;

        private string _searchString;

        private int _currentIndex;
        private bool _firstPass;

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                Reset();
                _searchString = value.ToLower();
            }
        }

        public SearchHelper(IList<PasswordItem> collection)
        {
            _collection = collection;
            Reset();
        }

        public int FindNext()
        {
            if (_firstPass)
            {
                while (_currentIndex < _collection.Count)
                {
                    if (_collection[_currentIndex].Site.ToLower().StartsWith(_searchString))
                    {
                        _currentIndex++;
                        return _currentIndex - 1;
                    }
                    _currentIndex++;
                }
                _firstPass = false;
                _currentIndex = 0;
            }
            if (!_firstPass)
            {
                while (_currentIndex < _collection.Count)
                {
                    var siteLower = _collection[_currentIndex].Site.ToLower();
                    if (siteLower.Contains(_searchString) && !siteLower.StartsWith(_searchString))
                    {
                        _currentIndex++;
                        return _currentIndex - 1;
                    }
                    _currentIndex++;
                }
            }
            return -1;

        }

        public void Reset()
        {
            _currentIndex = 0;
            _firstPass = true;
        }
    }
}
