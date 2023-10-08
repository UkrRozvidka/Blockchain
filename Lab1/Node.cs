using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Node
    {
        private readonly Blockchain _blockchain;
        public readonly string publicKey;
        public List<IValidationTransactionRule> TransactionRules { get; private set; }
        private List<Transaction> checkedTransactions = new();

        public Node(Blockchain blockchain, string publicKey, List<IValidationTransactionRule> transactionRules)
        {
            _blockchain = blockchain;
            this.publicKey = publicKey;
            TransactionRules = transactionRules;
            checkedTransactions.Insert(0, new Transaction("0x0000", publicKey, _blockchain.Reward, "0x0000"));
            _blockchain.OnAddTransaction += OnTransactionAdd;
        }
        
        public void OnTransactionAdd(Blockchain blockchain, Transaction transaction)
        {
            if(transaction != null) throw new ArgumentNullException(nameof(transaction));
            if(ValidateTransaction(transaction) && !checkedTransactions.Contains(transaction))
                checkedTransactions.Add(transaction);
        }

        private bool ValidateTransaction(Transaction transaction)
        {
            foreach (var rulle in TransactionRules)
            {
                if (!rulle.IsValid(_blockchain, transaction)) return false;
            }
            return true;
        }

        private bool ValidateBlock(Block block)
        {
            foreach (var rulle in _blockchain.Rules)
            {
                if (!rulle.IsValid(_blockchain, block)) return false;
            }
            return true;
        }

        public async Task Mine()
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                var block = new Block(_blockchain.Chain.Count, i,
                    _blockchain.hashFunction.GetHash(_blockchain.Chain.Last()), checkedTransactions);
                if (ValidateBlock(block))
                {
                    await Console.Out.WriteLineAsync($"Minner {publicKey} add block");
                    checkedTransactions.RemoveAll(transaction => block.Transactions.Contains(transaction));
                    checkedTransactions.Insert(0, new Transaction("0x0000", publicKey, _blockchain.Reward, "0x0000"));
                    _blockchain.AddBlock(block);
                }
                
            }
        }
    }
}
