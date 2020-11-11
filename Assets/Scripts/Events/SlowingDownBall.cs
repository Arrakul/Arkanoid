using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDownBall : MonoBehaviour
{
    private Ball _ball;
    
    public int maxSpeed = 2;
    public int minSpeed = 1;
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
        _ball.Move(minSpeed, maxSpeed);
        
        while (timer < accelerationTime) 
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
        }
        
        _ball.Move();
    }
}
