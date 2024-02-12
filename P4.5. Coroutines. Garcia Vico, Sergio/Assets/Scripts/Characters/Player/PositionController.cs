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

    void Start()
    {        
        characterControler = GetComponent<CharacterController>();
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
    }

    
    public void StartAnimation(){
        foreach (var rotador in rotadores)
        {
            rotador.StartAnimation();
        }
    }

    public void StopAnimation(){
        foreach (var rotador in rotadores)
        {
            rotador.StopAnimation();
        }
    }

    void Update()
    {
        actualizaDesplazamientoCC();
        actualizarAscensoCC();
    }

    void actualizarAscensoCC(){
        if (Input.GetKey(KeyCode.Space) && isGrounded){
            vy = jumpInitialSpeed;
        }else if (vy > -fallSpeedLimit){
            vy = vy + gravity*Time.deltaTime;
            if (vy < -fallSpeedLimit){
                vy = -fallSpeedLimit;
            }
        }

        characterControler.Move(new Vector3(
            0,
            vy,
            0
        ) * speed/2 * Time.deltaTime);

        isGrounded = characterControler.isGrounded;
    }

    void actualizaDesplazamientoCC(){
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        
        Vector3 d = new Vector3(xMovement, 0, zMovement);
        if (!Mathf.Approximately(d.magnitude,0)){
            if (!bAnimationStarted) {
                StartAnimation();
                bAnimationStarted = true;
            }
        }else{
            if (bAnimationStarted) {
                StopAnimation();
                bAnimationStarted = false;
            }
        }

        characterControler.Move(transform.TransformDirection(d * speed * Time.deltaTime));
        
    }

}
