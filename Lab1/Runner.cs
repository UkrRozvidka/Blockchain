using Lab1.Commands;
using Lab1.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Runner
    {
        private Dictionary<CommandEnum, ICommand> commandDictionary;

        public MemPool MemPool;

        public Node CurentNode;

        public Runner()
        {
            MemPool = new MemPool();
            var rules = new List<IRule> { new PrevHashRule(), new ProofOfWorkRule(), new IndexRule(), new RewardRule()};
            var trules = new List<IValidationTransactionRule> { new TransactionsSignRule(), new BalanceRule() };
            var blockchain = new Blockchain(new SHA256Hash(), rules, MemPool);
            var publicKey = "BgIAAACkAABSU0ExAAIAAAEAAQBBxGzUEmTO1t1YMzXhBEP3kyYNeqr3SIIuMXIrxFC5DTPuCFUT1IelcYdcndYSxUFjIKtHkutD+3OlI1PX9ied";
            
            CurentNode = new Node(blockchain, publicKey, trules, new List<Node>());
            CurentNode.MineBlock();
            commandDictionary = new Dictionary<CommandEnum, ICommand>()
            {
                { CommandEnum.AddTransaction, new AddTransactionCommand(this)},
                { CommandEnum.AddBlock, new AddBlockCommand(this)},
                { CommandEnum.AddNode, new AddNodeCommand(this)},
                { CommandEnum.ChangeCurrentNode, new ChangeCurrentNodeCommand(this)},
                { CommandEnum.ShowBalances, new ShowBalancesCommand(this)},
                { CommandEnum.ShowBlockChain, new ShowBlockchainCommand(this)},
                { CommandEnum.ShowLastBLock, new ShowLastBlockCommand(this)},
                { CommandEnum.SyncBlockchain, new SyncBlockchainCommand(this)},
            };
        }
        public void Run()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Додати транзакцію");
                Console.WriteLine("2. Додати блок");
                Console.WriteLine("3. Додати ноду");
                Console.WriteLine("4. Змінити поточну ноду");
                Console.WriteLine("5. Вивести баланси гаманців");
                Console.WriteLine("6. Вивести весь блокчейн");
                Console.WriteLine("7. Bивести останній блок");
                Console.WriteLine("8. Синхронізувати блокчейн");
                Console.WriteLine("9. Вихід з програми");
                Console.ForegroundColor = ConsoleColor.White;

                int.TryParse(Console.ReadLine(), out int choice);
                var commandInvoker = new CommandInvoker();

                if (commandDictionary.TryGetValue((CommandEnum)choice, out ICommand selectedCommand))
                {
                    commandInvoker.SetCommand(selectedCommand);
                    try
                    {
                        commandInvoker.ExecuteCommand();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Щось пішло не так((");
                    }
                }
                else if (choice == 7)
                {
                    Console.WriteLine("Вихід!!!");
                    break;
                }
                else
                {
                    Console.WriteLine("Невірна команда!!!");
                }
            }
        }
    }
}
