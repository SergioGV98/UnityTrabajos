using UnityEngine;


public class PositionController : MonoBehaviour
{
    private CharacterController characterControler;
    public float speed = 10;
    float speedVertical = -10;

    public float jump = 3.5f;
    public float fallSpeedLimit = 20;
    public float gravity = -10;
    private bool isGrounded = false;

    void Start()
    {
        characterControler = GetComponent<CharacterController>();
    }

    void Update()
    {
        movementController();
        jumpController();
    }

    void jumpController()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            speedVertical = jump;
        }
        else if (speedVertical > -fallSpeedLimit)
        {
            speedVertical = speedVertical + gravity * Time.deltaTime;
            if (speedVertical < -fallSpeedLimit)
            {
                speedVertical = -fallSpeedLimit;
            }
        }
        characterControler.Move(new Vector3(0, speedVertical, 0) * speed / 2 * Time.deltaTime);
        isGrounded = characterControler.isGrounded;
    }

    void movementController()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(xAxis, 0, yAxis);
        characterControler.Move(transform.TransformDirection(direction * speed * Time.deltaTime));

    }

}
