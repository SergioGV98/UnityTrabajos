using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotacionX : MonoBehaviour

{
    private float i = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if(transform.rotation.x < 40)
        {
            transform.rotation = Quaternion.Euler(i++, 0, 0);
        } else if (transform.rotation.x > -40)
        {
            transform.rotation = Quaternion.Euler(i--, 0, 0);
        }
    }
}
