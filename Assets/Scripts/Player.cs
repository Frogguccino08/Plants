using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Modo de jogo (True == Combate ; False == Calmo) e se está mirando
    public bool modo = true;
    public bool isMirando = false;


    //Variáveis do Player
    public int vida = 50;
    public bool isHolding = false;
    public GameObject inHands;


    //Ataque físico
    public int attackType; // 0 == Espada, 1 == Martelo, 2 == Soco
    public int danoFis = 5;
    float recargaCorte = 0.5f;
    public bool podeCortar = true;
    public bool atacando = false;


    //Movimento & dash
    public int velDash = 13;
    float speed = 1f;
    float moveX = 0;
    float moveY = 0;
    float recargaDash = 0.5f;
    public bool dandoDash = false;
    public bool podeDash = true;


    //Variaveis do tiro
    public float sumir = 2f;
    public int recoil;
    public float forcaBala = 3f;
    public float spread = 5;
    public int quantBala = 5;
    public int danoDis = 5;
    float recargaTiro = 0.6f;
    public bool podeAtirar = true;


    //Objetos e Imagens para o jogador funcionar
    public Rigidbody2D rb;
    Vector3 mousePos;
    Camera mainCam;
    public Aim mira;
    public Sprite[] imgs = new Sprite[3];
    public GameObject colisao;



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

        if (Input.GetKeyDown(KeyCode.Q) && inHands != null)
        {
            inHands.transform.SetParent(null);
            inHands.GetComponent<SpriteRenderer>().sortingOrder = 0;
            if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
            inHands = null;
        }

        if (Input.GetKeyUp(KeyCode.E) && inHands == null)
        {
            if (colisao.GetComponent<PlayerInteracao>().obj.Count > 0)
            {
                inHands = colisao.GetComponent<PlayerInteracao>().obj[0];
                colisao.GetComponent<PlayerInteracao>().obj[0].transform.SetParent(transform);
                colisao.GetComponent<PlayerInteracao>().obj[0].GetComponent<SpriteRenderer>().sortingOrder = 3;
                colisao.GetComponent<PlayerInteracao>().obj[0].transform.localPosition = Vector3.zero;

                if (colisao.GetComponent<PlayerInteracao>().obj[0].transform.childCount > 0)
                {
                    colisao.GetComponent<PlayerInteracao>().obj[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && inHands != null && colisao.GetComponent<PlayerInteracao>().obj.Count > 0)
        {
            if (colisao.GetComponent<PlayerInteracao>().obj[0].GetComponent<Vaso>() != null && inHands.GetComponent<Plantas>() != null)
            {
                colisao.GetComponent<PlayerInteracao>().obj[0].GetComponent<Vaso>().semente = inHands;
                inHands.transform.SetParent(colisao.GetComponent<PlayerInteracao>().obj[0].transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = -1;
                //inHands.transform.localPosition = new Vector3(0, colisao.GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
                inHands = null;
                StartCoroutine(colisao.GetComponent<PlayerInteracao>().obj[0].GetComponent<Vaso>().Crescer());
            }
        }

        //Modo Ataque
            if (modo == true)
            {
                if (Input.GetKeyUp(KeyCode.Space) && podeDash == true)
                {
                    dandoDash = true;
                    StartCoroutine(Dash());
                }

                if (dandoDash == true)
                {
                    StartCoroutine(RecargaDash());
                    GetComponent<TrailRenderer>().enabled = true;
                    rb.linearVelocity = new Vector2(moveX, moveY) * velDash;
                    return;
                }

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    isMirando = true;
                    //Cursor.SetCursor(imgs[1].texture, Vector2.zero, CursorMode.Auto);
                    Debug.Log("Mirando");
                }

                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    isMirando = false;
                    //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    Debug.Log("Parou de mirar");
                }

                if (Input.GetKeyUp(KeyCode.Mouse0) && podeAtirar == true && isMirando == true)
                {
                    StartCoroutine(RecargaTiro());

                    for (int i = 0; i < quantBala; i++)
                    {
                        mira.Atirar();
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0) && podeCortar == true && isMirando == false)
                {
                    StartCoroutine(RecargaCorte());
                    atacando = true;
                    mira.Fisico();
                }
            }

        //Modo Calmo
        if (modo == false)
        {
            
        }
    }

    void Mover()
    {
        if (dandoDash == false && podeCortar == true)
        {
            moveX = Input.GetAxisRaw("Horizontal") * speed;
            moveY = Input.GetAxisRaw("Vertical") * speed;
        }

        ColisaoInteracao();

        if (atacando == true)
        {
            moveX = 0;
            moveY = 0;
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
        GetComponent<TrailRenderer>().enabled = false;
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

    void ColisaoInteracao()
    {
        if (moveX < 0)
        {
            colisao.transform.localPosition = new Vector3(-0.15f, 0, 0);
        }

        if (moveX > 0)
        {
            colisao.transform.localPosition = new Vector3(0.15f, 0, 0);
        }

        if (moveY < 0)
        {
            colisao.transform.localPosition = new Vector3(0, -0.15f, 0);
        }

        if (moveY > 0)
        {
            colisao.transform.localPosition = new Vector3(0, 0.15f, 0);
        }
    }
}