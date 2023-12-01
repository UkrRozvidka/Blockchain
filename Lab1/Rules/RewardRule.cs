using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class RewardRule : IRule
    {
        public bool IsValid(Blockchain blockchain, Block block)
        {
            if (block == null || blockchain == null) throw new ArgumentNullException();
            var firstTransaction = block.Transactions.First();
            return firstTransaction.Data.Amount == blockchain.Reward && firstTransaction.Data.From == "0x0000";
        }
    }
}
