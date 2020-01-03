using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int level = 1;
    public float timeToBeatLevel = 10;
    public Villagers enemyVillager;

    public static event System.Action UpdateUI;

    int strikes = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel ()
    {
        strikes = 0;
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLevels ()
    {
        strikes = 0;
        level = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public int GetVillagerAmountToSpawn()
    {
        int amount = 5 + (level * 2);
        return amount;
    }
    public int GetStrikes()
    {
        return strikes;
    }

    public void IncrementStrikes()
    {
        strikes++;

        if (strikes >= 3)
        {
            ResetLevels();
        }
        if (UpdateUI != null)
        {
            UpdateUI();
        }

    }

    public void TimeIsUp()
    {
        ResetLevels();
    }

}
