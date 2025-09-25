using System.Reflection;

namespace AdvancedTopics.Section2
{
    public static class Inspection
    {
        public static void Main(string[] args)
        {
            InspectTypes();
        }

        public static void InspectTypes()
        {
            var t = typeof(Guid);
            Console.WriteLine(t.Name); // "Guid"
            Console.WriteLine(t.FullName); // "System.Guid"            

            var ctors = t.GetConstructors(); // ConstructorInfo[6] { [Void .ctor(Byte[])], [Void .ctor(System.ReadOnlySpan`1[System.Byte])], [Void .ctor(UInt32, UInt16, UInt16, Byte, Byte, Byte, Byte, Byte, Byte, Byte, Byte)], [Void .ctor(Int32, Int16, Int16, Byte[])], [Void .ctor(Int32, Int16, Int16, Byte, Byte, Byte, Byte, Byte, Byte, Byte, Byte)], [Void .ctor(System.String)] }

            foreach (var ctor in ctors)
            {
                Console.Write(" - Guid(");

                var pars = ctor.GetParameters();

                for (var i = 0; i < pars.Length; ++i)
                {
                    var par = pars[i];
                    Console.Write($"{par.ParameterType.Name} {par.Name}");
                    if (i + 1 != pars.Length) Console.Write(",");
                }

                Console.Write(")");
                Console.WriteLine();
            }

            MethodInfo[] methods = t.GetMethods(); // methods // MethodInfo[23] { [System.Guid Parse(System.String)], [System.Guid Parse(System.ReadOnlySpan`1[System.Char])], [Boolean TryParse(System.String, System.Guid ByRef)], [Boolean TryParse(System.ReadOnlySpan`1[System.Char], System.Guid ByRef)], [System.Guid ParseExact(System.String, System.String)], [System.Guid ParseExact(System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char])], [Boolean TryParseExact(System.String, System.String, System.Guid ByRef)], [Boolean TryParseExact(System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char], System.Guid ByRef)], [Byte[] ToByteArray()], [Boolean TryWriteBytes(System.Span`1[System.Byte])], [System.String ToString()], [Int32 GetHashCode()], [Boolean Equals(System.Object)], [Boolean Equals(System.Guid)], [Int32 CompareTo(System.Object)], [Int32 CompareTo(System.Guid)], [Boolean op_Equality(System.Guid, System.Guid)], [Boolean op_Inequality(System.Guid, System.Guid)], [System.String ToString(System.String)], [System.String ToString(System.String, Sys...

            foreach (var method in methods)
            {
                Console.WriteLine(method.Name);
            }

            PropertyInfo[] properties = t.GetProperties(); // PropertyInfo[0] { }
            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
            }

            EventInfo[] events = t.GetEvents(); // EventInfo[0] { }
            foreach (var e in events)
            {
                Console.WriteLine(e.Name);
            }
        }
    }
}
