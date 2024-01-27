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

        RaycastHit hit;

        // Primer punto de impacto
        Physics.SphereCast(rayo, 0.25f, out hit);
        Debug.DrawLine(rayo.origin, hit.point, Color.red);

        // DrawLine desde el punto de impacto hacia adelante
        Vector3 hitNormal = hit.point + hit.normal * 5;
        Debug.DrawLine(hit.point, hitNormal, Color.blue);

        // DrawLine reflect del primer punto de impacto
        Vector3 direccionReflejada = rayo.direction;
        Vector3 hitReflejado = Vector3.Reflect(direccionReflejada, hit.normal);
        Debug.DrawLine(hit.point, hit.point + hitReflejado * 5, Color.white);

        float distanciaAlPuntoDeImpacto = Vector3.Distance(transform.position, hit.point);

        if (distanciaAlPuntoDeImpacto > 2.5f)
        {
            estado = EstadoEnemigo.Andando;
            if (estado == EstadoEnemigo.Andando)
            {
                IniciarAnimacion();
                transform.position = Vector3.Lerp(transform.position, hit.point, Time.deltaTime * 0.5f);
            }
        } else
        {
            estado = EstadoEnemigo.Parado;
            if (estado == EstadoEnemigo.Parado)
            {
                PararAnimacion();
                hit.point = hitReflejado;
            }
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
