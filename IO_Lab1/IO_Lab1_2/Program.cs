using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IO_Lab1_zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(waitingThread, new object[] { 1000 });
            Thread.Sleep(1000);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("x");
            }
            Console.ReadKey();
        }


        static void waitingThread(Object stateInfo)
        {
            var waitTime = ((object[])stateInfo)[0];
            Thread.Sleep((int)waitTime);
            Console.WriteLine(100);
        }
    }
}
