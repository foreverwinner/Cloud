using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WorkerRole1
{
    public class JobServer
    {
        ServiceHost serviceHost;
        private string externalEndPointName = "InputRequest";
        public JobServer()
        {
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndPointName];
            string endpoint = String.Format("net.tcp://{0}/{1}",inputEndPoint.IPEndpoint, externalEndPointName);
            serviceHost = new ServiceHost(typeof(JobServerProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IJob), binding, endpoint);
        }
        public void Open()
        {
            serviceHost.Open();
        }
        public void Close()
        {
            serviceHost.Close();
        }
    }
}
