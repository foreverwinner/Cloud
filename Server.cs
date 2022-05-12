using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker2
{
    internal class Server
    {
        private ServiceHost workerServiceHost = new ServiceHost(typeof(Worker2InterroleService));

        public Server()
        {
            var workerServiceEndpoin = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Internal"].IPEndpoint;

            workerServiceHost.AddServiceEndpoint(typeof(IWorker2InterroleService), new NetTcpBinding(), $"net.tcp://{workerServiceEndpoin}/Internal");
        }

        public void Open()
        {
            try
            {
                workerServiceHost.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Close()
        {
            try
            {
                workerServiceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
