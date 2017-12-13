using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_Lab2_zad3
{
    class Program
    {
        delegate string DelegateType(Object argumets);
        static DelegateType delegateFibIter;
        static DelegateType delegateFibRec;
        static DelegateType delegateFacIter;
        static DelegateType delegateFacRec;
        static void Main(string[] args)
        {
            int number = 10;
            Console.Write("n - {0} \n", number);

            //BEGININVOKE
            delegateFibIter = new DelegateType(FibIter);
            IAsyncResult ar = delegateFibIter.BeginInvoke(number, null, null);
            delegateFibRec = new DelegateType(FibRec);
            IAsyncResult ar2 = delegateFibRec.BeginInvoke(number, null, null);

            delegateFacIter = new DelegateType(FacIter);
            IAsyncResult ar3 = delegateFacIter.BeginInvoke(number, null, null);
            delegateFacRec = new DelegateType(FacRec);
            IAsyncResult ar4 = delegateFacRec.BeginInvoke(number, null, null);

            //ENDINVOKE
            string result1 = delegateFibIter.EndInvoke(ar);
            string result2 = delegateFibRec.EndInvoke(ar2);
            string result3 = delegateFacIter.EndInvoke(ar3);
            string result4 = delegateFacRec.EndInvoke(ar4);

            Console.WriteLine("Fibonacci Iterative - " + result1);
            Console.WriteLine("Fibonacci Recursive - " + result2);

            Console.WriteLine("Factorial Iterative - " + result3);
            Console.WriteLine("Factorial Recursive - " + result4);

            Console.ReadLine();
        }

        public static string FacIter(object arguments)
        {
            int number = (int)arguments;
            int resultINT = 1;
            if (number == 0) resultINT = 1;
            else
            {
                while (number > 0)
                {
                    resultINT *= number;
                    number--;
                }
            }
            string result = resultINT.ToString();
            return result;
        }

        public static string FacRec(object arguments)
        {
            int number = (int)arguments;
            int resultINT = 1;
            if (number < 2)
                resultINT = 1;
            else
            {
                resultINT = number * Int32.Parse(FacRec(number - 1));
            }
            string result = resultINT.ToString();
            return result;
        }

        public static string FibIter(object arguments)
        {
            int number = (int)arguments;
            int a = 0, b = 1, c = 0;
           // Console.Write("{0} {1}", a, b);

            for (int i = 2; i <= number; i++)
            {
                c = a + b;
              //  Console.Write(" {0}", c);
                a = b;
                b = c;
            }
            string result = c.ToString();
            return result;
        }

        public static string FibRec(object arguments)
        {
            int number = (int)arguments;
            int resultINT = 1;
            if (number <= 2)
            {
                resultINT = 1;
            }
            else
            {
                resultINT = Int32.Parse(FibRec(number - 1)) + Int32.Parse(FibRec(number - 2));
            }
            string result = resultINT.ToString();
            return result;
        }
    }
}
