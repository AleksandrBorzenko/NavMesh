using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// This is a facade for bot's behaviour
/// </summary>
public class Bot : MonoBehaviour, ITarget<Bot>, IBotBehaviour,IPlayer
{
    private readonly int minDamage = 1;
    private readonly int maxDamage = 6;

    private readonly int minHealth = 10;
    private readonly int maxHealth = 20;

    private readonly int minVelocity = 1;
    private readonly int maxVelocity = 3;

    public readonly BotInfo botInfo = new BotInfo();
    /// <summary>
    /// It's a sign from ITarget
    /// </summary>
    public Bot targetSign => this;

    public Vector3 position => transform.position;

    public int DelayForDoDamage => 2;

    public bool canDamage { get; set; } = true;
    public UnityEvent TargetLost { get; set; }
    public bool isStaying { get; set; }
    public UnityEvent<int> scoreChanged { get; set; }
    public UnityEvent<int> healthChanged { get; set; }

    private readonly BotTargetSearcher botTargetSearcher = new BotTargetSearcher();

    private NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;

    void Awake()
    {
        TargetLost = new UnityEvent();
        scoreChanged = new UnityEvent<int>();
        healthChanged = new UnityEvent<int>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
    }

    void Start()
    {
        SetBotData();
        FindTarget();
    }
    /// <summary>
    /// Set damage, health, velocity to a bot. Change navMeshAgent's speed. Update HealthScore in UI
    /// </summary>
    public void SetBotData()
    {
        botInfo.SetDamage(minDamage,maxDamage);
        botInfo.SetHealth(minHealth,maxHealth);
        healthChanged?.Invoke(botInfo.health);
        botInfo.SetVelocity(minVelocity,maxVelocity);
        navMeshAgent.speed = botInfo.velocity;
    }
    /// <summary>
    /// Set a material to a bot
    /// </summary>
    /// <param name="material">Material</param>
    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }
    /// <summary>
    /// This method is needed in order to not adding yourself to the targets
    /// </summary>
    /// <param name="searcher"></param>
    /// <returns></returns>
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
                        StartCoroutine(DoDamage(botTargetSearcher.BotTarget,this));
            }
            else
            {
                if (!botTargetSearcher.BotTarget.targetSign.botInfo.isAlive) return;
                Move();
            }
        }
        else
        {
            if(!isStaying)
                FindTarget();
        }
    }
    /// <summary>
    /// One bot sends another bot a damage (with delay)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="sender"></param>
    /// <returns></returns>
    public IEnumerator DoDamage(ITarget<Bot> target, Bot sender)
    {
        target.targetSign.TakeDamage(botInfo.damage,sender);
        canDamage = false;
        yield return new WaitForSeconds(DelayForDoDamage);
        canDamage = true;
    }
    /// <summary>
    /// This method increases your bot's health. If a health is lower than 0, bot will be destroyed. Also the sender
    /// (who destroyed this bot) will increase it's score
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="sender"></param>
    public void TakeDamage(int damage,Bot sender)
    {
        var isHealthMoreZero = (botInfo.health - damage) > 0;
        if (isHealthMoreZero)
        {
            botInfo.DecreaseHealth(damage);
            healthChanged?.Invoke(botInfo.health);
        }
        else
        {
            botInfo.SetHealthToZero();
            healthChanged?.Invoke(botInfo.health);
            botInfo.isAlive = false;
            sender.botInfo.IncreaseScore();
            sender.scoreChanged?.Invoke(sender.botInfo.score);
            TargetLost?.Invoke();
            Destroy(gameObject); // To Objects pool
        }
    }
    /// <summary>
    /// Finding a target. If there are not targets bot will stay, otherwise will start moving to the nearest target.
    /// </summary>
    public void FindTarget()
    {
        var targets = botTargetSearcher.GetTargets();
        if (targets.Count == 0)
        {
            isStaying = true;
            return;
        }
        botTargetSearcher.GetNearestTarget(transform.position, targets);
        botTargetSearcher.BotTarget.targetSign.TargetLost.AddListener(TargetSign_TargetLost);
        Move();
    }
    
    private void TargetSign_TargetLost()
    {
        botTargetSearcher.BotTarget.targetSign.TargetLost.RemoveListener(TargetSign_TargetLost);
        botTargetSearcher.hasTarget = false;
    }
    /// <summary>
    /// Method to move bot to the destination with NavMesh components
    /// </summary>
    public void Move()
    {
        if (navMeshAgent.CalculatePath(transform.position, navMeshPath))
            navMeshAgent.SetDestination(botTargetSearcher.BotTarget.position);
    }
}

