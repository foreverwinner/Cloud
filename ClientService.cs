using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Worker1
{
    internal class ClientService : IClientService
    {
        public void SendMessage(string value)
        {
            Trace.WriteLine($"\tSendMessage method called\n\t\tClient sent a message : {value}", "\nInformation");

            int currentInstanceIndex = Util.GetInstanceIndex(RoleEnvironment.CurrentRoleInstance.Id);

            int forwardInstanceIndex;

            if(currentInstanceIndex == 0)
            {
                forwardInstanceIndex = 1;
            }
            else
            {
                forwardInstanceIndex = currentInstanceIndex - 1;
            }

            string forwardAddress = "";

            foreach(var instance in RoleEnvironment.Roles["Worker1"].Instances)
            {
                if (forwardInstanceIndex == Util.GetInstanceIndex(instance.Id))
                {
                    forwardAddress = $"net.tcp://{instance.InstanceEndpoints["Internal"].IPEndpoint}/Internal";
                    break;
                }
            }

            using(ChannelFactory<IWorker1InterroleService> factory = new ChannelFactory<IWorker1InterroleService>(new NetTcpBinding(), forwardAddress))
            {
                var proxy = factory.CreateChannel();

                proxy.ForwardMessage(value);
            }
        }
    }
}
