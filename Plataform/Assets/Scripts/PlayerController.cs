using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed = 7;
    public float jumpImpulse = 5;
    public float acceleration = 30;
    public float decceleration = 30;
    bool isJumping = false;
    int dir = 1;
    float velocity = 0;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        velocity = 0;
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if(dx == 0)
        {
            float delta = decceleration * Time.fixedDeltaTime;
            // Deceleramos el personaje en la direccion contraria a su movimiento
            if(rb.velocityX > 0)
            {
                // Me muevo a la derecha
                velocity = rb.velocityX - delta;
                if(velocity < 0) velocity = 0;
            } else if (rb.velocityX < 0)
            {
                // Me muevo a la izquierda
                velocity = rb.velocityX + delta;
                if (velocity > 0) velocity = 0;
            }
            
        } else
        {
            // Aceleramos en la direccion indicada por dx
            velocity = rb.velocityX + dx * acceleration * Time.fixedDeltaTime;
            velocity = Mathf.Clamp(velocity, -maxSpeed, maxSpeed);
        }

        if (velocity > 0) dir = 1;
        if (velocity < 0) dir = -1;

        transform.localScale = new Vector3(dir, 1, 1);

        // Asignandoselo al RB
        animator.SetFloat("speed", Mathf.Abs(velocity));
        rb.velocityX = velocity;

        if(dy > 0 && !isJumping)
        {
            rb.AddForceY(jumpImpulse, ForceMode2D.Impulse);
            isJumping = true; 
            animator.SetBool("isJumping", isJumping);
        } 

        if(rb.velocityY < 0)
        {
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
        }
    }
}
