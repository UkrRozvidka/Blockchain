using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public interface IHashFunction
    {
        string GetHash(Block block);
    }

    public interface IRule
    {
        bool IsValid(Blockchain blockchain, Block block);
    }

    public interface IValidationTransactionRule
    {
        bool IsValid(Node node, Transaction transaction);
    }
}
