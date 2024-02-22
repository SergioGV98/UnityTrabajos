using UnityEngine;


public class AcelerarPelota : MonoBehaviour
{
    public float factorAceleracion = 0.05f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AplicarEfecto(collision.gameObject.GetComponent<Ball>());
            Destroy(this); 
        }
    }

    public void AplicarEfecto(Ball ball)
    {
        ball.speed += factorAceleracion;
    }
}
