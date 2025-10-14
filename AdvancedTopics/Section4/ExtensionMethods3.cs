using System.Net;
using System.Text;

namespace AdvancedTopics.Section4
{
    public class ExtensionMethodsDemo3
    {
        public static void Main(string[] args)
        {
            var p = new Person1();
            string postcode = MyMethod(p);
            Console.WriteLine($"Address is: {postcode}");
        }

        public static string MyMethod(Person1 p)
        {
            string postcode;
            //if (p != null)
            //{
            //    if (HasMedicalRecord(p) && p.Address != null)
            //    {
            //        CheckAddress(p.Address);
            //        if (p.Address.PostCode != null)
            //            postcode = p.Address.PostCode.ToString();
            //        else
            //            postcode = "UNKNOWN";
            //    }
            //}

            // postcode = p?.Address?.PostCode ?? "UNKNOWN";

            // Logic of this file is here

            postcode = p.With(x => x.Address).With(x => x.PostCode); // example of usage of With extension method

            postcode = p.If(HasMedicalRecord).With(x => x.Address).Do(CheckAddress).Return(x => x.PostCode, "UNKNOWN"); // example of usage of If, With, Do and Return extension methods

            return postcode;
        }

        private static void CheckAddress(Address pAddress)
        {
            Console.WriteLine("Address was checked successfully.");
        }

        private static bool HasMedicalRecord(Person1 person)
        {
           return true;
        }
    }

    public class Person1
    {
        public Address Address { get; set; } = new Address();
    }

    public class Address
    {
        public string PostCode { get; set; } = "UNKNOWN";
    }

    public static class ExtensionMethodsMonad
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue) where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }

        public static TResult WithValue<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) where TInput : struct
        {
            return evaluator(o);
        }
    }
}

    