using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Interface for targetSearcher module
/// </summary>
public interface IBotTargetSearcher
{
    List<ITarget<Bot>> GetTargets();
    bool hasTarget { get; }
}