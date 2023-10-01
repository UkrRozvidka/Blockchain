using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    interface IBlockchain : IEnumerable<Block>
    {
        void AddBlock(Block block);
    }

    interface IHashFunction
    {
        string GetHash(string data);
    }
}
