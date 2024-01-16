using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject mercurio;
    [SerializeField] private GameObject venus;
    [SerializeField] private GameObject tierra;
    [SerializeField] private GameObject marte;
    [SerializeField] private GameObject jupiter;
    [SerializeField] private GameObject saturno;
    [SerializeField] private GameObject urano;
    [SerializeField] private GameObject neptuno;
    public float rotationSpeed = 30f;
    public float velocidadAngular = 100.0f;

    void Start()
    {
    }

    void Update()
    {
        //Sol
        sol.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        RotatePlanet(mercurio, 3.5f);
        RotatePlanet(venus, 3.0f);
        RotatePlanet(tierra, 2.5f);
        RotatePlanet(marte, 2.0f);
        RotatePlanet(jupiter, 1.5f);
        RotatePlanet(saturno, 1.0f);
        RotatePlanet(urano, 0.7f);
        RotatePlanet(neptuno, 0.4f);
    }

    void RotatePlanet(GameObject planet, float distanceScale)
    {
        planet.transform.RotateAround(sol.transform.position, Vector3.up, velocidadAngular * distanceScale * Time.deltaTime);
        planet.transform.Rotate(Vector3.up, rotationSpeed * distanceScale * Time.deltaTime);
    }
}
