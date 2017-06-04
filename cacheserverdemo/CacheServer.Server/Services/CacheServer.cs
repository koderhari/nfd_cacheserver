using CacheServer.Contracts.Services;
using NFX.ApplicationModel.Pile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheServer.Server.Services
{
    public class CacheServer : ICacheServer
    {
        private Dictionary<string, PilePointer> keyPilePointersStore = new Dictionary<string, PilePointer>();
        private IPile _pile;
        public NFX.OS.ManyReadersOneWriterSynchronizer RWSynchronizer;

        public CacheServer(IPile pile)
        {
            _pile = pile;
        }

        public byte[] AddOrGetExisiting(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
