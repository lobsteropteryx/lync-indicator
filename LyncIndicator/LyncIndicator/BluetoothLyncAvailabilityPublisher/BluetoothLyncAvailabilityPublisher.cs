using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncAvailabilityPublisher
{
    class BluetoothLyncAvailabilityPublisher : ILyncAvailabilityPublisher<bool>
    {
        private BluetoothAddress bluetoothAddress;
        private Guid serviceClassId;

        public BluetoothLyncAvailabilityPublisher(string address)
        {
            this.bluetoothAddress = BluetoothAddress.Parse(address);
            this.serviceClassId = Guid.NewGuid();
        }

        public async Task<bool> Send(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            var task = Task.Run(() =>
            {
                var success = false;
                using (var bluetoothClient = new BluetoothClient())
                {
                    var bluetoothEndpoint = new BluetoothEndPoint(this.bluetoothAddress, serviceClassId);
                    bluetoothClient.Connect(bluetoothEndpoint);
                    var bluetoothStream = bluetoothClient.GetStream();

                    if (bluetoothClient.Connected && bluetoothStream != null)
                    {
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        bluetoothStream.Write(buffer, 0, buffer.Length);
                        bluetoothStream.Flush();
                        bluetoothStream.Close();
                        success = true;
                    }
                    return success;
                }
            });
            return await task;
        }
    }
}
