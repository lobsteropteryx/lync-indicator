using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncAvailabilityPublisher
{
    public class ConsoleLyncAvailabilityPublisher : ILyncAvailabilityPublisher
    {
        public async Task<bool> Send(string availability)
        {
            var task = Task.Run(() =>
                {
                    Console.WriteLine(availability);
                    return true;
                });
            return await task;
        }
    }
}
