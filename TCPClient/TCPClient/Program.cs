using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1302;
            string ipAddress = "127.0.0.1";
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            ClientSocket.Connect(ep);
            Console.WriteLine("Client is Connected!");

            Program p = new Program();

            while (true)
            {
                string messageFromClient = null;
                Console.Write("You : ");
                messageFromClient = Console.ReadLine();
                ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0,
                messageFromClient.Length, SocketFlags.None);

                byte[] message = new byte[1024];
                int size = ClientSocket.Receive(message);
                Console.WriteLine("Server : " + System.Text.Encoding.ASCII.GetString(message, 0, size));
            }
        }
    }
}