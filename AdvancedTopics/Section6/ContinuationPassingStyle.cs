using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.Section6
{
    public class QuadraticEquationSolver
    {
        public Tuple<Complex, Complex> Start(double a, double b, double c)
        {
            var disc = Math.Pow(b, 2) - 4*a*c;

            if(disc < 0)
            {
                return SolveComplex(a, b, c, disc);
            }

            return SolveReal(a, b, c, disc);
        }

        private Tuple<Complex, Complex> SolveReal(double a, double b, double c, double disc)
        {
            var rootDisc = Math.Sqrt(disc);

            return Tuple.Create(
                new Complex((-b + rootDisc) / (2 * a), 0),
                new Complex((-b - rootDisc) / (2 * a), 0)
            );
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

    public class ContinuationPassingStyle
    {
        public static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            var result1 = solver.Start(1, -3, 2); // Real roots
            var result2 = solver.Start(1, 2, 5);  // Complex roots

            Console.WriteLine($"Real Roots: {result1.Item1}, {result1.Item2}");
            Console.WriteLine($"Complex Roots: {result2.Item1}, {result2.Item2}");
        }
    }
}
