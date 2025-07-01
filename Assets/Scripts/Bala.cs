using UnityEngine;

public class Bala : MonoBehaviour
{
    Vector3 mousePos;
    Camera mainCam;
    Rigidbody2D rb;
    float forcaBala;
    Player player;
    Vector3 variacao;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        variacao = new Vector3(UnityEngine.Random.Range(((player.spread * 0.01f) * -1), (player.spread * 0.01f)), UnityEngine.Random.Range(((player.spread * 0.01f) * -1), (player.spread * 0.01f)), 0);
        forcaBala = player.forcaBala;

        Vector3 direction = mousePos - transform.position - variacao;
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
