using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Variáveis do Inimigo
    public int vida = 50;
    public int danoFis = 5;
    public int danoDis = 5;
    public int velDash = 13;

    void Update()
    {
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fisico"))
        {
            vida -= collision.GetComponent<BasicAttack>().dano;
            Debug.Log("Golpeado");
        }
        else if (collision.gameObject.CompareTag("Distancia"))
        {
            vida -= collision.GetComponent<Bala>().dano;
            Debug.Log("Atirado");
            Destroy(collision.gameObject);
        }
    }
}
