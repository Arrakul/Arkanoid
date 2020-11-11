using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] [Range(10, 50)] private float speed = 15f;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    void Update()
    {
        transform.Translate(Vector3.right * (speed * Input.GetAxis("Horizontal") * Time.deltaTime));
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), 
            transform.position.y);
    }
}
