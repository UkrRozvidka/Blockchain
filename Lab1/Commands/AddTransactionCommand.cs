using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class AddTransactionCommand : ICommand
    {
        private readonly Runner _runner;

        public AddTransactionCommand(Runner runner) => _runner = runner;

        public void Execute()
        {
            if(_runner == null) throw new NullReferenceException();

            Console.WriteLine("Введіть адресу призначення:");
            var to = Console.ReadLine();

            Console.Write ("Введіть кількість монет: ");
            if (!int.TryParse(Console.ReadLine(), out int amount))
                Console.WriteLine("Було введено невалідне значення");

            Console.WriteLine("Підпишіть транзакцію приватним ключем");
            var privateKey = Console.ReadLine();

            _runner.MemPool.AddTransaction(new Transaction(_runner.CurentNode.PublicKey, to, amount, privateKey));  
        }   
    }
}
