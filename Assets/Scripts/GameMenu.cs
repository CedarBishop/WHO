
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Image targetImage;
    public Text levelText;
    public Text timerText;

    SpriteRenderer enemySpriteRenderer;
    GameManager gameManager;

    float timer;
    bool hasInitialized;

    IEnumerator Start()
    {
        GameManager.UpdateUI += UpdateUI;
        hasInitialized = false;
        yield return new WaitForSeconds(0.05f);
        gameManager = GameManager.instance;
        timer = gameManager.timeToBeatLevel;
        enemySpriteRenderer = gameManager.enemyVillager.GetComponent<SpriteRenderer>();
        targetImage.sprite = enemySpriteRenderer.sprite;
        targetImage.color = enemySpriteRenderer.color;
        UpdateUI();
        hasInitialized = true;
    }

    void Update ()
    {
        if (hasInitialized)
        {
            if (timer <= 0.0f)
            {
                gameManager.TimeIsUp();
            }
            else
            {
                timer -= Time.deltaTime;
            }
            timerText.text = timer.ToString("F1");
        }        
    }

    void OnDestroy ()
    {
        GameManager.UpdateUI -= UpdateUI;
    }
    
    void UpdateUI ()
    {
        print( gameManager.GetStrikes());
        levelText.text = "Level " + gameManager.level;
    }
}
