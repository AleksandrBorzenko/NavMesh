using System.Collections.Generic;
/// <summary>
/// The interface for object pooling
/// </summary>
public interface IObjectsPool
{
    bool isEmpty { get; }
    List<Bot> BotsList { get; }
    void AddBot(Bot bot);
    void RemoveBot();
}
