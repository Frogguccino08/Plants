using UnityEngine;

public class Player : MonoBehaviour
{
    public Aim mira;
    float speed = 1f;

    void Update()
    {
        Mover();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mira.Atirar();
        }
    }

    void Mover()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        transform.position = new Vector3(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime, transform.position.z);
    }
}