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
    class JobServerInterRoleProvider : IJobInterRole
    {
        public void ProslediBratskoj(string message)
        {
            Trace.WriteLine("Poruka od bratske je primljena: " + message);
            Trace.WriteLine("Prosledjujemo je sada drugoj WorkerRoli.");
            //KOD ZA PROSLEDJIVANJE WORKER ROLI 2 SA INSTANCOM ISTOG ID
            /*string currentInstance = RoleEnvironment.CurrentRoleInstance.Id;
            string[] pomocni = currentInstance.Split('_');
            int currentId = int.Parse(pomocni[2]);
            Trace.WriteLine(currentId);

            string adresa = String.Format("net.tcp://{0}/InternalRequest", RoleEnvironment.Roles["WorkerRole2"].Instances[currentId].InstanceEndpoints["InternalRequest"].IPEndpoint);

            var factory = new ChannelFactory<IJob2>(new NetTcpBinding(), new EndpointAddress(adresa));

            var proxy = factory.CreateChannel();
            proxy.SendMessage(message);
            */

            //PROSLEDJIVANJE SVIM INSTANACAM 2 WORKER ROLE
            foreach (var instance in RoleEnvironment.Roles["WorkerRole2"].Instances)
            {
                string adresa = String.Format("net.tcp://{0}/InternalRequest", instance.InstanceEndpoints["InternalRequest"].IPEndpoint);
                var factory = new ChannelFactory<IJob2>(new NetTcpBinding(), new EndpointAddress(adresa));
                var proxy = factory.CreateChannel();
                proxy.SendMessage(message);
            }
        }
    }
}
