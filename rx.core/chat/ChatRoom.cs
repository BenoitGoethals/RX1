using System.Linq;
using System.Reactive.Disposables;

namespace rx.core.chat;

public class ChatRoom:IObservable<MessageChat>
{
    private readonly List<IObserver<MessageChat>>? _observers = new();
 
    public string Name { get; set; }

    public string Description { get; set; }
    public string Title { get; set; }

    public Queue<MessageChat> Chats { get; } = new Queue<MessageChat>();

    public void AddMsg(MessageChat chat)
    {
        Chats.Enqueue(chat);
        if (_observers == null) return;
        foreach (var observer in _observers.ToList())
        {
            observer.OnNext(chat);
        }
    }

    public IDisposable Subscribe(IObserver<MessageChat> observer)
    {
        if (_observers != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers?.Remove(observer));
    }
}