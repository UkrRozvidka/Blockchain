using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Block
    {
        public int Index { get; private set; }
        public DateTime TimeStamp {  get; private set; }
        public int Nonce { get; private set; }
        public string PrevHash { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Block(int index, int nonce, string prevHash, List<Transaction> transactions)
        {
            Index = index;
            TimeStamp = DateTime.Now;
            Nonce = nonce;
            PrevHash = prevHash;
            Transactions = transactions;
        }

    }
}
