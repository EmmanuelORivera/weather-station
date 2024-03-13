namespace AnotherObserverExample
{
    public interface IObserver
    {
        // Receive update from subject
        void Update(ISubject subject);
    }
    public interface ISubject
    {
        // Attach an observer to the subject
        void Attach(IObserver observer);
        // Detach an observer from the subject
        void Detach(IObserver observer);
        // Notify all observers about an event
        void Notify();
    }

    // The subject owns some important state and notifies observers when 
    // the state changes

    public class Subject : ISubject
    {
        // for the sake of simplicity, the subject's state, essential to all
        // subscribers, is stored in this variable.

        public int State { get; set; } = -0;

        // List of subscribers.
        public List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            System.Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            System.Console.WriteLine("Subject: Detached an observer.");
        }

        // Trigger an update in each subscriber.
        public void Notify()
        {
            System.Console.WriteLine("Subject: Notifying observers ...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        // Usually, the subscription logic is only a fraction of what the subject
        // can really do. Subjects commonly hold same important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).

        public void SomeBusinessLogic()
        {
            System.Console.WriteLine("\nSubject: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            System.Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    // Concrete Observers react to the updates issued by the subject they had
    // been attached to.

    class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State < 3)
            {
                System.Console.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                System.Console.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }
}