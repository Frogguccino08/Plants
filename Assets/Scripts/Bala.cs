using UnityEngine;

public class Bala : MonoBehaviour
{
    Vector3 mousePos;
    Camera mainCam;
    Rigidbody2D rb;
    float forcaBala = 3f;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * forcaBala;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        if (transform.position.y >= 1.1f || transform.position.y <= -1.1f || transform.position.x >= 2.2f || transform.position.x <= -2.2f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}
