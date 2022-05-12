using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Util
    {
        public static int GetInstanceIndex(string instanceId)
        {
            int index = 0;

            if (!int.TryParse(instanceId.Substring(instanceId.LastIndexOf(".") + 1), out index))
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf("_") + 1), out index);

            return index;
        }
    }
}
