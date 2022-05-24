using System.Collections;
using UnityEngine.Events;


public interface IBotBehaviour
{
    IEnumerator DoDamage(ITarget<Bot> target);
    void TakeDamage(int damage);
    int DelayForDoDamage { get; }
    bool canDamage { get; set; }
    void FindTarget();
    UnityEvent TargetLost { get; set; }
    bool isStaying { get; set; }

}
