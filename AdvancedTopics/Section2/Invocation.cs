namespace AdvancedTopics.Section2
{
    public class Invocation
    {
        public static void Main(string[] arggs)
        {
            var s = "abracadabra   ";
            var t = typeof(string); // t // [System.String]
            var trimMethod = t.GetMethod("Trim", Array.Empty<Type>()); // trimMethod // [System.String Trim()]
            var result = trimMethod?.Invoke(s, Array.Empty<object>());
            // result // "abracadabra"

            // bool int.TryParse(str, out int n)
            var numberString = "123";
            var parseMethod = typeof(int).GetMethod("TryParse", new[]{ typeof(string), typeof(int).MakeByRefType() });
            // parseMethod // [Boolean TryParse(System.String, Int32 ByRef)]
            object?[] args = { numberString, null };
            var ok = parseMethod?.Invoke(null, args);
            // ok // true
            // args[1] // 123

            var at = typeof(Activator);
            var method = at.GetMethod("CreateInstance", Array.Empty<Type>());
            // method // [T CreateInstance[T]()]
            var ciGeneric = method?.MakeGenericMethod(typeof(Guid));
            // ciGeneric // [System.Guid CreateInstance[Guid]()]
            var guid = ciGeneric?.Invoke(null, null);
            // guid // [00000000-0000-0000-0000-000000000000]
        }
    }
}
