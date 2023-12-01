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
        public MemPool MemPool;
        public int Dificalty { get { return 3; } }
        public int Reward { get { return 25; } }

        public delegate void BlockchainAddBlockHendler(Blockchain sender, Block e);
        public event BlockchainAddBlockHendler? OnAddBlock;

        public Blockchain(IHashFunction hashFunction, List<IRule> rules, MemPool memPool) 
        {
            this.HashFunction = hashFunction;
            Rules = new List<IRule>(rules);
            MemPool = memPool;
        }

        public void AddBlock(Block block)
        {
            foreach(Transaction transaction in block.Transactions)
            {
                MemPool.RemoveTransaction(transaction);
            }
            Chain.Add(block);
            OnAddBlock?.Invoke(this, block);
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return Chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Block this[int index]
        {
            get
            {
                return Chain[index];
            }
        }

        public override string ToString()
        {
            var res = "=========================\n";
            foreach (var block in Chain)
                res += block.ToString();
            return res;
        }

        public object Clone()
        {
            var clonedBlockchain = new Blockchain(HashFunction, new List<IRule>(Rules), MemPool);

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
