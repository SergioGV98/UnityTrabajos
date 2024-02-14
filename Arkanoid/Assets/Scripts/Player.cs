using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    public float speed = 1.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector2.left * speed;
        } else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector2.right * speed;
        }
    }

    private Vector3 MousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousePosition.y = transform.position.y;
        return mousePosition;
    }

    private void OnMouseDrag()
    {
        if(capsuleCollider.gameObject.name != "RightWall" && capsuleCollider.gameObject.name != "LeftWall")
        {
            rb.MovePosition(MousePosition());
        }
    }
}
