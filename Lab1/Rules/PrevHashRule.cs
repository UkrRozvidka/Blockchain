using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class PrevHashRule : IRule
    {
        bool IRule.IsValid(Blockchain blockchain, Block block)
        {
            if (block == null || blockchain == null) throw new ArgumentNullException();
            var lastblockhash = blockchain.hashFunction.GetHash(blockchain.Chain.LastOrDefault());
            var res = block.PrevHash == lastblockhash;
            return res;
        }
    }
}
