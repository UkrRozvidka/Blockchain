using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp {  get; set; }
        public int Nonce { get; set; }
        public string PrevHash { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(int index, int nonce, string prevHash, List<Transaction> transactions)
        {
            Index = index;
            TimeStamp = DateTime.Now;
            Nonce = nonce;
            PrevHash = prevHash;
            Transactions = transactions;
        }

        public override string ToString()
        {
            return $"Index: {Index}; Nonce: {Nonce}; {TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}; " +
                $"{PrevHash};";
        }

    }
}
