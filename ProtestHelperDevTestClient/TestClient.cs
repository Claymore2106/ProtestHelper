using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProtestHelperBackend;
using Newtonsoft.Json;

namespace ProtestHelperDevTestClient
{
    class TestClient
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("10.11.5.101");
            }).Start();
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Connect("10.11.5.101");
            //}).Start();
            Console.ReadLine();
        }
        static void Connect(String server)
        {
            try
            {
                var testObject = new Item { ItemName = "Water", Count = 10 };

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(server), 11235);

                Socket sender = new Socket(remoteEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remoteEP);
                Console.WriteLine("Connected!");

                var jsonString = JsonConvert.SerializeObject(testObject);
                byte[] msg = Encoding.UTF8.GetBytes(jsonString);
                Console.WriteLine("Payload size : {0}", msg.Length);
                int sentBytes = sender.Send(Encoding.UTF8.GetBytes(msg.Length.ToString()));

                Console.WriteLine("Sent size data. Waiting on confirmation...");
                var confirmationBuffer = new byte[1];
                int confirmation = sender.Receive(confirmationBuffer);
                
                Console.WriteLine("Confirmed {0}. Sending payload...", Encoding.ASCII.GetString(confirmationBuffer));
                sender.Send(msg);
                Console.WriteLine("Sent!");

                Console.ReadKey();
                sender.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}
