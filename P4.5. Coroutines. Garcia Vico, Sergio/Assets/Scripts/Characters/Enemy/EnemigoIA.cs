using System.Collections;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public enum EstadoEnemigo
    {
        Parado = 0,
        Andando = 1
    }

    public EstadoEnemigo estadoEnemigo = EstadoEnemigo.Parado;
    public float speed = 4f;
    bool bMuriendo = false;
    bool bMuerto = false;

    public RotadorExtremidades[] rotadores;
    public RotadorCabeza rotadorCabeza;


    void Start()
    {
        
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
            Debug.DrawRay(hit.point, direccionReflejada * 5, Color.white);
            Debug.DrawRay(hit.point, hit.normal * 5, Color.blue);

            if (hit.distance < 1f) 
            {
                Vector3 forwardWithoutY = new Vector3(direccionReflejada.x, 0, direccionReflejada.z).normalized;
                transform.forward = forwardWithoutY;
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
        bMuriendo = true;
        StartCoroutine(AnimacionCaida());
    }

    private IEnumerator AnimacionCaida()
    {
        for (float a = 0; a <= 90; a += 75 * Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(a, 0, 0);
            yield return null;
        }
        bMuerto = true;
        Destroy(gameObject); 
        EnemySpawner enemySpawner = FindFirstObjectByType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.DecreaseEnemyCount(); 
        }
    }
}
