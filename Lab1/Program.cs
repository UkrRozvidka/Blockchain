using Lab1.Rules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1
{
    class Programm
    {
        public static void Main(string[] args)
        {
            var rules = new List<IRule>();
            rules.Add(new IndexRule());
            rules.Add(new PrevHashRule());
            rules.Add(new ProofOfWorkRule());
            var blockchain = new Blockchain(new SHA256Hash(), rules);
            var minner1 = new Node(blockchain, "1", 
                new List<IValidationTransactionRule>());
            var minner2 = new Node(blockchain, "2",
                new List<IValidationTransactionRule>());
            var minner3 = new Node(blockchain, "3",
                new List<IValidationTransactionRule>());

            Task task1 = Task.Run(() => minner1.Mine());
            Task task2 = Task.Run(() => minner2.Mine());
            Task task3 = Task.Run(() => minner3.Mine());


            Task.WaitAll(task1, task2, task3);

            //var e = new Encrypt();
            //var keys = e.GenerateKeyPair();
            //var keys2  = e.GenerateKeyPair();
            //Console.WriteLine(keys.PublicKey + '\n' + keys.PrivateKey);

            //string s = "hello world";
            //string signdata = e.SignData(s, keys2.PrivateKey);
            //Console.WriteLine(e.VerifySign(s, keys.PublicKey, signdata));
        }
    }
}