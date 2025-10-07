namespace AdvancedTopics.Section2
{
    public class DelegatesAndEvents
    {
        public event EventHandler<int> MyEvent;

        public void MyHandler(object sender, int arg)
        {
            Console.WriteLine($"I just got {arg} from {sender?.GetType().Name}");
        }

        public static void Main(string[] args)
        {
            var demo = new DelegatesAndEvents();

            var eventInfo = typeof(DelegatesAndEvents).GetEvent("MyEvent");
            var handlerMethod = demo.GetType().GetMethod("MyHandler");

            // we need a delegate of a particular type
            var handler = Delegate.CreateDelegate(
              eventInfo.EventHandlerType,
              null, // object that is the first argument of the method the delegate represents
              handlerMethod
            );
            eventInfo.AddEventHandler(demo, handler);

            demo.MyEvent?.Invoke(null, 312);
        }
    }
}
