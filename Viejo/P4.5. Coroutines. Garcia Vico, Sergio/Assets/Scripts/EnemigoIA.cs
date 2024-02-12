using UnityEngine;
using System.Collections;

public class EnemigoIA : MonoBehaviour
{
    public float speed = 4f;
    bool bMuriendo = false;
    bool bMuerto = false;

    private RotadorExtremidades[] rotadores;
    RotadorCabeza rotadorCabeza;

    enum EstadoEnemigo
    {
        Parado = 0,
        Andando = 1
    }

    private EstadoEnemigo estadoEnemigo = EstadoEnemigo.Andando;


    void Start()
    {
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
        rotadorCabeza = GetComponentInChildren<RotadorCabeza>();

    }

    void Update()
    {
        if (estadoEnemigo == EstadoEnemigo.Andando)
        {
            StartAnimation();
            StartMovement();
        }
        else
        {
            StopAnimation();
        }




    }
    public void StartMovement()
    {
        Ray rayo = new Ray(transform.position + new Vector3(0, 1, 0), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(rayo, out hit))
        {

            Debug.DrawLine(rayo.origin, hit.point, Color.red);

            Vector3 direccionReflejada = Vector3.Reflect(rayo.direction, hit.normal);
            Debug.DrawRay(hit.point, direccionReflejada * 5, Color.blue);

            Debug.DrawRay(hit.point, hit.normal * 5, Color.white);

            if (Physics.Raycast(rayo, out hit, 1f))
            {


                RaycastHit sphereHit;
                if (Physics.SphereCast(rayo, 0.5f, out sphereHit))
                {
                    Vector3 forwardWithoutY = new Vector3(direccionReflejada.x, 0, direccionReflejada.z).normalized;
                    transform.forward = forwardWithoutY;
                }
            }
            else
            {

                transform.position += rayo.direction * speed * Time.deltaTime;
            }
        }
    }

    public void StartAnimation()
    {
        rotadorCabeza.StartAnimation();
        foreach (var rotador in rotadores)
        {
            rotador.StartAnimation();
        }
    }

    public void StopAnimation()
    {
        rotadorCabeza.StopAnimation();
        foreach (var rotador in rotadores)
        {
            rotador.StopAnimation();
        }
    }

    public void SetTarget(GameObject target)
    {
        RotadorCabeza rotador = GetComponentInChildren<RotadorCabeza>();
        rotador.SetTarget(target);
    }

    public void Muere()
    {
        StopAnimation();
        StartCoroutine(AnimacionCaida());
        estadoEnemigo = EstadoEnemigo.Parado;
    }

    private IEnumerator AnimacionCaida()
    {
        for (float a = 0; a <= 90; a += 75 * Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(a, 0, 0);
            yield return null;
        }
        bMuriendo = false;
        bMuerto = true;
    }
}
