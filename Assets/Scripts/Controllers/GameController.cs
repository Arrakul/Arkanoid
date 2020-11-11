using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Platform platformPrefab;

    [HideInInspector]public Platform platform;
    private Ball _ball;
    private int coins;
    private int money;

    [Header("UI")]
    public GameObject panelLose;
    public GameObject panelPause;
    public Text coinsText;
    public Text moneyText;

    private const int pointsPerHit = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Start()
    {
        CreateScene();
    }

    private void HidePanels()
    {
        panelLose.SetActive(false);
        panelPause.SetActive(false);
    }

    private void CreateScene()
    {
        coins = 0;
        money = 0;
        coinsText.text = "Score : 0";
        moneyText.text = "Money : 0";
        platform = Instantiate(platformPrefab);

        BlockController.Instance.SpaunBlock();
        BallController.Instance.GetBall();
    }

    public void UpDateCoins()
    {
        coins += pointsPerHit;
        coinsText.text = "Score : " + coins;

        if ((coins % 100) == 0)
        {
            BlockController.Instance.SpaunBlock();
            money += 1;
            moneyText.text = "Money : " + money;
        }
        
        if ((coins % 1000) == 0)
        {
            BallController.Instance.GetBall();
        }
    }

    public void LoseGame()
    {
        panelLose.SetActive(true);
        SetPaused(true);
    }
    
    public void PauseGame(bool paused)
    {
        panelPause.SetActive(paused);
        SetPaused(paused);
    }
    
    public void ExitGame()
    {
        SetPaused(false);
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        BlockController.Instance.HideAllBlocks();
        BallController.Instance.HideAllBalls();
        Destroy(platform.gameObject);
        
        SetPaused(false);
        HidePanels();
        CreateScene();
    }

    public void StuckBall()
    {
        if (money >= 1)
        {
            money--;
            moneyText.text = "Money : " + money;
            BallController.Instance.HideAllBalls();
            BallController.Instance.GetBall();
        }
    } 
    
    public void SetPaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
