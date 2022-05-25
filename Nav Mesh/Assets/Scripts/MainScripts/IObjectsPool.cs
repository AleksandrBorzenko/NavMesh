using System.Collections.Generic;

public interface IObjectsPool
{
    bool isEmpty { get; set; }
    List<Bot> BotsList { get; }
    void AddBot(Bot bot);
    void RemoveBot(Bot bot);
}
