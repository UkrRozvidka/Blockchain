using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab1
{
    public class Node
    {
        public readonly Blockchain Blockchain;
        public readonly string publicKey;
        public List<IValidationTransactionRule> TransactionRules { get; private set; }
        public List<Transaction> MemPool { get; private set; } = new();
        public Dictionary<string, int> Balances { get; private set; } = new();
        public List<Node> Nodes { get; private set; }



        public Node(Blockchain blockchain, string publicKey, List<IValidationTransactionRule> transactionRules, List<Node> otherNodes)
        {
            Blockchain = blockchain;        
            this.publicKey = publicKey;
            TransactionRules = transactionRules;
            Nodes = otherNodes;
            Nodes.Add(this);
            MemPool.Insert(0, new Transaction("0x0000", publicKey, Blockchain.Reward, "0x0000"));          
        }
        
        public void AddTransaction(Blockchain blockchain, Transaction transaction)
        {
            if(transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (ValidateTransaction(transaction) && MemPool.TrueForAll(x => x.Data.From != transaction.Data.From))
            {
                MemPool.Add(transaction);
            }
        }

        public void AddBlock(Block block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if(ValidateBlock(block))
            {
                Blockchain.AddBlock(block);
                UpdateBalances(block);
                MemPool.Clear();
                MemPool.Insert(0, new Transaction("0x0000", publicKey, Blockchain.Reward, "0x0000"));
            }
        }

        private bool ValidateTransaction(Transaction transaction)
        {
            foreach (var rulle in TransactionRules)
            {
                if (!rulle.IsValid(this, transaction)) return false;
            }
            return true;
        }

        private bool ValidateBlock(Block block)
        {
            foreach (var rulle in Blockchain.Rules)
            {
                if (!rulle.IsValid(Blockchain, block)) return false;
            }
            return true;
        }

        private void UpdateBalances(Block block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (block.Transactions == null) return;
            foreach (var transaction in block.Transactions)
            {
                Balances[transaction.Data.From] = Balances.GetValueOrDefault(transaction.Data.From, 0) - transaction.Data.Amount;
                Balances[transaction.Data.To] = Balances.GetValueOrDefault(transaction.Data.To, 0) + transaction.Data.Amount;
            }
        }



        public void Mine()
        {
            int count = 0;
            int nonce = 0;
            var block = new Block(Blockchain.Chain.Count, nonce, Blockchain.hashFunction.GetHash(Blockchain.Chain.Last()), new List<Transaction>(MemPool));
            while(count < 5)
            {
                if (ValidateBlock(block))
                {
                    Console.Out.WriteLineAsync($"Minner {publicKey} add block");
                    foreach (var node in Nodes)
                    {
                        node.AddBlock(block);
                    }
                    count++;
                    block = new Block(Blockchain.Chain.Count, nonce, Blockchain.hashFunction.GetHash(Blockchain.Chain.Last()), new List<Transaction>(MemPool));
                }
                block.Nonce = ++nonce;
                block.TimeStamp = DateTime.UtcNow; 
            }
        }
    }
}
