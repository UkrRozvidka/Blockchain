using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Lab1
{
    public class Blockchain : IEnumerable<Block>, ICloneable
    {
        public List<Block> Chain { get; private set; }  = new();
        public List<IRule> Rules { get; private set; } 
        public readonly IHashFunction HashFunction;
        public List<Transaction> MemPool { get; private set; } = new();
        public int Dificalty { get { return 3; } }
        public int Reward { get { return 25; } }

        public delegate void BlockchainAddBlockHendler(Blockchain sender, Block e);
        public event BlockchainAddBlockHendler? OnAddBlock;

        public Blockchain(IHashFunction hashFunction, List<IRule> rules) 
        {
            this.HashFunction = hashFunction;
            Rules = new List<IRule>(rules);
        }

        public void AddBlock(Block block)
        {
            MemPool.RemoveAll(x => block.Transactions.Contains(x));
            Chain.Add(block);
            OnAddBlock?.Invoke(this, block);
        }

        public void AddTransaction(Transaction transaction)
        {
            MemPool.Add(transaction);
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return Chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            var res = "=========================\n";
            foreach (var block in Chain)
                res += block.ToString();
            return res;
        }

        public object Clone()
        {
            var clonedBlockchain = new Blockchain(HashFunction, new List<IRule>(Rules));

            for(int i = 0; i < Chain.Count; i++)
            {
                clonedBlockchain.Chain.Add((Block)Chain[i].Clone());
            }

            if (OnAddBlock != null)
            {
                foreach (var handler in OnAddBlock.GetInvocationList())
                {
                    if (handler is BlockchainAddBlockHendler blockchainAddBlockHandler)
                    {
                        clonedBlockchain.OnAddBlock += blockchainAddBlockHandler;
                    }
                }
            }

            return clonedBlockchain;
        }
    }
}
