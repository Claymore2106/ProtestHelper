using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using Newtonsoft.Json;

namespace ProtestHelperBackend.Networking
{
    class Server
    {
        //TcpListener server = null;
        public Server(string ip, int port)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            IPEndPoint localEndPoint = new IPEndPoint(localAddr, 11235);
            Socket server = new Socket(localAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(localEndPoint);
            //var server = new TcpListener(localAddr, port);
            server.Listen(10);
            StartListener(server);
        }

        public void StartListener(Socket server)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    var client = server.Accept();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server.Close();
            }
        }
        public void HandleDeivce(object obj)
        {
            var client = (Socket)obj;
            //TcpClient client = (TcpClient)obj;
            //var stream = client.;
            // imei = String.Empty;
            string data = null;
            //byte[] bytes = null;
            //int bytesRec = 0;
            //int i;
            try
            {
                var payloadBuffer = new byte[1024];
                var payloadSize = client.Receive(payloadBuffer);
                var payloadBufferSize = int.Parse(Encoding.UTF8.GetString(payloadBuffer, 0, payloadSize));
                payloadBuffer = new byte[payloadBufferSize];
                client.Send(new byte[] { 49 });
                payloadSize = client.Receive(payloadBuffer);
                data += Encoding.UTF8.GetString(payloadBuffer, 0, payloadSize);

                Console.WriteLine(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
}