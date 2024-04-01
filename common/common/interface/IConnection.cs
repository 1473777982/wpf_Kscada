using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public interface IConnection
    {
        void Stop();
        bool CheckStoped();
        bool WriteValue(runTag tag);
    }
}
