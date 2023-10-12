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
        public readonly IHashFunction hashFunction;
        public int Dificalty { get { return 3; } }
        public int Reward { get { return 100; } }

        public delegate void BlockchainAddBlockHendler(Blockchain sender, Block e);
        public event BlockchainAddBlockHendler? OnAddBlock;

        public Blockchain(IHashFunction hashFunction, List<IRule> rules, bool IsGenesis = true) 
        {
            this.hashFunction = hashFunction;
            Rules = new List<IRule>(rules);
            if(IsGenesis)
            {
                var genesisHash = new String('0', 58) + "batsan";
                Chain.Add(new Block(0, 0, genesisHash, new()));
            }
        }

        public void AddBlock(Block block)
        {
            Chain.Add(block);
            OnAddBlock?.Invoke(this, block);
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
            var clonedBlockchain = new Blockchain(hashFunction, new List<IRule>(Rules), false);

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
