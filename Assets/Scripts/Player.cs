using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Modo de jogo (True == Combate ; False == Calmo)
    public bool modo = true;


    //Variáveis do Player
    public int vida = 50;
    public int danoFis = 5;
    public int danoDis = 5;
    public int velDash = 13;


    //Movimento & dash
    float speed = 1f;
    float moveX = 0;
    float moveY = 0;


    //Tempos de recarga
    float recargaCorte = 0.5f;
    float recargaTiro = 0.6f;
    float recargaDash = 0.5f;
    public bool dandoDash = false;
    public bool podeDash = true;
    public bool podeAtirar = true;
    public bool podeCortar = true;


    //Objetos e Imagens para o jogador funcionar
    public Rigidbody2D rb;
    Vector3 mousePos;
    Camera mainCam;
    public Aim mira;
    public Sprite[] imgs = new Sprite[3];


    //Para Ajudar com o dash
    Vector2 ondeTava;
    Vector2 ondeMouseTava;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //Todos os modos usam
        Mover();
        TrocarModo();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (modo == true)
        {
            if (Input.GetKeyUp(KeyCode.Space) && podeDash == true)
            {
                ondeTava = transform.position;
                ondeMouseTava = mousePos;
                dandoDash = true;
                StartCoroutine(Dash());
            }

            if (dandoDash == true)
            {
                StartCoroutine(RecargaDash());
                rb.linearVelocity = new Vector2(ondeMouseTava.x - ondeTava.x, ondeMouseTava.y - ondeTava.y).normalized * velDash;
                return;
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && podeAtirar == true)
            {
                StartCoroutine(RecargaTiro());
                mira.Atirar();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && podeCortar == true)
            {
                StartCoroutine(RecargaCorte());
                mira.Cortar();
            }
        }
    }

    void Mover()
    {
        if (dandoDash == false && podeCortar == true)
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
        yield return new WaitForSeconds(0.05f);
        dandoDash = false;
    }

    IEnumerator RecargaCorte()
    {
        podeCortar = false;
        yield return new WaitForSeconds(recargaCorte);
        podeCortar = true;
    }

    IEnumerator RecargaTiro()
    {
        podeAtirar = false;
        yield return new WaitForSeconds(recargaTiro);
        podeAtirar = true;
    }

    IEnumerator RecargaDash()
    {
        podeDash = false;
        yield return new WaitForSeconds(recargaDash);
        podeDash = true;
    }

    void TrocarModo()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (modo == true)
            {
                modo = false;
                Debug.Log("Modo Alterado para Calmo");
            }
            else
            {
                modo = true;
                Debug.Log("Modo alterado para agressivo");
            }
        }
    }
}