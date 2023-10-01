using System;
using System.Collections.Generic;

namespace Lab1
{
    class Programm
    {
        public static void Main(string[] args)
        {
            var q = new LinkedList<string>();
            var blockchain = new Blockchain(new SHA256Hash());
            blockchain.AddBlock(0, null);
            blockchain.AddBlock(0, null);
            blockchain.AddBlock(0, null);
            blockchain.AddBlock(0, null);
            blockchain.AddBlock(0, null);
            blockchain.AddBlock(0, null);

            foreach(var block in blockchain)
            {
                Console.WriteLine($"{block.Index} {block.Nonce} {block.PrevHash} " +
                    $"{block.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}");

            }
        }
    }
}