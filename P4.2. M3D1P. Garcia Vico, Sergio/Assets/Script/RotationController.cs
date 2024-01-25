using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EjesRotacion
{
    MouseX,
    MouseY,
    MouseXY
}

public class RotationController : MonoBehaviour
{
    [Range(0, 800)]
    public float velocidadRotacion = 320f;

    public EjesRotacion modoRotacion = EjesRotacion.MouseXY;
    public float rotacionXMin = -90f; 
    public float rotacionXMax = 90f;  

    void Start()
    {
        Debug.Log("Coordenadas" + transform.position);
        Debug.Log("Orientacion" + transform.rotation);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float movimientoRatonY = Input.GetAxis("Mouse Y");
        float movimientoRatonX = Input.GetAxis("Mouse X");

        float incRotacionX = 0f;
        float incRotacionY = 0f;

        if (modoRotacion == EjesRotacion.MouseX || modoRotacion == EjesRotacion.MouseXY)
        {
            incRotacionY = movimientoRatonX * velocidadRotacion * Time.deltaTime;
        }

        if (modoRotacion == EjesRotacion.MouseY || modoRotacion == EjesRotacion.MouseXY)
        {
            incRotacionX = -movimientoRatonY * velocidadRotacion * Time.deltaTime;
            incRotacionX = Mathf.Clamp(incRotacionX, rotacionXMin, rotacionXMax); 
        }

        transform.localEulerAngles += new Vector3(incRotacionX, incRotacionY, 0);
    }
}
