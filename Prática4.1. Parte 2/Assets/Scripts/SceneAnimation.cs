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

        //Planetas
        RotatePlanet(mercurio, 0.4f);
        RotatePlanet(venus, 0.7f);
        RotatePlanet(tierra, 1.0f);
        RotatePlanet(marte, 1.5f);
        RotatePlanet(jupiter, 2.0f);
        RotatePlanet(saturno, 2.5f);
        RotatePlanet(urano, 3.0f);
        RotatePlanet(neptuno, 3.5f);
    }

    void RotatePlanet(GameObject planet, float distanceScale)
    {
        planet.transform.RotateAround(sol.transform.position, Vector3.up, velocidadAngular * distanceScale * Time.deltaTime);
        planet.transform.Rotate(Vector3.up, rotationSpeed * distanceScale * Time.deltaTime);
    }
}
