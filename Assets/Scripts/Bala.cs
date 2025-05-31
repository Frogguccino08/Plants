using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    float forcaBala = 3f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + forcaBala * Time.deltaTime, transform.position.z);

        if (transform.position.y >= 1.2f)
        {
            Destroy(this.gameObject);
        }
    }
}
