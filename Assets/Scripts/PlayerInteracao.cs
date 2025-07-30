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
        if (collision.GetComponent<PickUpItem>() != null)
        {
            if (collision.GetComponent<Plantas>() != null && collision.transform.parent != null)
            {
                return;
            }
            obj.Add(collision.gameObject);
            Debug.Log(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
{
    if (collision.GetComponent<PickUpItem>() != null)
    {
        if (obj.Contains(collision.gameObject))
        {
            obj.Remove(collision.gameObject);
            Debug.Log("Removido: " + collision.gameObject.name);
        }
    }
}
}
