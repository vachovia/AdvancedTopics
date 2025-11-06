using System.Numerics;

namespace AdvancedTopics.Section61
{
    public enum WorkflowResult
    {
        Success,
        Failure
    }

    public class QuadraticEquationSolver
    {
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            var disc = Math.Pow(b, 2) - 4*a*c;

            if(disc < 0)
            {
                result = null;
                return WorkflowResult.Failure;
            }

            return SolveReal(a, b, c, disc, out result);
        }

        private WorkflowResult SolveReal(double a, double b, double c, double disc, out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);

            result = Tuple.Create(
                new Complex((-b + rootDisc) / (2 * a), 0),
                new Complex((-b - rootDisc) / (2 * a), 0)
            );

            return WorkflowResult.Success;
        }

        private Tuple<Complex, Complex> SolveComplex(double a, double b, double c, double disc)
        {
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));

            return Tuple.Create(
                (-b + rootDisc) / (2 * a),
                (-b - rootDisc) / (2 * a)
            );
        }
    }

    public class ContinuationPassingStyleImprooved
    {
        public static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            Tuple<Complex, Complex> result1, result2;
            var flag1 = solver.Start(1, -3, 2, out result1); // Real roots
            // var flag2 = solver.Start(1, 2, 5, out result2);  // Complex roots

            Console.WriteLine($"Real Roots: {result1.Item1}, {result1.Item2}");
            // Console.WriteLine($"Complex Roots: {result2.Item1}, {result2.Item2}");
        }
    }
}
