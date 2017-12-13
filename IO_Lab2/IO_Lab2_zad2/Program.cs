using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace IO_Lab2_zad2
{
    class Program
    {
        delegate string DelegateType(Object argumets);
        static DelegateType delegateName;

        static void Main(string[] args)
        {
            //AutoResetEvent event_1 = new AutoResetEvent(false);
            delegateName = new DelegateType(fileRead);
            IAsyncResult ar = delegateName.BeginInvoke("argument", null, null);
            string result = delegateName.EndInvoke(ar);


            Console.WriteLine("main");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static string fileRead(object arguments)
        {
            Console.WriteLine("invoke");
            //byte[] buffer = new byte[1000];
            //FileStream fstream = new FileStream("input.txt", FileMode.Open);
            string result = File.ReadAllText("input.txt");
            //string result = System.Text.Encoding.UTF8.GetString(buffer);
            //Console.WriteLine(result);
            return result;
        }
    }
}
