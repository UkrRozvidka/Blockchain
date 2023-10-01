using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Lab1
{
    class Blockchain : IBlockchain
    {
        private List<Block> _chain = new List<Block>();
        private readonly IHashFunction _hashFunction;

        public Blockchain(IHashFunction hashFunction) 
        {
            var genesisHash = new String('0', 64);
            _hashFunction = hashFunction;
            _chain.Add(new Block(0, 0, genesisHash, null));
        }

        private bool BlockValidation(Block block) // TODO: Add transaction check 
        {
            var tail = _chain.LastOrDefault();
            var tailHash = _hashFunction.GetHash(JsonSerializer.Serialize(tail));
            if (block.PrevHash == tailHash && block.Index == _chain.Count)
               return true;
            return false;
        }

        public void AddBlock(Block block)
        {
            if (BlockValidation(block)) _chain.Add(block);
            else throw new ApplicationException("block is not valid");
        }

        public void AddBlock(int nonce, List<Transaction> transactions) 
        {
            var prevHash = _hashFunction.GetHash(JsonSerializer.Serialize(_chain.LastOrDefault()));
            _chain.Add(new Block(_chain.Count, nonce, prevHash, transactions));
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return _chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
}
