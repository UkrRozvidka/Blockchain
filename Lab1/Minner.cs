using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Minner
    {
        private readonly Blockchain _blockchain;
        public Minner(Blockchain blockchain)
        {
            _blockchain = blockchain;
        }

        public void Mine()
        {
            int n = 0;
            for(int i = 0; i < int.MaxValue; i++)
            {
                var block = new Block(_blockchain.Chain.Count, i, 
                    _blockchain.hashFunction.GetHash(_blockchain.Chain.LastOrDefault()), null);
                if (_blockchain.AddBlock(block))
                {
                    i = 0;
                    n++;
                    if (n == 5) return;
                }
            }
        }
    }
}
