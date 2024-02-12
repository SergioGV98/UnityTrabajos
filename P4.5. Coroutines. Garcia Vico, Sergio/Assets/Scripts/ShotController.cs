using UnityEngine;
using System.Collections;

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
    public float markDisappearTime = 5f;
    public float fadeTime = 2f;
    public float smoothTime = 2f;

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
            StartCoroutine(PlaceShotMark());
        }
    }

    IEnumerator PlaceShotMark()
    {
        float posX = cam.pixelWidth / 2;
        float posY = cam.pixelHeight / 2;
        Ray ray = cam.ScreenPointToRay(new Vector3(posX, posY, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("El rayo ha golpeado en el punto: " + hit.point);
            Debug.Log("Objeto golpeado: " + hit.collider.gameObject.name);

            Vector3 offset = hit.normal * 0.001f; 
            Vector3 spawnPosition = hit.point + offset;

            GameObject shotMark = Instantiate(shotMarkPrefab, spawnPosition, Quaternion.identity);
            shotMark.transform.LookAt(hit.point - hit.normal);

            Material m = shotMark.GetComponent<Renderer>().material;
            Color alphaColor = new Color(m.color.r, m.color.g, m.color.b, m.color.a);

            // Desvanecimiento con Mathf.Lerp()
            
            for (float s = 0; s < fadeTime; s += Time.deltaTime)
            {
                alphaColor.a = Mathf.Lerp(1, 0, s / fadeTime);
                m.color = alphaColor;
                yield return null;
            }
            

            // Desvanecimiento con Mathf.SmoothDamp()
            /*
            float currentVelocity = 0f;
            float targetAlpha = 0f;

            while (m.color.a > 0.01f)
            {
                alphaColor.a = Mathf.SmoothDamp(alphaColor.a, targetAlpha, ref currentVelocity, smoothTime);
                m.color = alphaColor;
                yield return null;
            }*/

            Destroy(shotMark);
        }
        else
        {
            Debug.Log("El rayo no ha golpeado en ningun lado.");
        }
    }
}
