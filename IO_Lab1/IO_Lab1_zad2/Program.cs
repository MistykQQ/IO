using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IO_Lab1_zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadTCPServer);
            ThreadPool.QueueUserWorkItem(ThreadTCPClient);
            ThreadPool.QueueUserWorkItem(ThreadTCPClient);
            Thread.Sleep(100);
            Console.ReadKey();
        }

        static void ThreadTCPServer(Object StateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.Close();
                Thread.Sleep(100);
            }
        }

        static void ThreadTCPClient(Object StateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            byte[] message = new ASCIIEncoding().GetBytes("wiadomosc");
            client.GetStream().Write(message, 0, message.Length);
            Console.WriteLine("Client message {0}", Encoding.ASCII.GetString(message));
        }
    }
}
