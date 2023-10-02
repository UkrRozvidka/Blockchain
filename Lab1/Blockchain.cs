using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Lab1
{
    public class Blockchain : IBlockchain
    {
        public List<Block> Chain { get; private set; }  = new List<Block>();
        public List<IRule> Rules { get; private set; }
        public readonly IHashFunction hashFunction;

        public Blockchain(IHashFunction hashFunction, List<IRule> rules) 
        {
            var genesisHash = new String('0', 58) + "batsan";
            this.hashFunction = hashFunction;
            Rules = rules;
            this.AddBlock(new Block(0, 0, genesisHash, null));

        }

        private bool BlockValidation(Block block) // TODO: Add transaction check 
        {
           foreach(var rule in Rules)
           {
                if(!rule.IsValid(this, block)) return false;
           }
           return true;
        }

        public bool AddBlock(Block block)
        {
            if (BlockValidation(block))
            {
                Chain.Add(block);
                Console.WriteLine(block.ToString()); //TODO replace by event
                return true;
            }
            return false;
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return Chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
}
