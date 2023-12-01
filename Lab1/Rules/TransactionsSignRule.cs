using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class TransactionsSignRule : IValidationTransactionRule
    {
        public bool IsValid(Node node, Transaction transaction)
        {
            if(transaction == null) throw new ArgumentNullException();
            var encryptor = new Encrypt();
            var data = JsonSerializer.Serialize(transaction.Data);
            if (!encryptor.VerifySign(data, transaction.Data.From, transaction.Sign))
                return false;
            return true;
        }
    }
}
