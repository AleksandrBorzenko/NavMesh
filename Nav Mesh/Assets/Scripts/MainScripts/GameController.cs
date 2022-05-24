using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the game process
/// </summary>
public class GameController : MonoBehaviour
{
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

    void Awake()
    {
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
    /// <param name="spawnZone">Input position</param>
    void SpawnBot(Vector3 spawnZone)
    {
        var bot = Instantiate(botPrefab, spawnZone,Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetRandom());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnZone">Zones on the scene</param>
    /// <param name="materialIndex">To call a method GetMaterialByIndex</param>
    void SpawnBot(Vector3 spawnZone, int materialIndex)
    {
        Vector3 spawnVec = new Vector3(spawnZone.x, 0, spawnZone.z);
        var bot = Instantiate(botPrefab, spawnVec, Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetMaterialByIndex(materialIndex));
    }
}
