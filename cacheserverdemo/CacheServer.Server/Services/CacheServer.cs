using CacheServer.Contracts.Services;
using NFX;
using NFX.ApplicationModel.Pile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheServer.Server.Services
{
    public class CacheServer : DisposableObject,ICacheServer
    {
        private Dictionary<string, PilePointer> _keyPilePointersStore = new Dictionary<string, PilePointer>();
        private IPile _pile;
        public NFX.OS.ManyReadersOneWriterSynchronizer RWSynchronizer;
        public bool Running { get; set; }

        public CacheServer()
        {
            var pile = new DefaultPile();
            pile.Configure(null);
            // m_Pile.SegmentSize = 512 * 1024 * 1024;
            pile.Start();
            _pile = pile;
            Running = true;
        }


        public byte[] AddOrGetExisiting(string key, byte[] value)
        {
            if (!getWriteLock()) return null;
            try
            {
                if (_keyPilePointersStore.ContainsKey(key))
                {
                    return (byte[])_pile.Get(_keyPilePointersStore[key]);
                }
                _keyPilePointersStore.Add(key, _pile.Put(value));
                return value;
            }
            finally
            {
                releaseWriteLock();
            }
        }

        public bool Contains(string key)
        {
            if (!getReadLock()) return false;
            try
            {
                return _keyPilePointersStore.ContainsKey(key);
            }
            finally
            {
                releaseReadLock();
            }
        }

        protected override void Destructor()
        {
            Running = false;
            base.Destructor();
            foreach (var item in _keyPilePointersStore)
            {
                _pile.Delete(item.Value);
            }
        }

        public byte[] Get(string key)
        {
            if (!getReadLock()) return null;
            try
            {
                if (!_keyPilePointersStore.ContainsKey(key)) return null;
                return (byte[])_pile.Get(_keyPilePointersStore[key]);
            }
            finally
            {
                releaseReadLock();
            }
        }

        public bool Remove(string key)
        {
            if (!getWriteLock()) return false;
            try
            {
                if (_keyPilePointersStore.ContainsKey(key))
                {
                    _pile.Delete(_keyPilePointersStore[key]);
                    _keyPilePointersStore.Remove(key);
                    return true;
                }
                else return false;
            }
            finally
            {
                releaseWriteLock();
            }
        }

        public void Set(string key, byte[] value)
        {
            if (!getWriteLock()) return;
            try
            {
                if (_keyPilePointersStore.ContainsKey(key))
                {
                    _pile.Put(_keyPilePointersStore[key], value);
                }
                else
                {
                    _keyPilePointersStore.Add(key, _pile.Put(value));
                } 
            }
            finally
            {
                releaseWriteLock();
            }
        }


        private bool getReadLock()
        {
            return RWSynchronizer.GetReadLock((_) => !this.Running);
        }

        private void releaseReadLock()
        {
            RWSynchronizer.ReleaseReadLock();
        }

        private bool getWriteLock()
        {
            return RWSynchronizer.GetWriteLock((_) => !this.Running);
        }

        private void releaseWriteLock()
        {
            RWSynchronizer.ReleaseWriteLock();
        }
    }
}
