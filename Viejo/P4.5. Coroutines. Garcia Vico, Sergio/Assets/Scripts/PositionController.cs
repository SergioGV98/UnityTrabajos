using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PositionController : MonoBehaviour
{
    private CharacterController characterControler;
    RotadorExtremidades[] rotadores;
    public float speed = 10;
    float vy = -10;

    public float jumpInitialSpeed = 3.5f;
    public float fallSpeedLimit = 20;
    public float gravity = -10;
    private bool isGrounded = false;
    private bool bAnimationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        characterControler = GetComponent<CharacterController>();
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
    }


    public void StartAnimation()
    {
        foreach (var rotador in rotadores)
        {
            rotador.StartAnimation();
        }
    }

    public void StopAnimation()
    {
        foreach (var rotador in rotadores)
        {
            rotador.StopAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        actualizaDesplazamientoCC();
        actualizarAscensoCC();
    }

    void actualizarAscensoCC()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            vy = jumpInitialSpeed;
        }
        else if (vy > -fallSpeedLimit)
        {
            vy = vy + gravity * Time.deltaTime;
            if (vy < -fallSpeedLimit)
            {
                vy = -fallSpeedLimit;
            }
        }

        // El movimiento en el eje Y lo aplicamos con respecto al sistema de coordenadas global,
        // ya que el ascenso o descenso de la cámara debe ser independiente de la orientación de la misma.
        characterControler.Move(new Vector3(
            0,
            vy,
            0
        ) * speed / 2 * Time.deltaTime);

        // isGrounded indica si el characterController tocó suelo en la última llamada a Move.
        // Para que se active es necesario que el movimiento se haya realizado hacia el suelo, por ello
        // leemos su valor en esta llamada a Move, que se encarga del movimiento vertical, y no en 
        // la correspondiente al desplazamiento.
        isGrounded = characterControler.isGrounded;
    }

    // Desplazamiento con CharacterController
    void actualizaDesplazamientoCC()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 d = new Vector3(xMovement, 0, zMovement);
        if (!Mathf.Approximately(d.magnitude, 0))
        {
            if (!bAnimationStarted)
            {
                StartAnimation();
                bAnimationStarted = true;
            }
        }
        else
        {
            if (bAnimationStarted)
            {
                StopAnimation();
                bAnimationStarted = false;
            }
        }

        // El desplazamiento de la cámara en los ejes X, Z debe hacerse con respecto a la orientación
        // actual de la cámara (sistema de coordenadas local). Por ello, debemos alinear el vector
        // de movimiento (x, 0, z) con el sistema de coordenadas local.
        // Para ello usaremos el método transform.TransformDirection
        characterControler.Move(transform.TransformDirection(d * speed * Time.deltaTime));

    }

}
