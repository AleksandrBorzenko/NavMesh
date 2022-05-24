using System.Collections.Generic;
using UnityEngine;

public interface IBotTargetSearcher
{
    List<ITarget<Bot>> GetTargets();
    bool hasTarget { get; }

    float GetDistance(Vector3 myCurrentPos);
}