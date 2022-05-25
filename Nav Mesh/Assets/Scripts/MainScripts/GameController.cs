using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the game process
/// </summary>
public class GameController : MonoBehaviour
{
    private readonly string bot = "Bot ";
    /// <summary>
    /// It's a value to count bots
    /// </summary>
    private int botOrder = 1;

    private BotMaterialStorage botMaterialStorage;
    /// <summary>
    /// The zones in which bots will be spawned
    /// </summary>
    [SerializeField] private Transform[] spawnZones;
    /// <summary>
    /// Prefab which is loaded from resources
    /// </summary>
    private GameObject botPrefab;
    /// <summary>
    /// Transform which contains all bots
    /// </summary>
    [SerializeField] private Transform botContainer;
    /// <summary>
    /// The number of bots will be spawned at start
    /// </summary>
    [SerializeField]private int botNumber;

    /// <summary>
    /// Caching main camera
    /// </summary>
    private Camera cam;
    /// <summary>
    /// Event which is called when new bot added on scene
    /// </summary>
    public UnityEvent NewBotAdded;

    public readonly ObjectsPool objectsPool = new ObjectsPool();

    void Awake()
    {
        cam = Camera.main;
        botMaterialStorage = new BotMaterialStorage();
        botMaterialStorage.AddMaterials();
        botPrefab = Resources.Load<GameObject>("Prefabs/Bot");
    }
    /// <summary>
    /// Spawning bots
    /// </summary>
    void Start()
    {
        int spawnNumber = 0;
        for (int i = 0; i < botNumber; i++)
        {
            if (spawnNumber >= spawnZones.Length) spawnNumber = 0;
            SpawnBot(spawnZones[spawnNumber].position,i);
            spawnNumber++;
        }
    }
    /// <summary>
    /// This method will spawn bot from clicking at the screen
    /// </summary>
    /// <param name="spawnZone">Hit position</param>
    void SpawnBot(Vector3 spawnZone)
    {
        var bot = Instantiate(botPrefab, spawnZone,Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetRandom());
        bot.GetComponent<Bot>().InitializeGameController(this);
        bot.GetComponent<Bot>().botInfo.SetName(this.bot+botOrder);
        botOrder++;
    }
    /// <summary>
    /// Spawns a bot with determined material
    /// </summary>
    /// <param name="spawnZone">Zones on the scene</param>
    /// <param name="materialIndex">To call a method GetMaterialByIndex</param>
    void SpawnBot(Vector3 spawnZone, int materialIndex)
    {
        Vector3 spawnVec = new Vector3(spawnZone.x, 0, spawnZone.z);
        var bot = Instantiate(botPrefab, spawnVec, Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetMaterialByIndex(materialIndex));
        bot.GetComponent<Bot>().InitializeGameController(this);
        bot.GetComponent<Bot>().botInfo.SetName(this.bot + botOrder);
        botOrder++;
    }
    /// <summary>
    /// Spawns a bot from the pool
    /// </summary>
    /// <param name="spawnZone">Hit position</param>
    void SpawnBotFromPool(Vector3 spawnZone)
    {
        objectsPool.BotsList[0].SetBotData();
        objectsPool.BotsList[0].transform.position = spawnZone;
        objectsPool.BotsList[0].gameObject.SetActive(true);
        objectsPool.BotsList[0].FindTarget();
        objectsPool.RemoveBot();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI())
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (!hit.collider.TryGetComponent(out NotPlacebleArea notPlaceble))
                {
                    if(objectsPool.isEmpty)
                        SpawnBot(hit.point);
                    else
                    {
                        SpawnBotFromPool(hit.point);
                    }
                    NewBotAdded?.Invoke();

                }
            }
        }
    }

    private bool isMouseOverUI()
    {
#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();

#elif UNITY_ANDROID
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
    }
}
