using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Lab1
{
    public class Blockchain : IEnumerable<Block>
    {
        public List<Block> Chain { get; private set; }  = new();
        public List<Transaction> MemPool { get; private set; } = new();
        public Dictionary<string, int> Balances { get; private set; } = new();
        public List<IRule> Rules { get; private set; }
        public readonly IHashFunction hashFunction;
        public int Dificalty { get { return 4; } }
        public int Reward { get { return 100; } }

        public delegate void BlockchainAddBlockHendler(Blockchain sender, Block e);
        public delegate void BlockchainAddTransactionHendler(Blockchain sender, Transaction e);

        public event BlockchainAddBlockHendler? OnAddBlock;
        public event BlockchainAddTransactionHendler? OnAddTransaction;

        public Blockchain(IHashFunction hashFunction, List<IRule> rules) 
        {
            var genesisHash = new String('0', 58) + "batsan";
            this.hashFunction = hashFunction;
            Rules = rules;
            Chain.Add(new Block(0, 0, genesisHash, new()));
        }

        public void AddBlock(Block block)
        {
            Chain.Add(block);
            UpdateBalances(block);
            MemPool.RemoveAll(transaction => block.Transactions.Contains(transaction));
            OnAddBlock?.Invoke(this, block);
        }

        public void AddToMemPool(Transaction transaction)
        {
            MemPool.Add(transaction);
            OnAddTransaction?.Invoke(this, transaction);
        }

        public void RemoveFromMemPool(Transaction transaction) => MemPool.Remove(transaction);

        private void UpdateBalances(Block block)
        {
            if(block == null) throw new ArgumentNullException(nameof(block));
            if(block.Transactions == null) return;
            foreach (var transaction in block.Transactions)
            {
                Balances[transaction.Data.From] = Balances.GetValueOrDefault(transaction.Data.From, 0) - transaction.Data.Amount;
                Balances[transaction.Data.To] = Balances.GetValueOrDefault(transaction.Data.To, 0) + transaction.Data.Amount;
            }
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return Chain.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
