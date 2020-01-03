
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Image targetImage;
    public Text levelText;
    public Text timerText;
    public Image strikesImage;


    public string mainMenuScene = "MainMenu";
    SpriteRenderer enemySpriteRenderer;
    GameManager gameManager;

    float timer;
    bool hasInitialized;
    int strikes;

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
        strikesImage.fillAmount = 0.0f;
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
        strikes = gameManager.GetStrikes();
        if (strikes == 1)
        {
            strikesImage.fillAmount = 0.34f;
        }
        else if (strikes == 2)
        {
            strikesImage.fillAmount = 0.67f;
        }
        else if (strikes == 3)
        {
            strikesImage.fillAmount = 1.0f;
        }
        levelText.text = "Level " + gameManager.level;
    }

    public void ReturnToMainMenu ()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
