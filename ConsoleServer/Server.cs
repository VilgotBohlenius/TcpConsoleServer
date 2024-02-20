using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fish.Networking
{
    public class Server
    {
        TcpListener tcpListener;

        byte[] inbuffer = new byte[1024];

        // Clients
        NetworkStream[] stream;

        public void Start(string ip, int port, int maxClients)
        {
            tcpListener = new TcpListener(IPAddress.Parse(ip), port);

            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(OnAcceptTcpClient, null);

            stream = new NetworkStream[maxClients];

            Console.WriteLine($"Server: Listening on port '{port}'");
        }

        public void Close()
        {
            tcpListener.Stop();
        }

        void OnAcceptTcpClient(IAsyncResult result)
        {
            TcpClient tcpClient = tcpListener.EndAcceptTcpClient(result);

            Console.WriteLine($"Server: Client connected from '{tcpClient.Client.RemoteEndPoint}'");

            tcpListener.BeginAcceptTcpClient(OnAcceptTcpClient, null);
        }

        public void SendPacket()
        {

        }
    }
}
