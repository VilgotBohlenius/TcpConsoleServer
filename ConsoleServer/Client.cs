using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fish.Networking
{
    public class Client
    {
        byte[] inBuffer = new byte[1024];

        TcpClient tcpClient;
        NetworkStream stream;

        public Client()
        {
            tcpClient = new TcpClient();
        }

        public void Connect(string ip, int port)
        {
            Console.WriteLine($"Client: Trying to connect to '{ip}:{port}'");

            tcpClient.BeginConnect(IPAddress.Parse(ip), port, OnConnect, null);

            void OnConnect(IAsyncResult result)
            {
                tcpClient.EndConnect(result);
                stream = tcpClient.GetStream();

                Console.WriteLine("Client: Connected to server");
            }
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }

        public void SendPacket()
        {
            byte[] bytes = { 177 };
            stream.BeginWrite(bytes, 0, 0, null, null);
        }
    }
}
