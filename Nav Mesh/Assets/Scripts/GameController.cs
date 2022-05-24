using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private BotMaterialStorage botMaterialStorage;
    [SerializeField] private Transform[] spawnZones;
    private GameObject botPrefab;
    [SerializeField] private Transform botContainer;
    [SerializeField]private int botNumber;

    void Awake()
    {
        botMaterialStorage = new BotMaterialStorage();
        botMaterialStorage.AddMaterials();
        botPrefab = Resources.Load<GameObject>("Prefabs/Bot");
    }

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

    void SpawnBot(Vector3 spawnZone)
    {
        var bot = Instantiate(botPrefab, spawnZone,Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetRandom());
    }

    void SpawnBot(Vector3 spawnZone, int materialIndex)
    {
        Vector3 spawnVec = new Vector3(spawnZone.x, 0, spawnZone.z);
        var bot = Instantiate(botPrefab, spawnVec, Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetMaterialByIndex(materialIndex));
    }
}
