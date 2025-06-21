using UnityEngine;

public class Aim : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;
    public GameObject bala;
    public GameObject corte;
    public Transform transformBala;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public void Atirar()
    {
        Instantiate(bala, transformBala.position, Quaternion.identity);
    }

    public void Cortar()
    {
        Instantiate(corte, player.transform.position, Quaternion.identity);
    }

    public void PegarItem()
    {
        
    }
}
