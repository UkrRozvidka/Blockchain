using Lab1.Rules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using System.Text;

namespace Lab1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var runner = new Runner();
            runner.Run();


            //var e = new Encrypt();
            //while (true)
            //{
            //    var keys = e.GenerateKeyPair();
            //    Console.WriteLine(keys.PublicKey);
            //    Console.WriteLine(keys.PrivateKey);
            //    Console.ReadKey();
            //}
        }
    }
}