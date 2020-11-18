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
    public Transform gridObjects;
    public Transform borders;
    public Sprite sprite;

    [HideInInspector]public Platform platform;
    private Ball _ball;
    private int score;
    private int money;

    [Header("UI")]
    public GameObject panelLose;
    public GameObject panelPause;
    public Text coinsText;
    public Text moneyText;
    public int numberLevel;
    
    float offsetObjects = -0.775f;
    const int pointsPerHit = 10;
    const float sizeRow = 150f;
    const float pixelPerUnit = 100f;
    const float posBlocks = 2.4f;

    private Block[,] _blocks; 

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
        score = 0;
        money = 0;
        CreateScene();
    }

    private void HidePanels()
    {
        panelLose.SetActive(false);
        panelPause.SetActive(false);
    }

    private void CreateScene()
    {
        UpDateText(score, money);
        platform = Instantiate(platformPrefab);
        
        float ratio = (float) Screen.height/Screen.width;
        float ortSize = ratio / 20f;
        borders.localScale = new Vector2(1 - ortSize, 1);

        BallController.Instance.GetBall();
        CreateBlocks();
    }
    
    int countLR = 1;
    private void CreateBlocks()
    {
        var modelLevel = Parcer.Instance().LoadModels(numberLevel);
        _blocks = new Block[modelLevel.size.x, modelLevel.size.y];

        for (int j = 0; j < _blocks.GetLength(1); j++) 
        {
            for (int i = 0; i < _blocks.GetLength(0); i++) 
            {
                Block obj = _blocks[i, j];

                if (obj == null)
                {
                    obj = BlockController.Instance.GetBlock();
                    obj.boxCollider2D.isTrigger = true;
                    obj.transform.parent = gridObjects;
                }

                obj.x = i;
                obj.y = j;
                obj.transform.localPosition = new Vector2(obj.x*(sizeRow/pixelPerUnit + offsetObjects), obj.y*(sizeRow/pixelPerUnit + offsetObjects));
                obj.transform.localScale = new Vector3(1,1,1);
                _blocks[obj.x, obj.y] = obj;
            }
        }
        
        foreach (Parcer.BlockObject block in modelLevel.blocks)
        {
            Block obj = _blocks[block.x, block.y];
            obj.type = (Block.EBlockType) Enum.Parse(typeof (Block.EBlockType), block.type, true);
            obj.boxCollider2D.isTrigger = false;
            obj.spriteRender = obj.GetComponent<SpriteRenderer>();
            obj.ResetSettings();

            BlockController.Instance.countActiveBlocks++;
        }

        float width = (modelLevel.size.x + countLR*2)*(sizeRow + offsetObjects*pixelPerUnit);
        float height = (modelLevel.size.y)*(sizeRow + offsetObjects*pixelPerUnit);

        float screenHeight = Camera.main.orthographicSize * 2 * pixelPerUnit;
        float screenWidth = screenHeight * Camera.main.aspect;

        float koef = screenWidth < width ? screenWidth/width : 1;
        gridObjects.localScale = new Vector2(koef, koef);
        
        float newKoef = screenHeight < height*koef ? screenHeight/height*koef : 1;
        gridObjects.localScale = new Vector2(koef*newKoef*0.95f, koef*newKoef*0.95f);

        width -= (sizeRow + offsetObjects*pixelPerUnit);

        gridObjects.localPosition = new Vector3(
            -width/pixelPerUnit/3.5f*gridObjects.localScale.x,
            0,
            0);
        gridObjects.localPosition = new Vector2(gridObjects.localPosition.x, posBlocks);
    }

    public void UpDateText(int score, int money)
    {
        coinsText.text = "Score : " + score;
        moneyText.text = "Money : " + money;
    }

    public void UpDateCoins()
    {
        score += pointsPerHit;

        if ((score % 100) == 0)
        {
            money += 1;
        }
        
        if ((score % 1000) == 0)
        {
            BallController.Instance.GetBall();
        }
        
        UpDateText(score, money);
    }

    public void LoseGame()
    {
        panelLose.SetActive(true);
        SetPaused(true);
    }
    
    public void WinGame()
    {
        ++numberLevel;
        RestartGame();
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
        BlockController.Instance.countActiveBlocks = 0;
        BallController.Instance.HideAllBalls();
        Destroy(platform.gameObject);
        
        score = 0;
        money = 0;
        
        SetPaused(false);
        HidePanels();
        CreateScene();
    }

    public void StuckBall()
    {
        if (money >= 1)
        {
            money--;
            UpDateText(score, money);
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
