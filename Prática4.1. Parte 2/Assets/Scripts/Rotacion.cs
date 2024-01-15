using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject planeta;
    [SerializeField] private float velocidadAngular = 100.0f;
    [SerializeField] private float distanceScale = 0.4f;

    void Start()
    {
        
    }
    void Update()
    {
        planeta.transform.RotateAround(sol.transform.position, Vector3.up, velocidadAngular * distanceScale * Time.deltaTime);
        planeta.transform.Rotate(Vector3.up, 30 * distanceScale * Time.deltaTime);
    }
}
