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
        if (collision.CompareTag("Item"))
        {
            Debug.Log(collision);
            obj.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
{
    if (collision.CompareTag("Item"))
    {
        if (obj.Contains(collision.gameObject))
        {
            obj.Remove(collision.gameObject);
            Debug.Log("Removido: " + collision.gameObject.name);
        }
    }
}
}
