using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotTargetSearcher : IBotTargetSearcher
{
    public ITarget<Bot> BoTarget { get; private set; }
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

    public void GetNearestTarget(Vector3 myCurrentPos)
    {
        var targets = GetTargets();
        var arr = new List<float>();

        foreach (var target in targets)
        {
            arr.Add(Vector3.Distance(myCurrentPos,target.position));
        }

        var index = arr.FindIndex(minDistance => minDistance == Mathf.Min(arr.ToArray()));
        BoTarget = targets[index];
    }

}
