using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class SyncBlockchainCommand : ICommand
    {
        private readonly Runner _runner;

        public SyncBlockchainCommand(Runner runner) => _runner = runner;

        public void Execute()
        {
            if (_runner == null) throw new NullReferenceException();
            _runner.CurentNode.SyncBlockchain();
            Console.WriteLine("Блокчейни синхронізовано.");
        }
    }
}