using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Modo de jogo (True == Combate ; False == Calmo) e se está mirando
    public bool modo = true;
    public bool isMirando = false;


    //Variáveis do Player
    public int vida = 50;

    //Inventário e interação
    public GameObject inHands;
    public GameObject[] inventario = new GameObject[9];
    public int itemAtual;
    public bool segurandoPesado = false;



    //Ataque físico
    public int attackType = 0; // 0 == Espada, 1 == Martelo, 2 == Soco
    public int danoFis = 1;
    public float recargaCorte = 0.5f;
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
    public float sumir = 1f;
    public int recoil = 0; //fazer ainda
    public float forcaBala = 3f;
    public float spread = 0;
    public int quantBala = 1;
    public int danoDis = 1;
    float recargaTiro = 0.5f;
    public bool podeAtirar = true;


    //Objetos e Imagens para o jogador funcionar
    public Rigidbody2D rb;
    Vector3 mousePos;
    Camera mainCam;
    public Aim mira;
    public Sprite[] imgs = new Sprite[3];
    public GameObject colisao;
    public PlayerInteracao interacao;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        interacao = colisao.GetComponent<PlayerInteracao>();
    }

    void Update()
    {
        if (inventario[0] != null)
        {
            if (inventario[0].GetComponent<PickUpItem>().quant <= 0)
            {
                inventario[0] = null;
            }
        }
        //Todos os modos usam
        Mover();
        TrocarModo();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        EmMaos();
        Largar();
        Interagir();

        if (Input.GetKeyUp(KeyCode.F))
        {
            for (int i = 0; i < 9; i++)
            {
                if(inventario[i] != null)
                    Debug.Log(inventario[i].GetComponent<PickUpItem>().nome + " " + inventario[i].GetComponent<PickUpItem>().quant);
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
                    Debug.Log("Mirando");
                }

                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    isMirando = false;
                    Debug.Log("Parou de mirar");
                }

                if (Input.GetKeyUp(KeyCode.Mouse0) && podeAtirar == true && isMirando == true && inventario[1] != null)
                {
                    StartCoroutine(RecargaTiro());
                    inventario[1].GetComponent<PickUpItem>().quant -= 1;

                    for (int i = 0; i < quantBala; i++)
                    {
                        mira.Atirar();
                    }
                    
                    if(inventario[1].GetComponent<PickUpItem>().quant <= 0)
                    {
                        inventario[1] = null;
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
        if (dandoDash == false && atacando == false)
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
        if (inventario[0] == null)
        {
            yield return new WaitForSeconds(recargaCorte);
        }
        else
        {
            yield return new WaitForSeconds(inventario[0].GetComponent<ColetalFisico>().recargaCorte);
        }
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

    void Interagir()
    {
        if (Input.GetKeyUp(KeyCode.E) && interacao.obj.Count > 0 && segurandoPesado == false)
        {
            //Colocar planta no vaso
            if (interacao.obj[0].GetComponent<Vaso>() != null && inHands != null)
            {
                if (inHands.GetComponent<Plantas>())
                {
                    interacao.obj[0].GetComponent<Vaso>().semente = inHands;
                    inHands.transform.SetParent(colisao.GetComponent<PlayerInteracao>().obj[0].transform);
                    inHands.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    if (inHands.GetComponent<PickUpItem>().quant == 1)
                        inHands = null;
                    else if (inHands.GetComponent<PickUpItem>().quant > 1)
                        inHands.GetComponent<PickUpItem>().quant -= 1;
                    StartCoroutine(interacao.obj[0].GetComponent<Vaso>().Crescer());

                    return;
                }
            }
        }

        //Interagir com a planta crescida
        if (Input.GetKeyUp(KeyCode.E) && interacao.obj.Count > 0 && segurandoPesado == false)
        {
            if (interacao.obj[0].GetComponent<Vaso>() != null)
            {
                if (interacao.obj[0].GetComponent<Vaso>().coletavel == true)
                {
                    GameObject obj;

                    interacao.obj[0].GetComponent<Vaso>().coletavel = false;
                    StartCoroutine(interacao.obj[0].GetComponent<Vaso>().Crescer());

                    for (int i = 0; i <= UnityEngine.Random.Range((interacao.obj[0].GetComponent<Vaso>().plantaAtual.menorQuant), (interacao.obj[0].GetComponent<Vaso>().plantaAtual.maiorQuant + 1)); i++)
                    {
                        Debug.Log("Supostamente invocou");
                        obj = Instantiate(interacao.obj[0].GetComponent<Vaso>().plantaAtual.coletavel, new Vector2(interacao.obj[0].transform.position.x, interacao.obj[0].transform.position.y) + UnityEngine.Random.insideUnitCircle * 0.15f, quaternion.identity);
                        obj.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }

                    return;
                }
            }
        }
        

        if (Input.GetKeyUp(KeyCode.E) && interacao.obj.Count > 0 && segurandoPesado == false)
        {
            //Coletavel arma física
            if (interacao.obj[0].GetComponent<ColetalFisico>() != null && interacao.obj[0].GetComponent<ColetalFisico>().attackType == attackType)
            {
                if (inventario[0] == null)
                {
                    inventario[0] = interacao.obj[0];
                    interacao.obj[0].transform.SetParent(transform);
                    interacao.obj[0].transform.localPosition = Vector3.zero;
                    interacao.obj[0].GetComponent<SpriteRenderer>().enabled = false;
                    return;
                }
                else
                {
                    if (inventario[0].GetComponent<PickUpItem>().nome == interacao.obj[0].GetComponent<PickUpItem>().nome)
                    {
                        inventario[0].GetComponent<PickUpItem>().quant += interacao.obj[0].GetComponent<PickUpItem>().quant;
                        Destroy(interacao.obj[0]);
                        Debug.Log(inventario[0].name + " Quant: " + inventario[0].GetComponent<PickUpItem>().quant);
                        return;
                    }
                }
            }

            //Coletavel arma a distância
            if (interacao.obj[0].GetComponent<ColetalDistancia>() != null)
            {
                if (inventario[1] == null)
                {
                    inventario[1] = interacao.obj[0];
                    interacao.obj[0].transform.SetParent(transform);
                    interacao.obj[0].transform.localPosition = Vector3.zero;
                    interacao.obj[0].GetComponent<SpriteRenderer>().enabled = false;

                    danoDis = inventario[1].GetComponent<ColetalDistancia>().danoDis;
                    recoil = inventario[1].GetComponent<ColetalDistancia>().recoil;
                    forcaBala = inventario[1].GetComponent<ColetalDistancia>().forcaBala;
                    spread = inventario[1].GetComponent<ColetalDistancia>().spread;
                    quantBala = inventario[1].GetComponent<ColetalDistancia>().quantBala;
                    sumir = inventario[1].GetComponent<ColetalDistancia>().sumir;
                    recargaTiro = inventario[1].GetComponent<ColetalDistancia>().recargaTiro;
                    return;
                }
                else
                {
                    if (inventario[1].GetComponent<PickUpItem>().nome == interacao.obj[0].GetComponent<PickUpItem>().nome)
                    {
                        inventario[1].GetComponent<PickUpItem>().quant += interacao.obj[0].GetComponent<PickUpItem>().quant;
                        Destroy(interacao.obj[0]);
                        Debug.Log(inventario[1].name + " Quant: " + inventario[1].GetComponent<PickUpItem>().quant);

                        danoDis = inventario[1].GetComponent<ColetalDistancia>().danoDis;
                        recoil = inventario[1].GetComponent<ColetalDistancia>().recoil;
                        forcaBala = inventario[1].GetComponent<ColetalDistancia>().forcaBala;
                        spread = inventario[1].GetComponent<ColetalDistancia>().spread;
                        quantBala = inventario[1].GetComponent<ColetalDistancia>().quantBala;
                        sumir = inventario[1].GetComponent<ColetalDistancia>().sumir;
                        recargaTiro = inventario[1].GetComponent<ColetalDistancia>().recargaTiro;
                        return;
                    }
                }
            }

            //Coleta Item normal ou caso não tenha espaço na arma    
            for (int i = 2; i < 7; i++)
            {
                if (inventario[i] != null)
                {
                    if (inventario[i].GetComponent<PickUpItem>().nome == interacao.obj[0].GetComponent<PickUpItem>().nome && inventario[i].GetComponent<PickUpItem>().quant < inventario[i].GetComponent<PickUpItem>().max)
                    {
                        inventario[i].GetComponent<PickUpItem>().quant += interacao.obj[0].GetComponent<PickUpItem>().quant;
                        Destroy(interacao.obj[0]);
                        Debug.Log(inventario[i].name + " Quant: " + inventario[i].GetComponent<PickUpItem>().quant);
                        return;
                    }
                }
            }

            for (int i = 2; i < 7; i++)
            {
                if (inventario[i] == null)
                {
                    inventario[i] = interacao.obj[0];
                    interacao.obj[0].transform.SetParent(transform);
                    interacao.obj[0].transform.localPosition = Vector3.zero;
                    interacao.obj[0].GetComponent<SpriteRenderer>().enabled = false;

                    if (i == 2)
                    {
                        inHands = inventario[i];
                        itemAtual = i;
                        interacao.obj[0].GetComponent<SpriteRenderer>().enabled = true;
                        inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }

                    if (inventario[i].GetComponent<PickUpItem>().pesado == true)
                    {
                        inHands = inventario[i];
                        if (inventario[i].transform.childCount > 0)
                        {
                            inventario[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                        }

                        itemAtual = i;
                        segurandoPesado = true;
                        interacao.obj[0].GetComponent<SpriteRenderer>().enabled = true;
                        inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }
                    return;
                }
            }
        }
    }

    void EmMaos()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && segurandoPesado == false)
        {
            if(inHands != null)
                inHands.GetComponent<SpriteRenderer>().enabled = false;
            itemAtual = 2;
            inHands = inventario[2];
            if (inHands != null)
            {
                inHands.GetComponent<SpriteRenderer>().enabled = true;
                inHands.transform.SetParent(transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                inHands.transform.localPosition = Vector3.zero;

                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && segurandoPesado == false)
        {
            if(inHands != null)
                inHands.GetComponent<SpriteRenderer>().enabled = false;
            itemAtual = 3;
            inHands = inventario[3];
            if (inHands != null)
            {
                inHands.GetComponent<SpriteRenderer>().enabled = true;
                inHands.transform.SetParent(transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                inHands.transform.localPosition = Vector3.zero;

                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3) && segurandoPesado == false)
        {
            if(inHands != null)
                inHands.GetComponent<SpriteRenderer>().enabled = false;
            itemAtual = 4;
            inHands = inventario[4];
            if (inHands != null)
            {
                inHands.GetComponent<SpriteRenderer>().enabled = true;
                inHands.transform.SetParent(transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                inHands.transform.localPosition = Vector3.zero;

                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4) && segurandoPesado == false)
        {
            if(inHands != null)
                inHands.GetComponent<SpriteRenderer>().enabled = false;
            itemAtual = 5;
            inHands = inventario[5];
            if (inHands != null)
            {
                inHands.GetComponent<SpriteRenderer>().enabled = true;
                inHands.transform.SetParent(transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                inHands.transform.localPosition = Vector3.zero;

                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5) && segurandoPesado == false)
        {
            if(inHands != null)
                inHands.GetComponent<SpriteRenderer>().enabled = false;
            itemAtual = 6;
            inHands = inventario[6];
            if (inHands != null)
            {
                inHands.GetComponent<SpriteRenderer>().enabled = true;
                inHands.transform.SetParent(transform);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 3;
                inHands.transform.localPosition = Vector3.zero;

                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
    }

    void Largar()
    {
        if (inHands != null)
        {
            if (Input.GetKeyDown(KeyCode.Q) && inHands.GetComponent<PickUpItem>().quant == 1)
            {
                segurandoPesado = false;
                inventario[itemAtual] = null;
                inHands.transform.SetParent(null);
                inHands.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (inHands.transform.childCount > 0)
                {
                    inHands.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
                inHands = null;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && inHands.GetComponent<PickUpItem>().quant > 1)
            {
                segurandoPesado = false;
                GameObject obj;
                inHands.GetComponent<PickUpItem>().quant -= 1;
                obj = Instantiate(inHands.GetComponent<PickUpItem>().prefab, transform);
                obj.transform.SetParent(null);
                obj.GetComponent<SpriteRenderer>().sortingOrder = 0;
                obj.GetComponent<PickUpItem>().quant = 1;
            }
        }
    }
}