using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class IndexRule : IRule
    {
        public bool IsValid(Blockchain blockchain, Block block)
        {
            if (block == null || blockchain == null) throw new ArgumentNullException();

            if(block.Index == 0) return true;
            return block.Index == blockchain.Chain[block.Index - 1].Index + 1;
        }
    }
}
