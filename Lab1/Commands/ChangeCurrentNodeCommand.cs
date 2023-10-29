using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class ChangeCurrentNodeCommand : ICommand
    {
        private Runner runner;

        public ChangeCurrentNodeCommand(Runner runner) => this.runner = runner;

        public void Execute()
        {
            if (runner.CurentNode == null) throw new NullReferenceException();

            for(int i = 0; i < runner.CurentNode.Nodes.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + runner.CurentNode.Nodes[i].PublicKey);
            }
            Console.Write("Введіть номер ноди на яку хочете перейти: ");
            if (!int.TryParse(Console.ReadLine(), out int n) && n > 1 && n < runner.CurentNode.Nodes.Count  )
                Console.WriteLine("Було введено невалідне значення");
            else
                runner.CurentNode = runner.CurentNode.Nodes[n-1];
        }
    }
}
