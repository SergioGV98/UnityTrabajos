using UnityEngine;
using System.Collections;

public class EnemigoIA : MonoBehaviour
{
    enum EstadoEnemigo
    {
        Parado = 0,
        Andando = 1
    }

    public RotadorExtremidades[] rotadores;
    public RotadorCabeza rotadorCabeza;
    private EstadoEnemigo estado = EstadoEnemigo.Parado;
    public float speed;
    bool bMuriendo = false;
    bool bMuerto = false;
    bool bInicioMovimiento = false;

    void Start()
    {
        StartCoroutine(StartMovingAfterDelay());
    }

    IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(FindAnyObjectByType<EnemySpawner>().initialDelay);

        bInicioMovimiento = true;
    }

    void Update()
    {
        if (!bInicioMovimiento)
            return;

        Ray rayo = new Ray(transform.position + new Vector3(0, 1, 0) + transform.forward, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, Mathf.Infinity, LayerMask.GetMask("Walls")))
        {
            Debug.DrawLine(rayo.origin, hit.point, Color.red);

            Vector3 hitNormal = hit.point + hit.normal * 5;
            Debug.DrawLine(hit.point, hitNormal, Color.blue);

            Vector3 direccionReflejada = rayo.direction;
            Vector3 hitReflejado = Vector3.Reflect(direccionReflejada, hit.normal);
            Debug.DrawLine(hit.point, hit.point + hitReflejado * 5, Color.white);

            float distanciaAlPuntoDeImpacto = Vector3.Distance(transform.position, hit.point);

            if (distanciaAlPuntoDeImpacto > 2.5f)
            {
                estado = EstadoEnemigo.Andando;
                if (estado == EstadoEnemigo.Andando)
                {
                    StartAnimation();
                    Vector3 directionToTarget = (hit.point - transform.position).normalized;
                    transform.position += directionToTarget * speed * Time.deltaTime;
                }
            }
            else
            {
                estado = EstadoEnemigo.Parado;
                if (estado == EstadoEnemigo.Parado)
                {
                    StopAnimation();
                    transform.rotation = Quaternion.LookRotation(hitReflejado.normalized);
                }
            }
        }
    }



    public void StartAnimation()
    {
        if(bInicioMovimiento)
        {
            rotadorCabeza.StartAnimation();
            foreach (var rotador in rotadores)
            {
                rotador.StartAnimation();
            }
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
        if (!bMuerto && !bMuriendo) 
        {
            bMuriendo = true;
            StopAnimation();
            StartCoroutine(AnimacionCaida());
        }
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
        Destroy(gameObject);
    }
}
