using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Aim mira;
    public Sprite[] imgs = new Sprite[3];

    //Movimento & dash
    float speed = 1f;
    float moveX = 0;
    float moveY = 0;
    public bool dandoDash = false;
    Vector2 ondeTava;
    Vector2 ondeMouseTava;

    public Rigidbody2D rb;
    Vector3 mousePos;
    Camera mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyUp(KeyCode.Space) && dandoDash == false)
        {
            ondeTava = transform.position;
            ondeMouseTava = mousePos;
            dandoDash = true;
            StartCoroutine(Dash());
        }

        if (dandoDash == true)
        {
            rb.linearVelocity = new Vector2(ondeMouseTava.x - ondeTava.x, ondeMouseTava.y - ondeTava.y).normalized * 13;
            return;
        }
        
        Mover();
        

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mira.Atirar();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mira.Cortar();
        }
    }

    void Mover()
    {
        if (dandoDash == false)
        {
            moveX = Input.GetAxisRaw("Horizontal") * speed;
            moveY = Input.GetAxisRaw("Vertical") * speed;
        }

        if (moveX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (moveY < 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[0];
        }
        else if (moveY > 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[2];
        }

        if (moveX != 0)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[1];
        }

        rb.linearVelocity = new Vector2(moveX, moveY);
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.1f);
        dandoDash = false;
    }
}