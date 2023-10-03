using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class ProofOfWorkRule : IRule
    {
        public bool IsValid(Blockchain blokchain, Block block)
        {
            if (block == null || blokchain == null) throw new ArgumentNullException();
            return blokchain.hashFunction.GetHash(block).StartsWith(new string('0', 4));
        }   
    }
}
