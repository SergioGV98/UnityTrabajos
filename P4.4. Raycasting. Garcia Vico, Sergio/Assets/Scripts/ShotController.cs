using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Camera cam;
    public GameObject shotMarkPrefab;
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
        }
    }

    void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void ShootRay()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("El rayo ha golpeado en el punto: " + hit.point);

            Debug.Log("Objeto golpeado: " + hit.collider.gameObject.name);

            GameObject shotMark;
            shotMark = Instantiate(shotMarkPrefab) as GameObject;
            shotMark.transform.position = new Vector3(

            hit.point.x,
            hit.point.y,
            hit.point.z
            ) + hit.normal * 0.01f;
            shotMark.transform.LookAt(hit.point - hit.normal);
            shotMark.transform.SetParent(hit.transform, true);
        }
        else
        {
            Debug.Log("El rayo no ha golpeado en ningun lado.");
        }
    }
}
