using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    FixedJoint2D fixedJoint;
    public Ball ball;
    public float speed = 1f;
    [SerializeField] public float width = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Cursor.lockState = CursorLockMode.Confined;
        fixedJoint = GetComponent<FixedJoint2D>();
        StartCoroutine(LaunchBall());
    }

    void Update()
    {
        gameObject.transform.localScale = new Vector3(width, 1, 1);

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

    public IEnumerator LaunchBall()
    {
        yield return new WaitForSeconds(2f);
        fixedJoint.enabled = false; 
        Vector2 launchDirection = rb.velocity.normalized + Vector2.up; 
        ball.Launch(launchDirection );
    }

}
