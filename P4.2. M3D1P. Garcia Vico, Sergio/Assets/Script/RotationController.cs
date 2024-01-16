using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Coordenadas" + transform.position);
        Debug.Log("Orientacion" + transform.rotation);
    }

    void Update()
    {
        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));
    }
}
