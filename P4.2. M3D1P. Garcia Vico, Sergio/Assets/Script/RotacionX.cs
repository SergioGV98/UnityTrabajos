using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotacionX : MonoBehaviour
{
    [SerializeField]
    private float velocidadRotacion = 30f;
    [SerializeField]
    private float limiteRotacionX = 50f;
    private bool subiendo = true;

    void Update()
    {
        Vector3 rotacionActual = transform.rotation.eulerAngles;

        if (subiendo && transform.rotation.eulerAngles.x < limiteRotacionX)
        {
            rotacionActual.x += velocidadRotacion * Time.deltaTime;
        }
        else
        {
            subiendo = false;
            rotacionActual.x -= velocidadRotacion * Time.deltaTime;

            if (rotacionActual.x <= 0.0f)
            {
                subiendo = true;
                rotacionActual.x = 0.0f;
            }
        }
        transform.rotation = Quaternion.Euler(rotacionActual);
    }
}