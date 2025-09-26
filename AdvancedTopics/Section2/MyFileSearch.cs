public static class MyDelegateEvent
{
    public static void Main(string[] args)
    {
        MyFileSearch x = new MyFileSearch();

        // publisher -- subcriber
        // observable -- observer

        x.Publisher += Subscriber1;
        x.Publisher += Subscriber2;
        // x.Publisher = null; // crashing if no delegate assigned
        // event is encapsulation of delegate and uses it internally and makes them safe
        // if we use event keyword with delegate then it will not allow to assign null

        Console.WriteLine("File Search started....");

        _ = Task.Run(() => x.Search(@"C:\Temp")); // non blocking call
        //x.Search("E:\\shivprasad data"); // blocking call

        Console.WriteLine("Continue executing MAIN function....");

        Console.ReadLine();

        static void Subscriber1(string filename)
        {
            Console.WriteLine(filename);
        }
        static void Subscriber2(string filename)
        {
            Console.WriteLine(filename);
        }

        // Task.Run(()=>x.DirSearch("E:\\shivprasad data"));
        // Task.Run(() => MethodWithParameter(param));
    }
}

public class MyFileSearch
{
    public delegate void searchMethod(string search);
    // delegates are callback methods useful for parallel programming and not encapsulated
    // events are encapsulated delegates and used for safe communication between publisher and subscriber
    // public event searchMethod? publisher;
    public event searchMethod Publisher = null;

    public void Search(string dir)
    {
        try
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    // Notify subscribers about the found file
                    // publisher(f); // also valid
                    Publisher?.Invoke(f);
                }
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}