using NFX.Glue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheServer.Contracts.Services
{
    [Glued]
    [LifeCycle(ServerInstanceMode.Singleton)]
    public interface ICacheServer
    {
        byte[] AddOrGetExisiting(string key, byte[] value);
        bool Remove(string key);
        byte[] Get(string key);
        void Set(string key, byte[] value);
        bool Contains(string key);
    }
}
