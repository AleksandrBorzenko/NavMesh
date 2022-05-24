using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// A module of bot which is responsible for a searching of ITarget sign
/// </summary>
public class BotTargetSearcher : IBotTargetSearcher
{
    /// <summary>
    /// Target of bot
    /// </summary>
    public ITarget<Bot> BotTarget { get; private set; }
    /// <summary>
    /// Tells if bot has a target
    /// </summary>
    public bool hasTarget { get; set; }
    /// <summary>
    /// Gets all targets on the scene
    /// </summary>
    /// <returns>List of ITarget</returns>
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
    /// <summary>
    /// Gets the nearest target from list of ITarget
    /// </summary>
    /// <param name="myCurrentPos">Bot's current position</param>
    /// <param name="targets">List of ITarget</param>
    public void GetNearestTarget(Vector3 myCurrentPos, List<ITarget<Bot>> targets)
    {
        var arr = new List<float>();

        foreach (var target in targets)
        {
            arr.Add(Vector3.Distance(myCurrentPos,target.position));
        }

        var index = arr.FindIndex(minDistance => minDistance == Mathf.Min(arr.ToArray()));
        BotTarget = targets[index];
        hasTarget = true;
    }
}
