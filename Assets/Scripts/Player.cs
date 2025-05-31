using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 1f;

    void Update()
    {
        Mover();
    }

    void Mover()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        transform.position = new Vector3(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime, transform.position.z);
    }
}