using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker2
{
    internal class Worker2InterroleService : IWorker2InterroleService
    {
        public void ForwardMessageToWorker2(string value)
        {
            Trace.WriteLine($"\tForwardMessageToWorker2 method called\n\t\tClient sent a message : {value}", "\nInformation");
        }
    }
}
