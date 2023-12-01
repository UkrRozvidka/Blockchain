using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class MemPool : IEnumerable<Transaction>
    {
        public List<Transaction> _memPool { get; private set; } = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            _memPool.Add(transaction);
        }

        public void RemoveTransaction(Transaction transaction)
        {
            _memPool.Remove(transaction);
        }

        public IEnumerator<Transaction> GetEnumerator()
        {
            return _memPool.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
