namespace AdvancedTopics.Section4
{
    public class ExtensionMethodsDemo1
    {
        public static void Main(string[] args)
        {
            string s = "Meaning of life";
            s.SetTag(42);
            Console.WriteLine(s.GetTag());
        }
    }    

    public static class ExtensionMethods1
    {
        private static Dictionary<WeakReference, object> data = new Dictionary<WeakReference, object>();

        public static object? GetTag(this object o)
        {
            var key = data.Keys.FirstOrDefault(k => k.IsAlive && k.Target == o);

            return key != null ? data[key] : null;
        }

        public static void SetTag(this object o, object tag)
        {
            var key = data.Keys.FirstOrDefault(k => k.IsAlive && k.Target == o);

            if (key != null)
            {
                data[key] = tag;
            }
            else
            {
                data.Add(new WeakReference(o), tag);
            }
        }
    }
}
