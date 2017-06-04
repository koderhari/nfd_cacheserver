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
                var num = cmd[1].AsInt();
                //client.Add(num);
                var bytes = client.Get();
                if (bytes == null) Console.WriteLine($"{key} is not exist");
                else Console.WriteLine($"{key} is {Encoding.Default.GetString(bytes)}");
                var bytes = Encoding.Default.GetBytes(cmd[2]);
                client.AddOrGetExisiting(cmd[1], bytes);
                return;

                
            }

            if (cmd[0].Equals("result"))
            {
                //var value = client.GetValue();
                //Console.WriteLine("Server state: " + value);
                return;
            }

            if (cmd[0].Equals("done"))
            {
                //client.Done();
                return;
            }
        }
    }
}
