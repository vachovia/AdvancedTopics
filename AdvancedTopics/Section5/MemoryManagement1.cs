using System.Collections.Generic;

namespace AdvancedTopics.Section5
{
    public class MemoryManagamentDemo1
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

        // we cannot overload method with in keyword, it compiles but gives runtime error ??????????????????
        double MeasureDistance(Point p1,Point p2)
        {
            return 0.0;
        }

        public MemoryManagamentDemo1()
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

            var demo = new MemoryManagamentDemo1();

            var dist = demo.MeasureDistance(in p1, in p2);            

            Console.WriteLine($"Distance between {p1} and {p2} is {dist}");

            dist = demo.MeasureDistance(in p1, new Point()); // we cannot use ref but can use new keyword

            Console.WriteLine($"Distance between {p1} and origin is {dist}");

            dist = demo.MeasureDistance(p1, p2);

            Console.WriteLine($"Distance between {p1} and {p2} is {dist}");
        }
    }    
}
