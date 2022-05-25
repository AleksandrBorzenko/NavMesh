using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Determine the behaviour of the bot
/// </summary>
public interface IBotBehaviour
{
    IEnumerator DoDamage(ITarget<Bot> target, Bot sender);
    void TakeDamage(int damage, Bot sender);
    int DelayForDoDamage { get; }
    bool canDamage { get; set; }
    void FindTarget();
    void Move();
    UnityEvent TargetLost { get; set; }
    bool isStaying { get; set; }
    void SendBotToObjectsPool();

}
