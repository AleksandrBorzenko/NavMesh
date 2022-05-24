using System.Collections;

public interface IBotBehaviour
{
    IEnumerator DoDamage(ITarget<Bot> target);
    void TakeDamage(int damage);
    int DelayForDoDamage { get; }
    bool canDamage { get; set; }
    void FindTarget();
}
