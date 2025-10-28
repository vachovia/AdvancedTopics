using System.Collections.Generic;

namespace AdvancedTopics.Section5
{
    public class MemoryManagamentDemo
    {
        void ChangeMe(ref Point p)
        {
            p.X++;
        }

        double MeasureDistance(in Point p1, in Point p2)
        {
            // p1 = new Point(); // p1 is readonly because of in modifier - ERROR
            // ChangeMe(ref p1); // p1 is readonly because of in modifier - ERROR

            p1.Reset(); // This is allowed, Reset modifies the internal state of p1 but !!! does not change dist value because p.Reset() works on copy of p1.

            Console.WriteLine($"Reset value of p1: {p1}");

            double deltaX = p2.X - p1.X;
            double deltaY = p2.Y - p1.Y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
        
        double MeasureDistance(Point p1,Point p2)
        {
            return 0.0;
        }

        public MemoryManagamentDemo()
        {
            var p1 = new Point(1.0, 1.0);
            var p2 = new Point(4.0, 5.0);

            var dist = MeasureDistance(in p1, in p2);

            var distOverload = MeasureDistance(p1, p2);
        }

        public static void Main(string[] args)
        {
            var p1 = new Point(1.0, 1.0);
            var p2 = new Point(4.0, 5.0);

            var demo = new MemoryManagamentDemo();

            var dist = demo.MeasureDistance(in p1, in p2);            

            Console.WriteLine($"Distance between {p1} and {p2} is {dist}");

            var distFromOrigin = demo.MeasureDistance(in p1, Point.Origin);

            Point copyOfOrigin = Point.Origin; // copyOfOrigin -> we get by-value copy of origin

            // Please uncomment these lines see what is allowed
            // ref var messWithOrigin = ref Point.Origin; // messWithOrigin -> we get reference to origin point - not possible
            // ref readonly var messWithOrigin1 = ref Point.Origin; // messWithOrigin -> we get reference to origin point - possible
            // messWithOrigin1.X = 10; // not possible because messWithOrigin1 is readonly reference

            Console.WriteLine($"Distance between {p1} and origin is {distFromOrigin}");

            dist = demo.MeasureDistance(p1, p2); // it works but we get runtime error if we have both overloads ???

            Console.WriteLine($"Distance between {p1} and {p2} is {dist}");
        }
    }

    public struct Point
    {
        public double X, Y;
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Reset()
        {
            X = Y = 0;
        }

        private static Point origin = new Point();
        public static ref readonly Point Origin => ref origin; // when we call Point.Origin we get readonly reference to origin point and not copy of it while passing into constructor.
        // so why we are not expanding memory usage

        public override string ToString() => $"({X}, {Y})";
    }
}
