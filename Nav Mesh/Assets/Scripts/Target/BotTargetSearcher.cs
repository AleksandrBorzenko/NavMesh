using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotTargetSearcher : IBotTargetSearcher
{
    public List<ITarget<Bot>> GetTargets()
    {
        var bots = GameObject.FindObjectsOfType<Bot>();

        var targets = new List<ITarget<Bot>>();

        foreach (var bot in bots)
        {
            targets.Add(bot.GetComponent<ITarget<Bot>>());
        }

        return targets;
    }

}
