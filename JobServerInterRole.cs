using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class JobServerInterRole
    {
        private ServiceHost serviceHost;

        private String externalEndpointName = "InternalRequest";

        public JobServerInterRole()
        {
            RoleInstanceEndpoint internalEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = String.Format("net.tcp://{0}/{1}", internalEndPoint.IPEndpoint, externalEndpointName);
            serviceHost = new ServiceHost(typeof(JobServerInterRoleProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IJobInterRole), binding, endpoint);

        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation("Host for {0} endpoint type successfully opened at {1}", externalEndpointName, DateTime.Now);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close error for {0} endpoint type.Error message is: {1}.", externalEndpointName, e.Message);
            }
        }
        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation("Host for {0} endpoint type successfully closed at {1}", externalEndpointName, DateTime.Now);
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close erro for {0} endpoint type.Error message is: {1}", externalEndpointName, e.Message);
            }
        }
    }
}
