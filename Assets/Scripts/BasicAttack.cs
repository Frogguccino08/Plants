using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;
    GameObject player;

    public int dano;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (player.GetComponent<Player>().inventario[0] != null)
        {
            dano = player.GetComponent<Player>().inventario[0].GetComponent<ColetalFisico>().danoFis;
        }
        else
        {
            dano = player.GetComponent<Player>().danoFis;
        }

        Vector3 rotation = transform.position - mousePos;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 200);
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    public void Quebro()
    {
        Destroy(gameObject);
    }

    public void AcabouAtaque()
    {
        player.GetComponent<Player>().atacando = false;
    }
}