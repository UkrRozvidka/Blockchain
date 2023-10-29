using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class ShowLastBlockCommand : ICommand
    {
        private readonly Runner _runner;

        public ShowLastBlockCommand(Runner runner) => _runner = runner;  

        public void Execute()
        {
            if (_runner == null) throw new NullReferenceException();

            Console.WriteLine(_runner.CurentNode.Blockchain.Chain.LastOrDefault().ToString());
        }
    }
}
