using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public float speed = 7f;
    Rigidbody2D rb;
    LevelController levelController;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelController = FindFirstObjectByType<LevelController>();
    }

    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void Launch(Vector2 dir)
    {
        rb.velocity = dir.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            levelController.OnBrickCollided(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Card"))
        {
            AplicarEfectoDeCarta(collision.gameObject);
        }
    }

    private void AplicarEfectoDeCarta(GameObject card)
    {
        AcelerarPelota acelerarPelota = card.GetComponent<AcelerarPelota>();
        if (acelerarPelota != null)
        {
            speed += 2f; 
            Destroy(card);
        }
    }



}
