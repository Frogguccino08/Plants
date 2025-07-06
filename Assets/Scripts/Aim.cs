using UnityEngine;

public class Aim : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;
    public GameObject bala;
    public Transform transformBala;
    public GameObject player;
    public Vector3 rotation;

    //Objetos ataques físicos
    public GameObject espada;
    public GameObject martelo;
    public GameObject soco;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        rotation = mousePos - transform.position;
        
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public void Atirar()
    {
        Instantiate(bala, transformBala.position, Quaternion.identity);
    }

    public void Fisico()
    {
        if (player.GetComponent<Player>().attackType == 0)
        {
            Instantiate(espada, player.transform.position, Quaternion.identity);
        }else if (player.GetComponent<Player>().attackType == 1)
        {
            Instantiate(martelo, player.transform.position, Quaternion.identity);
        }else if (player.GetComponent<Player>().attackType == 2)
        {
            Instantiate(soco, player.transform.position, Quaternion.identity);
        }
        
    }

    public void PegarItem()
    {
        
    }
}
