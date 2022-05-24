using System.Collections.Generic;

public interface IBotTargetSearcher
{
    List<ITarget<Bot>> GetTargets();
}