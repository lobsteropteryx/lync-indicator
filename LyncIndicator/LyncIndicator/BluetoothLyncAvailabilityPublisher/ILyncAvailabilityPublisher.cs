using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncAvailabilityPublisher
{
    public interface ILyncAvailabilityPublisher
    {
        Task<bool> Send(string availability);
    }
}
