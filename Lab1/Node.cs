using Lab1.Rules;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab1
{
    public class Node
    {
        public Blockchain Blockchain { get; private set; }
        public readonly string PublicKey;
        public List<IValidationTransactionRule> TransactionRules { get; private set; }
        public Dictionary<string, int> Balances { get; private set; } = new();
        public List<Node> Nodes { get; private set; }


        public Node(Blockchain blockchain, string publicKey, List<IValidationTransactionRule> transactionRules, List<Node> otherNodes)
        {
            Blockchain = blockchain;
            this.PublicKey = publicKey;
            TransactionRules = transactionRules;
            Nodes = otherNodes;
            Nodes.Add(this);
            RecalculateBalances();
        }

        //public void AddTransaction(Transaction transaction)
        //{
        //    foreach (var node in Nodes)
        //    {
        //        node.Blockchain.MemPool.AddTransaction(transaction);
        //    }
        //}

        private void SyncTransaction(List<Transaction> blockTransactions)
        {
            blockTransactions.Clear();
            foreach (var transaction in Blockchain.MemPool)
            {
                if (ValidateTransaction(transaction) && blockTransactions.TrueForAll(x => x.Data.From != transaction.Data.From))
                {
                    blockTransactions.Add(transaction);
                }
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

        public void MineBlock()
        {
            Block block;
            if (Blockchain.Chain.Count == 0)
                block = FindGenesisBlock();
            else
                block = FindCorrectBlockHash();
            Blockchain.AddBlock(block);
            UpdateBalances(block);
            
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

        private void RecalculateBalances()
        {
            Balances.Clear();
            foreach (var block in Blockchain)
                UpdateBalances(block);
        }

        private Block FindCorrectBlockHash()
        {
            int nonce = 0;
            var block = new Block(Blockchain.Chain.Count, nonce, Blockchain.HashFunction.GetHash(Blockchain.Chain.Last()), new List<Transaction>());
            while (true)
            {
                SyncTransaction(block.Transactions);
                block.Transactions.Insert(0, new Transaction("0x0000", PublicKey, Blockchain.Reward, null));
                if (ValidateBlock(block)) 
                    return block;
                block.Nonce = ++nonce;
                block.TimeStamp = DateTime.UtcNow;
            }
        }

        private Block FindGenesisBlock()
        {
            int nonce = 25112003;
            var genesisHash = new String('0', 58) + "batsan";
            var block = new Block(0, nonce, genesisHash, new List<Transaction>());
            block.Transactions.Insert(0, new Transaction("0x0000", PublicKey, Blockchain.Reward, null));
            while (true)
            {

                var pow = new ProofOfWorkRule();
                if (pow.IsValid(Blockchain, block)) return block;
                block.Nonce = ++nonce;
                block.TimeStamp = DateTime.UtcNow;
                if (nonce == int.MaxValue) nonce = 0;
            }
        }

        private bool ValidateBlockchain(Blockchain blockchain)
        {
            foreach (var block in blockchain)
            {
                if (!ValidateBlock(block)) return false;
                for (int i = 1; i < block.Transactions.Count; i++)
                {
                    if (!ValidateTransaction(block.Transactions[i])) return false;
                }
            }
            return true;
        }

        public void SyncBlockchain()
        {
            int maxLenght = Blockchain.Chain.Count;
            Blockchain longestValidChain = Blockchain;
            foreach (var node in Nodes)
            {
                var otherBlockchain = node.Blockchain;
                if(otherBlockchain.Chain.Count > maxLenght && ValidateBlockchain(otherBlockchain))
                {
                    maxLenght = otherBlockchain.Chain.Count;
                    longestValidChain = otherBlockchain;
                }
            }

            foreach (var node in Nodes)
            {
                for(int i = 0; i < node.Blockchain.Count(); i++)
                {
                    if (node.Blockchain.HashFunction.GetHash(node.Blockchain[i]) != node.Blockchain.HashFunction.GetHash(longestValidChain[i]))
                    {
                        for(int j = i; j < node.Blockchain.Count(); j++)
                        {
                            for(int t = 1; t < node.Blockchain[j].Transactions.Count; t++)
                            {
                                node.Blockchain.MemPool.AddTransaction(node.Blockchain[j].Transactions[t]);
                            }
                        }
                    }
                }
            }

            foreach(var node in Nodes)
            {
                node.Blockchain = (Blockchain)longestValidChain.Clone();
                node.RecalculateBalances();
            }
        }
    }
}
