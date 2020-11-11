using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public int damage = 1;
    
    void Start()
    {
        Move();
    }

    public void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minSpeed, maxSpeed), 
            Random.Range(minSpeed, maxSpeed));
    }
    
    public void Move(int minspeed, int maxspeed)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minspeed, maxspeed), 
            Random.Range(minspeed, maxspeed));
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Block>())
        {
            other.gameObject.GetComponent<Block>().RegistredDamage(damage);
            BallController.Instance.DamageControll(gameObject.transform);
        }
        else if (other.gameObject.GetComponent<Border>() && other.gameObject.GetComponent<Border>().isLose)
        {
            gameObject.SetActive(false);
            BallController.Instance.CheckLose();
        }
    }
}
