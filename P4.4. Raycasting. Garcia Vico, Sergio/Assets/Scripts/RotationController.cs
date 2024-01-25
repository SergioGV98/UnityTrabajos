using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationVelocity = 700;
    public float speed = 10;

    private float xAngle = 0;
    private float yAngle = 0;
    bool bEnableControls = false;
    
    public enum RotationMode{
        MouseX,
        MouseY,
        MouseXY
    }
    
    public RotationMode rotationMode = RotationMode.MouseXY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        xAngle = transform.localEulerAngles.x;
        yAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) bEnableControls = !bEnableControls;

        if (!bEnableControls) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation;

        xAngle -= mouseY * rotationVelocity * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, -90,90);
        yAngle += mouseX * rotationVelocity * Time.deltaTime;

        switch(rotationMode){
            case RotationMode.MouseX:
                rotation = new Vector3(0, yAngle, 0);
                break;
            case RotationMode.MouseY:
                rotation = new Vector3(xAngle, 0, 0);
                break;
            default:
                rotation = new Vector3(xAngle, yAngle, 0);
                break;
        }
        
        transform.localEulerAngles = rotation;
    }

}
