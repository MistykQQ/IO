using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IO_Lab3_zad
{
    class Program
    {
        public static async Task<XmlDocument> Zadanie3(string adress)
        {
            WebClient webClient = new WebClient();
            string xmlContent = await webClient.DownloadStringTaskAsync(new Uri(adress));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            return doc;
        }

        static void Main(string[] args)
        {
            var test = Zadanie3("„http://www.feedforall.com/sample.xml");
            //Console.Write(test);
            Console.ReadLine();
        }

        public bool Z2 = false;

        public void Zadanie2()
        {
            //ZADANIE 2. ODKOMENTUJ I POPRAW
            //return.Task.Run(
            //() =>
           // {
            //Z2 = true;
            //});
            
        }

    }

    class ResultData
    {
        int a;
        public int A
        {
            get { return a; }
            set { a = value; }
        }
        int b;
        public int B
        {
            get { return a; }
            set { b = value; }
        }
    }
}
