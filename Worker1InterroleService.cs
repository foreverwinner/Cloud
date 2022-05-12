using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker1
{
    internal class Worker1InterroleService : IWorker1InterroleService
    {
        public void ForwardMessage(string value)
        {
            Trace.WriteLine($"\tForwardMessage method called\n\t\tClient sent a message : {value}", "\nInformation");

            var instanceIndex = Util.GetInstanceIndex(RoleEnvironment.CurrentRoleInstance.Id);

            string forwardAddress = "";

            foreach(var instance in RoleEnvironment.Roles["Worker2"].Instances)
            {
                if(instanceIndex == Util.GetInstanceIndex(instance.Id))
                {
                    forwardAddress = $"net.tcp://{instance.InstanceEndpoints["Internal"].IPEndpoint}/Internal";
                    break;
                }
            }

            using(ChannelFactory<IWorker2InterroleService> factory = new ChannelFactory<IWorker2InterroleService>(new NetTcpBinding(), forwardAddress))
            {
                var proxy = factory.CreateChannel();

                proxy.ForwardMessageToWorker2(value);
            }
        }
    }
}
