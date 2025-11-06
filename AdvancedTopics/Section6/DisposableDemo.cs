using System;
using System.Diagnostics;
using System.Threading;

namespace AdvancedTopics.Section6
{
    public class DisposableExample
    {
        public static void Main(string[] args)
        {
            using (var resource = new DisposableResource())
            {
                Console.WriteLine("Using the resource...");
            } // Dispose is called automatically here

            using (var resource = new SimpleTimer())
            {
                Thread.Sleep(1000); // Simulate some work
                Console.WriteLine("Using Simple Timer...");
            } // Dispose is called automatically here

            var st = new Stopwatch();

            using (SimpleAction.Create(() => st.Start(), () => st.Stop()))
            {
                Console.WriteLine($"Simple Action in Main thread.");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"Elapsed time: {st.ElapsedMilliseconds} ms");
        }
    }

    public class SimpleAction : IDisposable
    {
        private readonly Action _end;

        private SimpleAction(Action start, Action end)
        {
            _end = end;
            start();
        }

        public void Dispose()
        {
            _end();

            Console.WriteLine($"Disposing Simple Action...");
        }

        public static SimpleAction Create(Action start, Action end)
        {
            return new SimpleAction(start, end);
        }

    }

    public class SimpleTimer : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        public SimpleTimer()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        public void Dispose()
        {
            _stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {_stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public class DisposableResource : IDisposable
    {
        public DisposableResource()
        {
            Console.WriteLine("Hello!");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed! Goodbye.");
        }
    }
}
