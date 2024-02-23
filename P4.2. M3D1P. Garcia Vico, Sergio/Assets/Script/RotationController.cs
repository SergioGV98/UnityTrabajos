using UnityEngine;

public enum RotationMode
{
    MouseX,
    MouseY,
    MouseXY
}

public class RotationController : MonoBehaviour
{
    public float rotationVelocity = 700;
    public float speed = 10;

    private float xAxis = 0;
    private float yAxis = 0;


    public RotationMode rotationMode = RotationMode.MouseXY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xAxis = transform.localEulerAngles.x;
        yAxis = transform.localEulerAngles.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation;

        xAxis -= mouseY * rotationVelocity * Time.deltaTime;
        xAxis = Mathf.Clamp(xAxis, -90, 90);
        yAxis += mouseX * rotationVelocity * Time.deltaTime;

        switch (rotationMode)
        {
            case RotationMode.MouseX:
                rotation = new Vector3(0, yAxis, 0);
                break;
            case RotationMode.MouseY:
                rotation = new Vector3(xAxis, 0, 0);
                break;
            default:
                rotation = new Vector3(xAxis, yAxis, 0);
                break;
        }

        transform.localEulerAngles = rotation;
    }

}