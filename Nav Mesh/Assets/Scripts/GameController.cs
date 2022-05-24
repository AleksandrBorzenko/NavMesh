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
        for (int i = 0; i < botNumber; i++)
        {
            SpawnBot(spawnZones[i].position,i);
        }
    }

    void SpawnBot(Vector3 spawnZone)
    {
        var bot = Instantiate(botPrefab, spawnZone,Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetRandom());
        bot.GetComponent<Bot>().SetBotData();
    }

    void SpawnBot(Vector3 spawnZone, int materialIndex)
    {
        Vector3 spawnVec = new Vector3(spawnZone.x, 0, spawnZone.z);
        var bot = Instantiate(botPrefab, spawnVec, Quaternion.identity, botContainer);
        bot.GetComponent<Bot>().SetMaterial(botMaterialStorage.GetMaterialByIndex(materialIndex));
        bot.GetComponent<Bot>().SetBotData();
    }
}
