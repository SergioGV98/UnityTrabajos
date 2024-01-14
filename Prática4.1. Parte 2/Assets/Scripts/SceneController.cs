using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    int i;

    void Start()
    {
        Debug.Log("Hola mundo");
    }

    void Update()
    {
        Debug.Log(i++);
    }
}
