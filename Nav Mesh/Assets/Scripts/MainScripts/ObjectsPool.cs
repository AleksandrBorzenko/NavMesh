using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Responsible for holding and transfering the destroyed bots
/// </summary>
public class ObjectsPool : IObjectsPool
{
    /// <summary>
    /// Is pool empty
    /// </summary>
    public bool isEmpty => BotsList.Count == 0;

    /// <summary>
    /// List of bots in the pool
    /// </summary>
    public List<Bot> BotsList { get; private set; } = new List<Bot>();

    public UnityEvent<Bot> botAddedToObjectsPool { get; private set; } = new UnityEvent<Bot>();

    /// <summary>
    /// Add bot to a pool
    /// </summary>
    /// <param name="bot">Destroyed bot</param>
    public void AddBot(Bot bot)
    {
        BotsList.Add(bot);
        botAddedToObjectsPool?.Invoke(bot);
    }
    /// <summary>
    /// Remove the oldest bot from the list
    /// </summary>
    public void RemoveBot()
    {
        if(!isEmpty)
            BotsList.RemoveAt(0);
    }
}
