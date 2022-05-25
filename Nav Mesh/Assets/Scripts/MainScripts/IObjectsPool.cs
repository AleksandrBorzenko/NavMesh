using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// The interface for object pooling
/// </summary>
public interface IObjectsPool
{
    bool isEmpty { get; }
    List<Bot> BotsList { get; }
    void AddBot(Bot bot);
    void RemoveBot();
    UnityEvent<Bot> botAddedToObjectsPool { get; }
}
