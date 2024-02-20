using Fish.Networking;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;

namespace ConsoleServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int port = 8554;
            string ip = "127.0.0.1";

            bool isServer = false;

            Console.WriteLine("[1] Host a local server.\n[2] Host a public server.\n[3] Connect to a local server.\n[4] Connect to a public server.");

            IPAddress? ipAdress;

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    isServer = true;

                    Console.Clear();
                    Console.WriteLine("Starting local server...");
                    break;
                case ConsoleKey.D2:
                    isServer = true;
                    {
                        Console.Clear();

                        bool validIp = true;
                        do
                        {
                            if(!validIp)
                                Console.WriteLine("Invalid IP");

                            Console.WriteLine("Please enter local IPV4-adress");
                        } while (!(validIp = IPAddress.TryParse(Console.ReadLine(), out ipAdress)) || ipAdress is null);

                        ip = ipAdress.ToString();

                        Console.Clear();
                        Console.WriteLine("Starting public server...");
                    }
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("Trying to connect to local server...");
                    break;
                case ConsoleKey.D4:
                    {
                        Console.Clear();

                        bool validIp = true;
                        do
                        {
                            if (!validIp)
                                Console.WriteLine("Invalid IP");

                            Console.WriteLine("Please enter local IPV4-adress");
                        } while (!(validIp = IPAddress.TryParse(Console.ReadLine(), out ipAdress)) || ipAdress is null);

                        ip = ipAdress.ToString();

                        Console.Clear();
                        Console.WriteLine("Trying to connect to public server...");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid key");
                    Console.ReadKey(true);
                    break;
            }

            if(isServer)
            {
                Server(ip, port);
            }
            else
            {
                Client(ip, port);
            }
        }

        static void Server(string ip, int port)
        {
            Server server = new Server();
            server.Start(ip, port, 8);

            Console.ReadKey();
        }

        static void Client(string ip, int port)
        {
            Client client = new Client();
            client.Connect(ip, port);

            client.Disconnect();
        }
    }
}
