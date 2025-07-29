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
            if (collision.transform.parent.GetComponent<Player>().inventario[0] != null)
            {
                collision.transform.parent.GetComponent<Player>().inventario[0].GetComponent<PickUpItem>().quant -= 1;
            }
            
            vida -= collision.GetComponent<BasicAttack>().dano;
            Debug.Log("Golpeado " + collision.GetComponent<BasicAttack>().dano + " Dano");
        }
        else if (collision.gameObject.CompareTag("Distancia"))
        {
            vida -= collision.GetComponent<Bala>().dano;
            Debug.Log("Atirado " + collision.GetComponent<Bala>().dano + " Dano");
            Destroy(collision.gameObject);
        }
    }
}
