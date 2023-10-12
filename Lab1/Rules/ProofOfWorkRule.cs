using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class ProofOfWorkRule : IRule
    {
        public bool IsValid(Blockchain blockchain, Block block)
        {
            if (block == null || blockchain == null) throw new ArgumentNullException();
            var res = blockchain.hashFunction.GetHash(block).StartsWith(new string('0', blockchain.Dificalty));
            return res;
        }   
    }
}
