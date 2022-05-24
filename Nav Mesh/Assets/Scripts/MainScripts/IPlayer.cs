using UnityEngine.Events;

public interface IPlayer
{
    UnityEvent<int> scoreChanged { get; set;}
    UnityEvent<int> healthChanged { get; set;}
}
