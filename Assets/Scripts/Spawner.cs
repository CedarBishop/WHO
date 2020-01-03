using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gameManager;
    public Villagers villagerPrefab;
    void Start()
    {
        gameManager = GameManager.instance;
        int amount = gameManager.GetVillagerAmountToSpawn();
        int randomIndex = Random.Range(0,amount);
        for (int i = 0; i < amount; i++)
        {
            Villagers v = Instantiate(villagerPrefab);
            if (i == randomIndex)
            {
                gameManager.enemyVillager = v;
            }
        }
    }
}
