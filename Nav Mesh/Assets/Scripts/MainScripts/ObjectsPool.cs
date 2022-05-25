using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : IObjectsPool
{
    public bool isEmpty { get; set; }
    public List<Bot> BotsList { get; private set; }

    public void AddBot(Bot bot)
    {
        BotsList.Add(bot);   
    }

    public void RemoveBot(Bot bot)
    {
        BotsList.Remove(bot);
    }
}
