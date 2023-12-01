using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class ShowBalancesCommand : ICommand
    {
        private readonly Runner _runner;

        public ShowBalancesCommand(Runner runner) => _runner = runner;

        public void Execute()
        {
            if (_runner == null) throw new NullReferenceException();

            foreach(var bal in _runner.CurentNode.Balances)
            {
                Console.WriteLine($"{bal.Key}: {bal.Value}");
            }
        }
    }
}
