using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// This is a facade for bot's behaviour
/// </summary>
public class Bot : MonoBehaviour, ITarget<Bot>, IBotBehaviour,IPlayer
{
    /// <summary>
    /// Minimum and maximum damage
    /// </summary>
    private readonly int minDamage = 1;
    private readonly int maxDamage = 6;
    /// <summary>
    /// Minimum and maximum health
    /// </summary>
    private readonly int minHealth = 10;
    private readonly int maxHealth = 20;
    /// <summary>
    /// Minimum and maximum velocity
    /// </summary>
    private readonly int minVelocity = 1;
    private readonly int maxVelocity = 3;
    /// <summary>
    /// This module contains an information of bot (damage, health, score and etc.)
    /// </summary>
    public readonly BotInfo botInfo = new BotInfo();
    /// <summary>
    /// It's a sign from ITarget
    /// </summary>
    public Bot targetSign => this;
    /// <summary>
    /// Bot's current position
    /// </summary>
    public Vector3 position => transform.position;
    /// <summary>
    /// The cooldown time for an attack
    /// </summary>
    public int DelayForDoDamage => 2;
    /// <summary>
    /// Tells the bot if he can attack now
    /// </summary>
    public bool canDamage { get; set; } = true;
    /// <summary>
    /// Event called when the target of the bot is being lost
    /// </summary>
    public UnityEvent TargetLost { get; set; }
    /// <summary>
    /// If there are no targets isStaying will be true. Bot will stay and wait.
    /// </summary>
    public bool isStaying { get; set; }
    /// <summary>
    /// When bot destroys an opponent this event will be called
    /// </summary>
    public UnityEvent<int> scoreChanged { get; set; }
    /// <summary>
    /// When the health points of bot is changed this event will be called
    /// </summary>
    public UnityEvent<int> healthChanged { get; set; }

    private readonly BotTargetSearcher botTargetSearcher = new BotTargetSearcher();
    /// <summary>
    /// NavMesh components
    /// </summary>
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
    /// (who destroyed this bot) will increase it's score. If the bot is being destroyed it sends its damage to the bot who destroyed it.
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
            sender.botInfo.IncreaseDamage(botInfo.damage);
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
        botTargetSearcher.GetNearestTarget(position, targets);
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
        if (navMeshAgent.CalculatePath(position, navMeshPath))
            navMeshAgent.SetDestination(botTargetSearcher.BotTarget.position);
    }
}

