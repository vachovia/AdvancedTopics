using System.Text;

namespace AdvancedTopics.Section3
{
    public class DynamicVisitorDemo
    {
        public static void Main(string[] args)
        {
            //var e = new Addition(
            //    new Addition(new Literal(1), new Literal(2)),
            //    new Literal(3)
            //); // if we use Expression e then dynamic cast is required
            Expression e = new Addition(
                new Addition(new Literal(1), new Literal(2)),
                new Literal(3)
            );

            var sb = new StringBuilder();
            ExpressionPrinter.Print((dynamic)e, sb); // dynamic dispatching
            Console.WriteLine(sb);
        }
    }

    public abstract class Expression
    {

    }

    public class Literal : Expression
    {
        public double Value { get; }

        public Literal(int value)
        {
            Value = value;
        }
    }

    public class Addition : Expression
    {
        public Expression Left { get; }

        public Expression Right { get; }

        public Addition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class ExpressionPrinter
    {
        // We don't want to go this way because it requires modification every time
        //public static void Print(Expression e, StringBuilder sb)
        //{
        //    if(e.GetType() == typeof(Literal)) Print((Literal)e, sb);
        //    else if(e.GetType() == typeof(Addition)) Print((Addition)e, sb);
        //}

        public static void Print(Literal literal, StringBuilder sb)
        {
            sb.Append(literal.Value);
        }

        public static void Print(Addition addtition, StringBuilder sb)
        {
            sb.Append('(');
            Print((dynamic)addtition.Left, sb);
            sb.Append('+');
            Print((dynamic)addtition.Right, sb);
            sb.Append(')');
        }
    }
}
