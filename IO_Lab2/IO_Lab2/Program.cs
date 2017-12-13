using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_Lab2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            AutoResetEvent event_1 = new AutoResetEvent(false);
            byte[] buffer = new byte[1000];
            FileStream fstream = new FileStream("input.txt", FileMode.Open);

            fstream.BeginRead(buffer, 0, buffer.Length, fileRead, new object[] { fstream, buffer, event_1});
            event_1.WaitOne();

            Console.ReadLine();
        }

        public static void fileRead(IAsyncResult state)
        {
            FileStream fs = ((object[])state.AsyncState)[0] as FileStream;
            byte[] buffer = ((object[])state.AsyncState)[1] as byte[];
            AutoResetEvent event_1 = ((object[])state.AsyncState)[2] as AutoResetEvent;
            string result = System.Text.Encoding.UTF8.GetString(buffer);
            Console.WriteLine(result);
            event_1.Set();
        }
    }
}
