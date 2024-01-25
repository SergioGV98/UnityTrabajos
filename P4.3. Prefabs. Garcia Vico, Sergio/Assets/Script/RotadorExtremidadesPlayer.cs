using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorExtremidadesPlayer : MonoBehaviour
{
    public float angMinimo = -30f;
    public float angMaximo = 30f;
    public float vAngular = 150f;
    public int direccion = 1;

    private float anguloActual = 0f;
    private Vector3 originalPos;
    private Quaternion originalRot;

    void Start()
    {
        if (direccion == 0)
        {
            direccion = -1;
        }

        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            StartAnimation();
        }
        else
        {
            StopAnimation();
        }
    }

    public void StartAnimation()
    {
        anguloActual += vAngular * direccion * Time.deltaTime;

        if (anguloActual > angMaximo || anguloActual < angMinimo)
        {
            direccion = -direccion;
            anguloActual = Mathf.Clamp(anguloActual, angMinimo, angMaximo);
        }

        transform.localEulerAngles = new Vector3(anguloActual, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void StopAnimation()
    {
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
