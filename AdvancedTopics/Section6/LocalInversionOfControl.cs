namespace AdvancedTopics.Section6
{
    public static class ExtensionMethods
    {
        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;

            public enum Operation
            {
                None,
                And,
                Or
            }

            internal Operation PendingOp;

            internal BoolMarker(bool result, T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }

            public BoolMarker(bool result, T self): this(result, self, Operation.None)
            {
            }

            public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }

        public static T AddTo<T>(this T self, ICollection<T> collection)
        {
            collection.Add(self);
            return self;
        }

        public static T AddTo<T>(this T self, params ICollection<T>[] collection)
        {
            foreach(var coll in collection)
            {
                coll.Add(self);
            }
                
            return self;
        }

        public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject subject, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(subject).Any(), subject);
        }

        public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject subject, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(subject).Any(), subject);
        }

        public static BoolMarker<T> HasNo<T, U>(this BoolMarker<T> marker, Func<T, IEnumerable<U>> props)
        {
            if (marker.PendingOp == BoolMarker<T>.Operation.And && !marker.Result)
            {
                return marker;
            }

            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }

        public static bool IsOneOf<T>(this T self, params T[] options)
        {
            return options.Contains(self);
        }
    }

    public class Person
    {
        public List<string> Names { get; set; } = new List<string>();
        public List<Person> Children { get; set; } = new List<Person>();
    }

    public class MyAnotherClass
    {
        public List<int> Values = new();
    }

    internal class LocalInversionOfControl
    {
        public static void Main(string[] args)
        {
            // typical container
            var list = new List<int>();
            list.Add(123); // list in control            
            list.AddRange(new[] { 1, 2, 3 });

            // inverted
            var list2 = new List<int>();
            2.AddTo(list).AddTo(list2);
            3.AddTo(list, list2);
            456.AddTo(list); // inverted control - list passed as argument

            var myclass = new MyAnotherClass();
            if (myclass.Values.Count == 0) { } // typical
            if (!myclass.Values.Any()) { } // clearer

            if (myclass.HasSome(x => x.Values)) { }
            if (myclass.HasNo(x => x.Values)) { }

            var person = new Person();
            if (person.HasSome(p => person.Names).And.HasNo(p => p.Children))
            {                
            }

            string op = "OR";

            if (op == "AND" || op == "OR" || op == "XOR") { } // typical
            if (op.IsOneOf("AND", "OR", "XOR")) { } // clearer
        }
    }
}
