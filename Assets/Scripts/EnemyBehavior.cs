using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fisico"))
        {
            Debug.Log("Golpeado");
        }
        else if (collision.gameObject.CompareTag("Distancia"))
        {
            Debug.Log("Atirado");
            Destroy(collision.gameObject);
        }
    }
}
