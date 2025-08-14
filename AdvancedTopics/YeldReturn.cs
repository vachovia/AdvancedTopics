namespace AdvancedTopics
{
    public class YeldReturn
    {
        public static void Main(string[] args)
        {
            foreach (int number in GetFibonacciSequence(10))
            {
                Console.WriteLine(number);
            }

            foreach (int number in GenerateNumbers())
            {
                Console.WriteLine(number);
            }
        }

        static IEnumerable<int> GetFibonacciSequence(int count)
        {
            int previous = 0, current = 1;

            for (int i = 0; i < count; i++)
            {
                yield return previous;

                int next = previous + current;
                previous = current;
                current = next;
            }
        }

        static IEnumerable<int> GenerateNumbers()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}
