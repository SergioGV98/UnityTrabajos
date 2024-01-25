using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    enum EstadoEnemigo
    {
        Parado = 0,
        Andando = 1
    }

    private EstadoEnemigo estado = EstadoEnemigo.Parado;
    private RotadorExtremidades[] rotadores;

    void Start()
    {
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
    }

    void Update()
    {
        Ray rayo = new Ray(transform.position + new Vector3(0, 1, 0) + transform.forward, transform.forward);
        Debug.DrawRay(rayo.origin, rayo.direction, Color.red);

        RaycastHit hit;
        if (Physics.SphereCast(rayo, 0.1f, out hit))
        {
            Vector3 hitPoint = hit.point;
            Debug.DrawLine(Vector3.Reflect(transform.forward, hitPoint), Vector3.forward);
        }

        if (estado == EstadoEnemigo.Andando)
        {
            IniciarAnimacion();
        }
        else
        {
            PararAnimacion();
        }
    }

   private void IniciarAnimacion()
    {
        foreach (var item in rotadores)
        {
            item.StartAnimation();
        }
    }

    private void PararAnimacion()
    {
        foreach (var item in rotadores)
        {
            item.StopAnimation();
        }
    }


}
