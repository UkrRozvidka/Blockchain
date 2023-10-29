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
            var rules = new List<IRule>();
            rules.Add(new PrevHashRule());
            rules.Add(new ProofOfWorkRule());
            rules.Add(new IndexRule());
            rules.Add(new RewardRule());
            var trules = new List<IValidationTransactionRule>();
            trules.Add(new TransactionsSignRule());
            trules.Add(new BalanceRule());
            var blockchain = new Blockchain(new SHA256Hash(), rules);
            var publicKey = "BgIAAACkAABSU0ExAAIAAAEAAQBBxGzUEmTO1t1YMzXhBEP3kyYNeqr3SIIuMXIrxFC5DTPuCFUT1IelcYdcndYSxUFjIKtHkutD+3OlI1PX9ied";
            var runner = new Runner(new Node(blockchain, publicKey, trules, new List<Node>()));
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