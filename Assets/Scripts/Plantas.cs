using UnityEngine;

public class Plantas : MonoBehaviour
{
    //Variáveis padrão da planta
    public string nome;

    //Variáveis que a planta precisa para crescer
    public Sprite[] fases = new Sprite[3];

    public int[] tempo = new int[2];
    public int tempoAtual;

    //Variáveis que a planta precisa para colher armas
    public GameObject coletável;
}
