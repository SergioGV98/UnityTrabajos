using UnityEngine;

public class DecelerarPelota : MonoBehaviour
{
    public float factorDesceleracion = 2f;
    private bool triggerActivo = false;

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
        ball.speed -= factorDesceleracion;
    }
}
