using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotTargetSearcher : IBotTargetSearcher
{
    private bool _hasTarget;

    public ITarget<Bot> BotTarget { get; private set; }

    public bool hasTarget => _hasTarget;

    public List<ITarget<Bot>> GetTargets()
    {
        var bots = GameObject.FindObjectsOfType<Bot>();

        var targets = new List<ITarget<Bot>>();

        foreach (var bot in bots)
        {
            if(!bot.IsThisMySearcher(this))
                targets.Add(bot.GetComponent<ITarget<Bot>>());
        }

        return targets;
    }

    public void GetNearestTarget(Vector3 myCurrentPos, List<ITarget<Bot>> targets)
    {
        var arr = new List<float>();

        foreach (var target in targets)
        {
            arr.Add(Vector3.Distance(myCurrentPos,target.position));
        }

        var index = arr.FindIndex(minDistance => minDistance == Mathf.Min(arr.ToArray()));
        BotTarget = targets[index];
        _hasTarget = true;
    }

    public float GetDistance(Vector3 myCurrentPos)
    {
       return Vector3.Distance(myCurrentPos, myCurrentPos);
    }
}
