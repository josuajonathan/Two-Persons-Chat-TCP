using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1302;
            string ipAddress = "127.0.0.1";
            Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            ServerListener.Bind(ep);
            ServerListener.Listen(100);
            Console.WriteLine("Server is Listening....");
            Socket ClientSocket = default(Socket);
            int num = 0;
            Program p = new Program();

            while (true)
            {
                num++;
                ClientSocket = ServerListener.Accept();
                Console.WriteLine(num + " Clients Connected");

                Thread UserThread = new Thread(new ThreadStart(() => p.User(ClientSocket)));
                UserThread.Start();
            }

        }

        public void User(Socket client)
        {
            while (true)
            {
                byte[] message = new byte[1024];
                int size = client.Receive(message);
                Console.WriteLine("Client : " + System.Text.Encoding.ASCII.GetString(message, 0, size));

                string messageFromServer = null;
                Console.Write("You : ");
                messageFromServer = Console.ReadLine();
                client.Send(System.Text.Encoding.ASCII.GetBytes(messageFromServer), 0,
                messageFromServer.Length, SocketFlags.None);
            }
        }
    }
}