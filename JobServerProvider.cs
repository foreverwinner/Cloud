using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WorkerRole1
{
    public class JobServerProvider : IJob
    {
        public void Send(string message)
        {
            Trace.WriteLine("Poruka od klijenta je primljena u prvu istancu i glasi: " + message);
            Trace.WriteLine("Prosledjujemo je sada bratskoj instanci.");
            string currentInstance = RoleEnvironment.CurrentRoleInstance.Id;
            string[] pomocni = currentInstance.Split('_');
            int currentId = int.Parse(pomocni[2]);
            //int next = (currentId - 1) % 2;
            int next = 0;
            if (currentId >= 1)
                next = currentId - 1;
            else
                next = 1;
            foreach(var instance in RoleEnvironment.Roles["WorkerRole1"].Instances)
            {
                int p = int.Parse(instance.Id.Split('_')[2]);
                if (p == next)
                {
                    EndpointAddress adresa = new EndpointAddress($"net.tcp://{instance.InstanceEndpoints["InternalRequest"].IPEndpoint}/InternalRequest");
                    var factory = new ChannelFactory<IJobInterRole>(new NetTcpBinding(), adresa);
                    var proxy = factory.CreateChannel();
                    proxy.ProslediBratskoj(message);
                    break;
                }
            }

        }

    }
}
