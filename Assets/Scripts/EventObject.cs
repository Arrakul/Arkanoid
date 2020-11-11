using Controllers;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EventObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Platform>())
        {
            Debug.Log("Prize!");
            EventController.Instance.EventGeneration();
        }
        else if (other.GetComponent<Border>())
        {
            Destroy(gameObject);
        }

    }
}
