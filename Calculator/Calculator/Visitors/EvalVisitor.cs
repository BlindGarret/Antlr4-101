using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Visitors
{
    class EvalVisitor : exprBaseVisitor<int>
    {
        private Dictionary<string, int> _memory = new Dictionary<string, int>(); 

        // ID = expr NEWLINE
        public override int VisitAssign(exprParser.AssignContext context)
        {
            var id = context.ID().GetText(); // id is left-hand side of '='
            var value = Visit(context.expr());  // compute value of expression on right
            _memory.Add(id, value);
            return value;
        }

        // expr NEWLINE
        public override int VisitPrintExpr(exprParser.PrintExprContext context)
        {
            var value = Visit(context.expr());
            Console.WriteLine(value);
            return 0;
        }

        // INT
        public override int VisitInt(exprParser.IntContext context)
        {
            return int.Parse(context.INT().GetText());
        }

        // ID
        public override int VisitId(exprParser.IdContext context)
        {
            var id = context.ID().GetText();
            if (_memory.ContainsKey(id))
            {
                return _memory[id];
            }
            return 0;
        }

        // expr *|/ expr
        public override int VisitMulDiv(exprParser.MulDivContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));
            if (context.op.Type == exprParser.MUL)
            {
                return left*right;
            }
            return left/right;
        }

        // expr +|- expr
        public override int VisitAddSub(exprParser.AddSubContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));
            if (context.op.Type == exprParser.ADD)
            {
                return left + right;
            }
            return left - right;
        }

        // ( expr )
        public override int VisitParens(exprParser.ParensContext context)
        {
            return Visit(context.expr());
        }

        // clear
        public override int VisitClear(exprParser.ClearContext context)
        {
            _memory.Clear();
            return 0;
        }
    }
}
