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
            if(block.Index < 1) return true;
            var lastblockhash = blockchain.HashFunction.GetHash(blockchain.Chain[block.Index - 1]);
            return block.PrevHash == lastblockhash;
        }
    }
}
