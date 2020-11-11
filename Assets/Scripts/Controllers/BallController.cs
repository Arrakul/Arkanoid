using System.Collections;
using System.Collections.Generic;
using Arkanoid.Utils;
using UnityEngine;

public class BallController : Singleton<BallController>
{
    [SerializeField] private int numberBall = 10;
    [SerializeField] private GameObject ballPrefab;
    private List<GameObject> balls;
    private int countActiveBalls;
    
    public delegate void OnGame(Transform target);
    public event OnGame onDamageBlock;

    private void Awake()
    {
        CreateBalls();
    }

    private void CreateBalls()
    {
        balls = new List<GameObject>();
        balls = PoolManager.Instance.GetObjects(ballPrefab, numberBall, gameObject.transform);
    }
    
    private void CreateBall()
    {
        balls.Add(PoolManager.Instance.GetObject(ballPrefab, gameObject.transform));
    }

    public GameObject GetBall()
    {
        countActiveBalls++;

        foreach (var ball in balls)
        {
            if (!ball.activeSelf)
            {
                ball.SetActive(true);
                ball.transform.position = ballPrefab.transform.position;
                ball.GetComponent<Ball>().Move();
                return ball;
            }
        }

        CreateBall();
        var newBall = balls[balls.Count-1];
        newBall.SetActive(true);

        return newBall;
    }
    
    public GameObject GetActiveBall()
    {
        countActiveBalls++;

        foreach (var ball in balls)
        {
            if (ball.activeSelf)
            {
                return ball;
            }
        }

        return null;
    }

    public void HideAllBalls()
    {
        foreach (var ball in balls)
        {
            ball.SetActive(false);
        }

        countActiveBalls = 0;
    }

    public void CheckLose()
    {
        --countActiveBalls;

        if (countActiveBalls <= 0)
        {
            GameController.Instance.LoseGame();
        }
    }

    public void DamageControll(Transform target)
    {
        GameController.Instance.UpDateCoins();
        onDamageBlock?.Invoke(target);
    }
}
