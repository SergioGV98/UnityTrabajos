using UnityEngine;

public class AcelerarPelota : MonoBehaviour
{
    public float factorAceleracion = 5f;
    private bool triggerActivo = false
        ;
    private void Start()
    {
        Invoke("ActivarTrigger", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerActivo && collision.gameObject.CompareTag("Ball"))
        {
            AplicarEfecto(collision.gameObject.GetComponent<Ball>());
            Destroy(gameObject);
        }
    }

    private void ActivarTrigger()
    {
        triggerActivo = true;
    }

    public void AplicarEfecto(Ball ball)
    {
        ball.speed += factorAceleracion;
    }
}
