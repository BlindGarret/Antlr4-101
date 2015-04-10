using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Calculator.Visitors;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input equations: ");
            var input = new StringBuilder();
            while (true)
            {
                var temp = Console.ReadLine();
                if (string.IsNullOrEmpty(temp))
                {
                    input.Append(Environment.NewLine);
                    break;
                }
                input.Append(temp);
                input.Append(Environment.NewLine);
            }

            var stream = new AntlrInputStream(input.ToString());
            var lexer = new exprLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new exprParser(tokens);
            var tree = parser.prog();

            var visitor = new EvalVisitor();
            visitor.Visit(tree);

            Console.ReadKey();
        }
    }
}
