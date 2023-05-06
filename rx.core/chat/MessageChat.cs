namespace rx.core.chat;

public class MessageChat
{
    public Guid Guid { get; set; }= Guid.NewGuid();
    public string? MsgTxt { get; set; }
    public string Chatroom { get; set; }
}