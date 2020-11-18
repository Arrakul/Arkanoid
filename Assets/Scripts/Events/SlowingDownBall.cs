using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDownBall : MonoBehaviour
{
    private Ball _ball;
    
    public float cofficient = 2f;
    public int accelerationTime = 5;
    private void Start()
    {
        gameObject.GetComponent<EventGame>().onEvent += Play;
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<EventGame>().onEvent -= Play;
    }

    public void Play()
    {
        Debug.Log("SlowingDownBall");
        _ball = BallController.Instance.GetActiveBall().GetComponent<Ball>();
        StartCoroutine(AccelerationMove());
    }

    IEnumerator AccelerationMove()
    {
        float timer = 0f;
        _ball.Move(true, cofficient);
        
        while (timer < accelerationTime) 
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
        }
        
        _ball.Move();
    }
}
