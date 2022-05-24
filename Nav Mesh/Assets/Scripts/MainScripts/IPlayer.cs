using UnityEngine.Events;
/// <summary>
/// Interaface with UnityEvent to not only bots
/// </summary>
public interface IPlayer
{
    UnityEvent<int> scoreChanged { get; set;}
    UnityEvent<int> healthChanged { get; set;}
}
