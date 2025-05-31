using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fisico")
        {
            Debug.Log("Golpeado");
        }
        else if (collision.gameObject.tag == "Distancia")
        {
            Debug.Log("Atirado");
            Destroy(collision.gameObject);
        }
    }
}
