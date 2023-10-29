using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class ShowBlockchainCommand : ICommand
    {
        private readonly Runner _runner;

        public ShowBlockchainCommand(Runner runner) => _runner = runner;

        public void Execute()
        {
            if (_runner == null) throw new NullReferenceException();

            Console.WriteLine(_runner.CurentNode.Blockchain.ToString());
        }
    }
}
