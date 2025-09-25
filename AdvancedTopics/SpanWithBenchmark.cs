using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.InteropServices;

namespace AdvancedTopics
{
    public class SpanWithBenchmark
    {
        private static readonly List<string> words = new List<string> { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchy>();
            // TestCollectionsMarshal();
        }

        public static void TestCollectionsMarshal()
        {
            Span<string> spans = CollectionsMarshal.AsSpan(words);

            spans[1] = "Dodgy";
            var vlad = spans[1].AsSpan();
            var vika = vlad.Contains('o');

            // Output modified list
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }
    }

    [MemoryDiagnoser]
    public class Benchy
    {
        private static readonly string _dateAsText = "08 07 2021";
        private static readonly List<string> words = new List<string> { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };

        [Benchmark]
        public (int day, int month, int year) DateWithSpan()
        {
            var dateAsSpan = _dateAsText.AsSpan();
            var dayAsText = dateAsSpan.Slice(0, 2);
            var monthAsText = dateAsSpan.Slice(3, 2);
            var yearAsText = dateAsSpan.Slice(6);
            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);
            return (year, month, day);
        }

        [Benchmark]
        public (int day, int month, int year) DateWithStringAndSubstring()
        {
            var dayAsText = _dateAsText.Substring(0, 2);
            var monthAsText = _dateAsText.Substring(3, 2);
            var yearAsText = _dateAsText.Substring(6);

            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day, month, year);
        }

        //[Benchmark]
        //public void CollectionsMarshalForNumbers()
        //{
        //    // Create a large list
        //    List<int> numbers = new List<int>();
        //    for (int i = 0; i < 1_000_000; i++)
        //    {
        //        numbers.Add(i);
        //    }

        //    // Sum using CollectionsMarshal.AsSpan
        //    Span<int> span = CollectionsMarshal.AsSpan(numbers);
        //    int sum = 0;

        //    foreach (int number in span)
        //    {
        //        sum += number;
        //    }

        //    Console.WriteLine($"Sum of elements: {sum}");
        //}

        //[Benchmark]
        //public void OldFashionForNumbers()
        //{
        //    // Create a large list
        //    List<int> numbers = new List<int>();
        //    for (int i = 0; i < 1_000_000; i++)
        //    {
        //        numbers.Add(i);
        //    }

        //    int sum = 0;

        //    foreach (int number in numbers)
        //    {
        //        sum += number;
        //    }

        //    Console.WriteLine($"Sum of elements: {sum}");
        //}

        [Benchmark]
        public ReadOnlySpan<char> TestCollectionsMarshal()
        {
            Span<string> spans = CollectionsMarshal.AsSpan(words);

            spans[1] = "Dodgy";

            // Output modified list
            foreach (var word in spans)
            {
                Console.WriteLine(word);
            }

            return spans[1];
        }
    }
}
