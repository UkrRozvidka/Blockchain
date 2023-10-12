using Lab1.Rules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace Lab1
{
    class Program
    {
        public static void Main(string[] args)
        {
            var rules = new List<IRule>();
            rules.Add(new IndexRule());
            rules.Add(new PrevHashRule());
            rules.Add(new ProofOfWorkRule());
            var blockchain = new Blockchain(new SHA256Hash(), rules);
            var nodes = new List<Node>();
            var mainnode = new Node((Blockchain)blockchain.Clone(), "9999", new List<IValidationTransactionRule>(), nodes);


            for (int i = 0; i < 4; i++)
            {
                var node = new Node((Blockchain)mainnode.Blockchain.Clone(), i.ToString(), new List<IValidationTransactionRule>(), nodes);
                node.Mine();
            }

            foreach (var node in nodes)
            {
                Console.WriteLine(node.Blockchain.ToString());
            }

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