using System.Text;

namespace AdvancedTopics.Section31
{
    public class DynamicVisitorDemoMy
    {
        public static void Main(string[] args)
        {
            Expression a = new Literal(1);
            Expression b = new Literal(2);
            Expression c = new Literal(3);
            Expression d = new Literal(4);
            Expression e = new Literal(3);
            Expression f = new Literal(4);

            Expression x1 = new BinaryOp(a, b, '+');
            Expression x2 = new BinaryOp(x1, c, '+');
            Expression x3 = new BinaryOp(x2, d, '*');

            var sb = new StringBuilder();

            // dynamic dispatching - dynamically decides to call the right Print method
            ExpressionPrinter.Print((dynamic)x3, sb);

            Console.WriteLine(sb);
        }
    }

    public abstract class Expression { }

    public class Literal(int value) : Expression
    {
        public double Value { get; } = value;
    }

    public class BinaryOp : Expression
    {
        public char Op { get; }

        public Expression Left { get; }

        public Expression Right { get; }

        public BinaryOp(Expression left, Expression right, char op)
        {
            Op = op;
            Left = left;
            Right = right;
        }
    }

    public class ExpressionPrinter
    {
        public static void Print(Literal literal, StringBuilder sb)
        {
            sb.Append(literal.Value);
        }

        public static void Print(BinaryOp binaryOp, StringBuilder sb)
        {
            sb.Append('(');
            Print((dynamic)binaryOp.Left, sb);
            sb.Append(binaryOp.Op);
            Print((dynamic)binaryOp.Right, sb);
            sb.Append(')');
        }
    }
}
