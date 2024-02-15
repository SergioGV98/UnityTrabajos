using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7f;
    Rigidbody2D rb;
    LevelController levelController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelController = FindAnyObjectByType<LevelController>();
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
        if(collision.gameObject.tag == "Brick")
        {
            levelController.OnBrickCollided(collision.gameObject.GetComponent<Brick>());
        }
    }
}