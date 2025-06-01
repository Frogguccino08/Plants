using UnityEngine;

public class Player : MonoBehaviour
{
    public Aim mira;
    float speed = 1f;
    public Sprite[] imgs = new Sprite[3];

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

        if (moveX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (moveY < 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[0];
        }
        else if (moveY > 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[2];
        }

        if (moveX != 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[1];
        }

        rb.linearVelocity = new Vector2(moveX, moveY);
    }
}