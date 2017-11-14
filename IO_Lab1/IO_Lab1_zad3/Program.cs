using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace IO_Lab1_zad3
{
    class Program
    {
        public static object locker = new object();

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
        //####################
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadTCPServer);
            ThreadPool.QueueUserWorkItem(ThreadTCPClient);
            ThreadPool.QueueUserWorkItem(ThreadTCPClient);
            Thread.Sleep(100);
            Console.ReadKey();
        }
        //####################

        static void ThreadTCPServer(Object StateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ThreadTCPServerAccept, client);
                Thread.Sleep(1000);
            }
        }

        static void ThreadTCPServerAccept(Object StateInfo)
        {
            TcpClient client = StateInfo as TcpClient;
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            client.GetStream().Write(buffer, 0, buffer.Length);
            //int lnt = client.GetStream().Read(buffer, 0, buffer.Length);
            writeConsoleMessage("Server message - " + Encoding.ASCII.GetString(buffer/*, 0 ,lnt*/), ConsoleColor.Red);
            client.Close();
        }

        static void ThreadTCPClient(Object StateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            byte[] message = new ASCIIEncoding().GetBytes("wiadomosc");
            //int lnt = client.GetStream().Read(message, 0, message.Length);
            writeConsoleMessage("Client message - " + Encoding.ASCII.GetString(message/*, 0, lnt*/), ConsoleColor.Green);
            client.GetStream().Write(message, 0, message.Length);
        }
    }
}
