using System.Runtime.InteropServices;

namespace AdvancedTopics.Section5
{
    public class MemoryManagamentDemo2
    {
        // Span<T>
        public static void Main(string[] args)
        {
            // ReverseString();
            // In Build properties settings to Enable unsafe code
            unsafe
            {
                // Create Spans on Manged and Unmanaged memory
                byte* ptr = stackalloc byte[100];
                Span<byte> memory = new Span<byte>(ptr, 100);

                IntPtr unmanagedPtr = Marshal.AllocHGlobal(123);
                Span<byte> unmanagedMemory = new Span<byte>(unmanagedPtr.ToPointer(), 123);
                Marshal.FreeHGlobal(unmanagedPtr);
            }

            char[] stuff = "hello".ToCharArray();
            Span<char> arrayMemory = stuff;

            arrayMemory.Fill('x');
            Console.WriteLine(stuff);
            arrayMemory.Clear();
            Console.WriteLine(stuff);

            // string immutable so it has another type of Span
            // we cannot modify string becouse of it we have ReadOnly Span
            ReadOnlySpan<char> more = "Hi there !".AsSpan();

            Console.WriteLine($"Out span has {more.Length} elements");
        }

        private static void ReverseString()
        {
            string text = "My name is Donald Trump";
            Console.WriteLine(string.Join(" ", text.Split(' ').Reverse())); // reverse sentence
        }

        public static string Reverse(string s) // reverse string
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
