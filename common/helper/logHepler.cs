using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.helper
{
    public class logHepler
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void addLog_common(string operation)
        {
            Log.Info(DateTime.Now.ToString() + " " + operation );
        }

    }
}
