using System.Dynamic;
using System.Xml.Linq;

namespace AdvancedTopics.Section3
{
    public class DynamicObjectExample
    {
        public const string xml = @"
            <people>
                <employees>
                    <person name='Vlad'/>
                </employees>
            </people>";

        public static void Main(string[] args)
        {
            // Simple example
            // DynamicObjectSimple();

            // DynamicObject example
            //var w = new Widget() as dynamic;
            //Console.WriteLine(w.Hello);
            //Console.WriteLine(w[7]);
            //w.WhatIsThis();

            // XML parsing example classic way
            //var node = XElement.Parse(xml);
            //var name = node?.Element("person")?.Attribute("name");
            //Console.WriteLine(name?.Value);

            // XML parsing dynamic way
            var node = XElement.Parse(xml);
            dynamic dyn = new DynamicXmlNode(node);
            Console.WriteLine(dyn.employees.person.name);
        }

        public static void DynamicObjectSimple()
        {
            Console.WriteLine("Dynamic Object Example");
            dynamic dyn = "Hello, World!";
            Console.WriteLine(dyn);
            Console.WriteLine(dyn.Length);
            Console.WriteLine(dyn.GetType());
            dyn = 42;
            Console.WriteLine(dyn);
            dyn = new { Name = "Alice", Age = 30 };
            Console.WriteLine($"{dyn.Name} is {dyn.Age} years old.");
            try
            {
                // This will throw a RuntimeBinderException at runtime
                Console.WriteLine(dyn.NonExistentProperty);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                Console.WriteLine($"Runtime error: {ex.Message}");
            }
        }
    }

    public class DynamicXmlNode: DynamicObject
    {
        private XElement _node;

        public DynamicXmlNode(XElement node)
        {
            _node = node;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var child = _node.Element(binder.Name);
            if (child != null)
            {
                result = new DynamicXmlNode(child);
                return true;
            }
            var attr = _node.Attribute(binder.Name);
            if (attr != null)
            {
                result = attr.Value;
                return true;
            }
            result = null;
            return false;
        }
    }

    public class Widget: DynamicObject
    {
        public void WhatIsThis()
        {
            Console.WriteLine(This.World); // this will fail without This
        }

        public dynamic This => this;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //if (binder.Name == "Hello")
            //{
            //    result = "Dynamic Widget";
            //    return true;
            //}
            //result = null;
            //return false;
            result = binder.Name;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            //if (indexes.Length == 1 && indexes[0] is int index)
            //{
            //    result = $"Index {index}";
            //    return true;
            //}
            //result = null;
            //return false;
            if (indexes.Length == 1)
            {
                result = new string('*', (int)indexes[0]);
                return true;
            }

            result = null;
            return false;
        }
    }
}
