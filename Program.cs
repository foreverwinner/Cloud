using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IClientService> factory = new ChannelFactory<IClientService>(new NetTcpBinding(), "net.tcp://localhost:10100/Input"))
            {
                Console.WriteLine("Slanje poruka dok se ne unese 'x'...\n");

                string answer;

                do
                {
                    Console.Write("Unesite string koji ce se proslediti serveru : ");
                    answer = Console.ReadLine();

                    if (answer.Equals("x"))
                        break;

                    var proxy = factory.CreateChannel();

                    proxy.SendMessage(answer);

                } while (true);
            }
        }
    }
}
