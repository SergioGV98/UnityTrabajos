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

    private EstadoEnemigo estado = EstadoEnemigo.Andando;
    private RotadorExtremidades[] rotadores;
    private Vector3 direccionMovimiento;
    private Vector3 posicionInicial; 
    public float distanciaUmbral = 10f; 

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

        if (Time.frameCount == 1) 
        {
            posicionInicial = transform.position;
        }

        float distanciaAlImpacto = Vector3.Distance(posicionInicial, hit.point);

        if (distanciaAlImpacto > distanciaUmbral)
        {
            direccionMovimiento = hit.normal.normalized;
            transform.LookAt(transform.position + direccionMovimiento);
        }
        else
        {
            direccionMovimiento = rayo.direction.normalized;
        }

        transform.LookAt(transform.position + direccionMovimiento * 2);

        /*if (estado == EstadoEnemigo.Andando)
        {
            IniciarAnimacion();
        }
        else
        {
            PararAnimacion();
        }*/

        MoveInReflectedDirection();
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

    private void MoveInReflectedDirection()
    {
        transform.position += direccionMovimiento * Time.deltaTime;
    }
}
