using UnityEngine;

public class Aim : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;
    public GameObject bala;
    public Transform transformBala;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        Instantiate(bala, transformBala.position, Quaternion.identity);
    }
}
