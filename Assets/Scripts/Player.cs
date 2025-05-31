using UnityEngine;

public class Player : MonoBehaviour
{
    public Aim mira;
    float speed = 1f;

    public Rigidbody2D rb;

    void Update()
    {
        Mover();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mira.Atirar();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mira.Cortar();
        }
    }

    void Mover()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * speed;
        float moveY = Input.GetAxisRaw("Vertical") * speed;

        //transform.position = new Vector3(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime, transform.position.z);
        rb.linearVelocity = new Vector2(moveX, moveY);
    }
}