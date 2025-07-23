using System.Collections;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Collider2D colisao;
    bool podePegar = true;

    void Awake()
    {
        colisao = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (podePegar == true && collision.gameObject.CompareTag("Player") && collision.GetComponent<Player>().inHands == null && collision.GetComponent<Player>().modo == false)
        {
            collision.GetComponent<Player>().inHands = gameObject;
            transform.SetParent(collision.transform);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            transform.localPosition = Vector3.zero;
            //No player escolhe um botão para botar o objeto de volta no chão
        */
    }

    public IEnumerator Largado()
    {
        podePegar = false;
        yield return new WaitForSeconds(2);
        podePegar = true;
    }
}
