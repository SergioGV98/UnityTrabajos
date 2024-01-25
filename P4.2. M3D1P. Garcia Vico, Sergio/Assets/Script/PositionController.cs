using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    [Range(0, 20)]
    public float speed = 10;
    public float velocidadSalto = 2f;
    public float gravedad = 9.8f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            movement = new Vector3(horizontalInput, velocidadSalto, verticalInput) * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.LeftShift))
        {
            //movement = new Vector3(horizontalInput, -velocidadSalto, verticalInput) * speed * Time.deltaTime;
            movement.y -= gravedad * Time.deltaTime;
        }
        characterController.Move(movement);

        Debug.Log("Horizontal: " + horizontalInput + ", Vertical: " + verticalInput);
    }
}
