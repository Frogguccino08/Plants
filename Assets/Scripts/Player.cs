using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 1f;
    public GameObject bala;

    void Update()
    {
        Mover();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Atirar();
        }
    }

    void Mover()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        transform.position = new Vector3(transform.position.x + moveX * speed * Time.deltaTime, transform.position.y + moveY * speed * Time.deltaTime, transform.position.z);
    }

    void Atirar()
    {
        Instantiate(bala, this.transform.position, Quaternion.identity);
    }
}