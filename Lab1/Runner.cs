using Lab1.Commands;
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

        public Node CurentNode;

        public Runner(Node node)
        {
            CurentNode = node;
            CurentNode.MineBlock();
            commandDictionary = new Dictionary<CommandEnum, ICommand>()
            {
                { CommandEnum.AddTransaction, new AddTransactionCommand(this)},
                { CommandEnum.AddBlock, new AddBlockCommand(this)},
                { CommandEnum.AddNode, new AddNodeCommand(this)},
                { CommandEnum.ChangeCurrentNode, new ChangeCurrentNodeCommand(this)},
                { CommandEnum.ShowBlockChain, new ShowBlockchainCommand(this)},
                { CommandEnum.ShowLastBLock, new ShowLastBlockCommand(this)}
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
                Console.WriteLine("5. Вивести весь блокчейн");
                Console.WriteLine("6. вивести останній блок");
                Console.WriteLine("7. Вихід з програми");
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
