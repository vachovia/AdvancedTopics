using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace AdvancedTopics.Section4
{
    public class ExtensionMethodsDemo
    {
        public static void Main(string[] args)
        {
            var foo = new Foo();
            Console.WriteLine(foo.Measure());

            Console.WriteLine(foo);
            Console.WriteLine(ExtensionMethods.ToString(foo)); // calls your extension method
            Console.WriteLine(foo.ToString()); // calls predefined ToString()

            var derived = new FooDerived();
            Foo parent = derived;
            Console.WriteLine("As parent: " + parent.Measure()); // 10
            Console.WriteLine("As child:  " + derived.Measure()); // 42

            Console.WriteLine(42.ToBinary());

            Person p = ("Dmitri", 22).ToPerson();
            Console.WriteLine(p); // p.ToString() is same as defined in Person class

            Console.WriteLine(Tuple.Create(12, "hello").Measure());

            Func<int, int> calculate = delegate (int num)
            {
                Thread.Sleep(1000);
                return num + 8;
            };
            Console.WriteLine($"Simple call: {calculate(2)}");
            var st = calculate.Measure(2);
            Console.WriteLine($"took {st.ElapsedMilliseconds}msec");
        }
    }

    public class Foo
    {
        public virtual string Name => "Foo";
    }

    public class FooDerived : Foo
    {
        public override string Name => "FooDerived";
    }

    public class Person
    {
        public int Age;
        public string Name = string.Empty;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, " + $"{nameof(Age)}: {Age}";
        }
    }

    public static class ExtensionMethods
    {//    ↑↑↑↑↑↑ must be static

        // extension on your own type
        public static int Measure(this Foo foo)
        { //   ↑↑↑↑↑↑  
            return foo.Name.Length;
        }

        // extension method on an existing type (incl. primitive type)
        public static string ToBinary(this int n)
        {
            return Convert.ToString(n, 2);
        }

        // extension on an interface
        public static void Save(this ISerializable serializable)
        {
            // 
        }

        // you don't get extension method polymorphism
        public static int Measure(this FooDerived derived)
        {
            return 42;
        }

        // it doesn't work as an override
        public static string ToString(this Foo foo)
        {
            return "test";
        }

        // extension methods on value tuples
        public static Person ToPerson(this (string name, int age) data)
        {
            return new Person { Name = data.name, Age = data.age };
        }

        // extension on a generic type
        public static int Measure<T, U>(this Tuple<T, U> t)
        {
            return t.Item2?.ToString()?.Length ?? 0;
        }

        // extension on a delegate
        public static Stopwatch Measure(this Func<int, int> action, int num)
        {
            var st = new Stopwatch();
            st.Start();
            action(num);
            st.Stop();
            return st;
        }
    }
}
