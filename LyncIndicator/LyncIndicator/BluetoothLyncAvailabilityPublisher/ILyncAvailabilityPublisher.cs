using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncAvailabilityPublisher
{
    interface ILyncAvailabilityPublisher
    {
        void Send(string availability);
    }
}
