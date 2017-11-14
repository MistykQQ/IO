using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_Lab1_zad5
{
    class Program
    {
        public static int NUMBERDIV = 100000;
        public static int TABLENGTH = 1000099;
        public static int[] tab = new int[TABLENGTH];
        public static int sum = 0;
        public static int rest = TABLENGTH % NUMBERDIV;
        public static int quantitypartialsum = (TABLENGTH - rest) / NUMBERDIV;
        public static int mutex = quantitypartialsum + 1;
        private static AutoResetEvent event_1 = new AutoResetEvent(true);

        public static void Main(string[] args)
        {
            // ########## Przykładowa tablica ##########
            // zapełniam jedynkami, żeby lepiej było widać czy algorytm faktycznie działa
            // przy liczbach pseudolosowych określenie tego byłoby niemożliwe
            for (int i = 0; i < TABLENGTH; i++)
            {
                tab[i] = 1;
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // ##########
            Sum();
            // ##########
            stopWatch.Stop();
            Console.WriteLine("Time elapsed: " + stopWatch.Elapsed);
            Console.ReadLine();
        }

        public static void Sum()
        {
            // ########## Algorytm ##########
            ThreadPool.QueueUserWorkItem(restSUM);

            for (int i = 0; i < quantitypartialsum; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(partialSUM), i);
            }
            while (mutex != 0)
            {
                Thread.Sleep(100);
            }
            //Console.WriteLine("quantitypartialsum " + quantitypartialsum + " |rest " + rest);
            event_1.Set();
            Console.WriteLine("suma: " + sum);
        }

        static void partialSUM(Object StateInfo)
        {
            int piece = (int)StateInfo;
            Console.WriteLine("Thread " + piece + " started");
            int tempSum = 0;
            for (int i = piece*NUMBERDIV; i < (piece + 1) * NUMBERDIV; i++)
            {
                tempSum = tempSum + tab[i];
            }
            sum = sum + tempSum;
            mutex--;
            Console.WriteLine("Thread " + piece + " finished");
            event_1.WaitOne();
        }
        static void restSUM(Object StateInfo)
        {
            int tempSum = 0;
            for (int i = quantitypartialsum*NUMBERDIV; i<TABLENGTH; i++)
            {
                tempSum = tempSum + tab[i];
            }
            event_1.WaitOne();
            mutex--;
            sum = sum + tempSum;
        }
    }
}
