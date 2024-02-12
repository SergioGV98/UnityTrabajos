using UnityEngine;
using System.Collections;
using System;

public class ShotController : MonoBehaviour
{
    private Camera cam;
    public enum ModoDeteccionEnemigos
    {
        usandoComponenteEnemy,
        usandoCapaEnemy
    }

    public ModoDeteccionEnemigos deteccionDeEnemigos = ModoDeteccionEnemigos.usandoComponenteEnemy;
    public GameObject shotMarkPrefab;
    public float fadeTime = 5;

    void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlaceShotMarkCoroutine());
        }
    }

    IEnumerator PlaceShotMarkCoroutine()
    {
        float posX = cam.pixelWidth / 2;
        float posY = cam.pixelHeight / 2;
        Ray ray = cam.ScreenPointToRay(new Vector3(posX, posY, 0));
        RaycastHit hit;

        switch (deteccionDeEnemigos)
        {
            case ModoDeteccionEnemigos.usandoComponenteEnemy:
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject impactado = hit.transform.gameObject;
                    EnemigoIA m = impactado.GetComponentInParent<EnemigoIA>();

                    Vector3 offset = hit.normal * 0.001f;
                    Vector3 spawnPosition = hit.point + offset;

                    GameObject shotMark = Instantiate(shotMarkPrefab, spawnPosition, Quaternion.identity);
                    shotMark.transform.LookAt(hit.point - hit.normal);

                    Material material = shotMark.GetComponent<Renderer>().material;
                    Color alphaColor = material.color;

                    for (float fadeTimer = 0f; fadeTimer < fadeTime; fadeTimer += Time.deltaTime)
                    {
                        alphaColor.a = Mathf.Lerp(1f, 0f, fadeTimer / fadeTime);
                        material.color = alphaColor;
                        yield return null;
                    }

                    Destroy(shotMark);

                    if (m != null)
                    {
                        m.Muere();
                    }
                }
                break;
            case ModoDeteccionEnemigos.usandoCapaEnemy:
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
                {
                    GameObject impactado = hit.transform.gameObject;
                    EnemigoIA m = impactado.GetComponentInParent<EnemigoIA>();

                    Vector3 offset = hit.normal * 0.001f;
                    Vector3 spawnPosition = hit.point + offset;

                    GameObject shotMark = Instantiate(shotMarkPrefab, spawnPosition, Quaternion.identity);
                    shotMark.transform.LookAt(hit.point - hit.normal);

                    Material material = shotMark.GetComponent<Renderer>().material;
                    Color alphaColor = material.color;

                    for (float fadeTimer = 0f; fadeTimer < fadeTime; fadeTimer += Time.deltaTime)
                    {
                        alphaColor.a = Mathf.Lerp(1f, 0f, fadeTimer / fadeTime);
                        material.color = alphaColor;
                        yield return null;
                    }

                    Destroy(shotMark);

                    if (m != null)
                    {
                        m.Muere();
                    }
                }
                break;
        }
    }
}