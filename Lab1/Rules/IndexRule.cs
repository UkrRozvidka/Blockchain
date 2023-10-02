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

            return block.Index == blockchain.Chain.Count;
        }
    }
}
