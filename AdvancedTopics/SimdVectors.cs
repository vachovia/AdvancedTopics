using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace AdvancedTopics
{
    public class SimdVectors
    {
        public static void Main(string[] args)
        {
            if (Avx.IsSupported)
            {
                var x = Vector256.Create(1.0, 2.0, 3.0, 4.0);
                var y = Vector256.Create(1.0, 2.0, 4.0, 8.0);

                var f = Avx.Add(x, y);
                var scalar = f.ToScalar();

                var z = Vector<int>.Count;
                var t = Vector256.IsHardwareAccelerated;

                Console.WriteLine($"AVX is supported{f.GetUpper()}");
            }

            byte[] array1 = Enumerable.Range(0, 128).Select(i => (byte)i).ToArray();

            byte[] array2 = Enumerable.Range(4, 128).Select(i => (byte)i).ToArray();

            byte[] result = new byte[128];

            int size = Vector<byte>.Count;

            for (int i = 0; i < array1.Length; i += size)
            {
                var va = new Vector<byte>(array1, i);
                var vb = new Vector<byte>(array2, i);
                var vc = va + vb;
                vc.CopyTo(result, i);
            }

            Console.WriteLine($"Result: {result.Max()}");
        }
    }
}
