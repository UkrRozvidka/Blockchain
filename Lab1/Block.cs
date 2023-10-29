using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Block : ICloneable
    {
        public int Index { get; set; }
        public DateTime TimeStamp {  get; set; }
        public int Nonce { get; set; }
        public string PrevHash { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(int index, int nonce, string prevHash, List<Transaction> transactions, DateTime timeStamp = default)
        {
            Index = index;
            Nonce = nonce;
            PrevHash = prevHash;
            Transactions = transactions;
            TimeStamp = timeStamp;
            if(TimeStamp == default) TimeStamp = DateTime.UtcNow;
        }

        public override string ToString()
        {
            var sha256 = new SHA256Hash();
            string res = $"Index: {Index}; Nonce: {Nonce}; {TimeStamp:yyyy-MM-dd HH:mm:ss.fffffff}; " +
                $"{PrevHash}; {sha256.GetHash(this)}  \n";
            if( Transactions.Count > 0 )
            {
               foreach( Transaction t in Transactions )
               {
                    res +=t.ToString()+ '\n';
               }
            }
            return res ;
        }

        public object Clone()
        {
            return new Block(Index, Nonce, PrevHash, new List<Transaction>(Transactions), TimeStamp);
        }
    }
}
