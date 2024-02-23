using UnityEngine;

public class AcelerarJugador : MonoBehaviour
{
    public float factorAceleracion = 3f;
    private bool triggerActivo = false;

    private void Start()
    {
        Invoke("ActivarTrigger", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerActivo && collision.gameObject.CompareTag("Ball"))
        {
            AplicarEfecto(FindAnyObjectByType<Player>());
            Destroy(gameObject);
        }
    }

    private void ActivarTrigger()
    {
        triggerActivo = true;
    }

    public void AplicarEfecto(Player player)
    {
        player.speed += factorAceleracion;
    }
}
