using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab1.Commands
{
    public class AddNodeCommand : ICommand
    {
        private readonly Runner _runner;

        public AddNodeCommand(Runner runner) => _runner = runner;

        public void Execute()
        {
            if (_runner == null) throw new NullReferenceException();
            Console.WriteLine("Згенеруйте пару ключів та введіть публічний: ");
            string publicKey = Console.ReadLine();

            var node = new Node((Blockchain)_runner.CurentNode.Blockchain.Clone(), publicKey, _runner.CurentNode.TransactionRules, _runner.CurentNode.Nodes);
        }
    }
}
