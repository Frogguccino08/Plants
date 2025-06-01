using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    Camera mainCam;
    Vector3 mousePos;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    public void Quebro()
    {
        Destroy(gameObject);
    }
}