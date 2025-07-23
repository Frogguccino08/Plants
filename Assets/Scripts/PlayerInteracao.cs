using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteracao : MonoBehaviour
{
    public Player player;
    public List<GameObject> obj = new List<GameObject>();

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Fazer uma lista que adiciona o objeto nela caso tenha a tag Item
        //Clicar E no player adiciona o primeiro objeto da lista na mão do player
        Debug.Log(collision);
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.CompareTag("Item") && player.inHands == null && player.modo == false)
        {
            player.inHands = collision.gameObject;
            collision.transform.SetParent(player.transform);
            collision.GetComponent<SpriteRenderer>().sortingOrder = 2;
            collision.transform.localPosition = Vector3.zero;
        }
    }
}
