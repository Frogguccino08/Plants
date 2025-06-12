using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Variáveis do Inimigo
    public int vida = 50;
    public int danoFis = 5;
    public int danoDis = 5;
    public int velDash = 13;


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
