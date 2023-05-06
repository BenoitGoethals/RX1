using System.Reactive;
using System.Reactive.Disposables;

namespace rx.core.chat;

public class ChatServer:IObservable<ChatRoom>
{
    private readonly List<IObserver<ChatRoom>>? _observers = new();
    public ChatServer()
    {
        _chatRoom.Add("1",new ChatRoom(){Description = "test",Name = "1"} );
        _chatRoom.Add("2", new ChatRoom() { Description = "test" , Name = "2" });
        _chatRoom.Add("3", new ChatRoom() { Description = "test", Name = "3" });
        _chatRoom.Add("4", new ChatRoom() { Description = "test", Name = "4" });
        _chatRoom.Add("5", new ChatRoom() { Description = "test", Name = "5" });
        _chatRoom.Add("6", new ChatRoom() { Description = "test", Name = "6" });


        //AddMsg("1", new MessageChat() { MsgTxt = "Welkom" });
        //AddMsg("2", new MessageChat() { MsgTxt = "Welkom" });
        //AddMsg("3", new MessageChat() { MsgTxt = "Welkom" });
        //AddMsg("5", new MessageChat() { MsgTxt = "Welkom" });
        //AddMsg("6", new MessageChat() { MsgTxt = "Welkom" });
        //AddMsg("4", new MessageChat() { MsgTxt = "Welkom" });
    }

    private readonly Dictionary<string, ChatRoom> _chatRoom = new Dictionary<string, ChatRoom>();

    public void AddMsg(string chatRoom,MessageChat msg)
    {
        msg.Chatroom=chatRoom;
        _chatRoom[chatRoom].AddMsg(msg);
        foreach (var observer in _observers.ToList())
        {
            observer.OnNext(_chatRoom[chatRoom]);
            
        }

    }
    public IDisposable Subscribe(IObserver<ChatRoom> observer)
    {
        if (_observers != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers?.Remove(observer));
    }


    public Dictionary<string, ChatRoom>  GetCurrentStatusChatRooms()
    { 
        return _chatRoom;
    }
}