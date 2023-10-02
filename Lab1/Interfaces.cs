using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public interface IBlockchain : IEnumerable<Block>
    {
        bool AddBlock(Block block);
    }

    public interface IHashFunction
    {
        string GetHash(Block block);
    }

    public interface IRule
    {
        bool IsValid(Blockchain blockchain, Block block);
    }
}
