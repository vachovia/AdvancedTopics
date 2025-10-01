using System.Dynamic;

namespace AdvancedTopics.Section3
{
    public class ExpandoObjectExample
    {
        public static void Main(string[] args)
        {
            dynamic person = new ExpandoObject();
            person.FirstName = "John";
            person.LastName = "Doe";
            person.Age = 30;
            Console.WriteLine($"{person.FirstName} is {person.Age} years old.");


            person.Address = new ExpandoObject();
            person.Address.Street = "123 Main St";
            person.Address.City = "Anytown";
            Console.WriteLine($"{person.FirstName} lives at {person.Address.Street}, {person.Address.City}.");


            person.SayHello = (Action)(() =>
            {
                Console.WriteLine($"Hello, my name is {person.FirstName} {person.LastName}.");
            });
            person.SayHello();


            person.FallsIll = null;
            person.FallsIll = new EventHandler<dynamic>((sender, args) =>
            {
                Console.WriteLine($"We need a doctor for {args}.");
            });
            EventHandler<dynamic> e = person.FallsIll;
            e?.Invoke(person, person.FirstName);


            var dict = (IDictionary<string, object>)person;
            Console.WriteLine(dict.ContainsKey("FirstName"));
            Console.WriteLine(dict.ContainsKey("MiddleName"));
            Console.WriteLine(dict.ContainsKey("LastName"));
            Console.WriteLine(dict.ContainsKey("Age"));
            dict["MiddleName"] = "Michael";
            Console.WriteLine(person.MiddleName);
        }
    }
}
