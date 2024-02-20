using Fish.Networking;
using System.Net;

namespace ConsoleServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int port = 8554;
            string ip = "127.0.0.1";

            Server server = new Server();
            Client client = new Client();

            bool isHost = false;

            Console.WriteLine("Please enter the target IP address.\n * /l to host a local server.\n * /h to host a public server.");

            switch (Console.ReadLine()!)
            {
                case "/l":
                    isHost = true;

                    break;
                case "/h":
                    isHost = true;

                    goto default;
                default:
                    bool validIp = false;
                    while (!validIp)
                    {
                        Console.WriteLine("Please enter IPV4 adress.");

                        validIp = IPAddress.TryParse(Console.ReadLine()!, out IPAddress? ipAdress) && ipAdress is not null;
                        if (!validIp)
                        {
                            Console.WriteLine($"Ip '{ipAdress}' is not a valid ip.");
                        }

                        ip = ipAdress.ToString();
                    }
                    break;
            }

            Console.WriteLine($"Using '{ip}'");

            if (isHost)
            {
                server.Start(ip, port, 2);
            }
            else
            {
                client.Connect(ip, port);
            }

            Console.ReadKey();

            if (isHost)
            {
                server.Close();
            }
            else
            {
                client.Disconnect();
            }
        }
    }
}
