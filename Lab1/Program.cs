using Lab1.Rules;
using System;
using System.Collections.Generic;

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
            var minner = new Minner(blockchain);
            minner.Mine();
            Console.WriteLine("=============================");
            foreach (var b in blockchain) { 
                Console.WriteLine(b);
            }
        }
    }
}