using System;
using System.Collections;

namespace AdvancedTopics.Section6
{
    interface IMyDisposable<T>: IDisposable
    {
        void IDisposable.Dispose()
        {
            // default Dispose implementation
            Console.WriteLine($"Disposing {typeof(T).Name}");
        }
    }

    interface IScalar<T> : IEnumerable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return (T)this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MyClass : IScalar<MyClass>, IMyDisposable<MyClass>
    {
        public override string ToString()
        {
            return "MyClass";
        }
    }

    public  class DuckTypingMixins
    {
        public static void Main(string[] args)
        {
            // duck typing and mixins example

            // GetEnumerator() — foreach (IEnumerable<T>)
            // Dispose() — using (IDisposable)

            using var mc = new MyClass();

            foreach (var x in mc)
            {
                Console.WriteLine(x);
            }
        }
    }
}


// duck typing: an object is treated as implementing an interface if it has the required methods/properties
// if class has Dispose method but it is not declared as implementing IDisposable, it can still be used in a using statement
// mixin: a class can "inherit" behavior from multiple sources without formal inheritance
// here, IScalar<T> provides GetEnumerator implementation to MyClass without MyClass explicitly implementing IEnumerable<T>