using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Collider2D colisao;

    void Awake()
    {
        colisao = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Checa se ele já ta segurando alguma coisa
            //Torta player parente do objeto
            //Bota ele no centro da imagem
            //No player escolhe um botão para botar o objeto de volta no chão
        }
    }
}
