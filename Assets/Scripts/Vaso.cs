using System.Collections;
using UnityEngine;

public class Vaso : MonoBehaviour
{
    public GameObject semente;
    public Plantas plantaAtual;
    public bool coletavel = false;

    public IEnumerator Crescer()
    {
        yield return new WaitForSeconds(0.01f);
        coletavel = false;

        plantaAtual = semente.GetComponent<Plantas>();

        semente.GetComponent<SpriteRenderer>().sprite = plantaAtual.fases[0];
        semente.transform.localPosition = new Vector3(0, GetComponent<SpriteRenderer>().bounds.size.y + semente.GetComponent<SpriteRenderer>().bounds.size.y, 0);

        for (int i = 0; i < plantaAtual.tempo.Length; i++)
        {

            yield return new WaitForSeconds(plantaAtual.tempo[i]);
            Debug.Log(plantaAtual.nome + " Cresceu!");

            if (i == plantaAtual.tempo.Length - 1)
            {
                coletavel = true;
                Debug.Log(plantaAtual.nome + " Coletavel!");
            }

            semente.GetComponent<SpriteRenderer>().sprite = plantaAtual.fases[i + 1];
            semente.transform.localPosition = new Vector3(0, GetComponent<SpriteRenderer>().bounds.size.y + semente.GetComponent<SpriteRenderer>().bounds.size.y, 0);
        }
    }
}
