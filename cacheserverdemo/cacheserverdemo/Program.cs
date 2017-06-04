using CacheServer.Contracts.Services.GluedClients;
using NFX;
using NFX.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cacheserverdemo
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var application = new ServiceBaseApplication(args, null))
            {
                var message = Console.ReadLine();

                using (var client = new CacheServerAutoClient("sync://localhost:8080"))
                {
                    while (true)
                    {
                        var input = Console.ReadLine();
                        var cmd = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        exeCommand(client, cmd);
                    }
                    //var responce = client.Echo(message);
                    //Console.WriteLine(responce);
                }
            }
        }

        private static void exeCommand(CacheServerAutoClient client, string[] cmd)
        {
            if (cmd[0].Equals("add"))
            {
                var bytes = Encoding.Default.GetBytes(cmd[2]);
                client.AddOrGetExisiting(cmd[1], bytes);
                return;
            }

            if (cmd[0].Equals("get"))
            {
                var key = cmd[1];
                var bytes = client.Get(key);
                if (bytes == null) Console.WriteLine($"{key} is not exist");
                else Console.WriteLine($"{key} is {Encoding.Default.GetString(bytes)}");
                return;

                
            }

            if (cmd[0].Equals("remove"))
            {
                var key = cmd[1];
                var bytes = client.Remove(key);
                Console.WriteLine($"{key} have been deleted");
                return;
            }

            if (cmd[0].Equals("set"))
            {
                var key = cmd[1];
                client.Set(key, Encoding.Default.GetBytes(cmd[2]));
                Console.WriteLine($"{key} have been set");
                return;
            }

            if (cmd[0].Equals("contains"))
            {
                var key = cmd[1];
                var result = client.Contains(key);
                Console.WriteLine($"{key} is {(result?"exist":"not exist")}");
                return;
            }
        }
    }
}
