using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    public float angMinimo = -30f;
    public float angMaximo = 30f;
    public float vAngular = 150f;
    public int direccion = 1;

    private float anguloActual = 0f;

    void Start()
    {
        if (direccion == 0)
        {
            direccion = -1;
        }
    }

    void Update()
    {
        anguloActual += vAngular * direccion * Time.deltaTime;

        if (anguloActual > angMaximo || anguloActual < angMinimo)
        {
            direccion = -direccion;
            anguloActual = Mathf.Clamp(anguloActual, angMinimo, angMaximo);
        }

        transform.localEulerAngles = new Vector3(anguloActual, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
