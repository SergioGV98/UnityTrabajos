using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingItem : MonoBehaviour
{
    public float velocidadRotacion = 50f; 
    public float amplitudBouncing = 1f; 
    public float smoothness = 1f; 

    private void Start()
    {
        StartCoroutine(Rotar());
        StartCoroutine(Bouncing());
    }

    private IEnumerator Rotar()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Bouncing()
    {
        Vector3 startPosition = transform.position;
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime * smoothness;
            float y = startPosition.y + Mathf.Sin(t) * amplitudBouncing;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
    }
}
