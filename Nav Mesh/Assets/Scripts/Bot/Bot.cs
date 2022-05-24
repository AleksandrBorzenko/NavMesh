
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour, ITarget<Bot>, IBotBehaviour
{
    private readonly int minDamage = 1;
    private readonly int maxDamage = 6;

    private readonly int minHealth = 10;
    private readonly int maxHealth = 20;

    private readonly int minVelocity = 1;
    private readonly int maxVelocity = 3;

    public readonly BotInfo botInfo = new BotInfo();

    public Bot targetSign => this;

    public Vector3 position => transform.position;

    public int DelayForDoDamage => 2;

    public bool canDamage { get; set; } = true;

    private BotTargetSearcher botTargetSearcher = new BotTargetSearcher();

    private NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
    }

    void Start()
    {
        SetBotData();
        var targets = botTargetSearcher.GetTargets();
        if (targets.Count == 0) return;
        botTargetSearcher.GetNearestTarget(transform.position,targets);
        if (navMeshAgent.CalculatePath(transform.position, navMeshPath))
            navMeshAgent.SetDestination(botTargetSearcher.BotTarget.position);
    }

public void SetBotData()
    {
        botInfo.SetDamage(minDamage,maxDamage);
        botInfo.SetHealth(minHealth,maxHealth);
        botInfo.SetVelocity(minVelocity,maxVelocity);
        navMeshAgent.speed = botInfo.velocity;
    }

    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    public bool IsThisMySearcher(BotTargetSearcher searcher)
    {
        return botTargetSearcher == searcher;
    }

    void Update()
    {
        if (botTargetSearcher.hasTarget)
        {
            if (navMeshAgent.remainingDistance<=navMeshAgent.stoppingDistance) 
            {
                    if (botTargetSearcher.BotTarget.targetSign.botInfo.isAlive && canDamage)
                        StartCoroutine(DoDamage(botTargetSearcher.BotTarget));
            }
            else
            {
                navMeshAgent.SetDestination(botTargetSearcher.BotTarget.position);
            }
        }
    
    }

    public IEnumerator DoDamage(ITarget<Bot> target)
    {
        target.targetSign.TakeDamage(botInfo.damage);
        canDamage = false;
        yield return new WaitForSeconds(DelayForDoDamage);
        canDamage = true;
    }

    public void TakeDamage(int damage)
    {
        var isHealthMoreZero = (botInfo.health - damage) > 0;
        if (isHealthMoreZero)
        {
            botInfo.DecreaseHealth(damage);
        }
        else
        {
            botInfo.SetHealthToZero();
            botInfo.isAlive = false;
            Destroy(gameObject,0.1f);
        }
    }
}

