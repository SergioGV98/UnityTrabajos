using System.Collections;
using UnityEngine;

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
    public float fadeTime = 2f;
    public float waitUntilDestroy = 5f;
    public bool useSmoothDamp = true; 

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

        switch (deteccionDeEnemigos)
        {
            case ModoDeteccionEnemigos.usandoComponenteEnemy:
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject impactado = hit.transform.gameObject;
                    EnemigoIA m = impactado.GetComponentInParent<EnemigoIA>();

                    GameObject shotMark = Instantiate(shotMarkPrefab, hit.point * 0.001f, Quaternion.identity);
                    shotMark.transform.position = hit.point + hit.normal * 0.01f;
                    shotMark.transform.LookAt(hit.point - hit.normal);
                    shotMark.transform.SetParent(hit.transform, true);

                    if (m != null)
                    {
                        m.Muere();
                    }

                    yield return new WaitForSeconds(waitUntilDestroy); 

                    Renderer renderer = shotMark.GetComponent<Renderer>();
                    Material material = renderer.material;
                    Color color = material.color;

                    if (useSmoothDamp)
                    {
                        float currentAlpha = color.a;
                        float targetAlpha = 0f;
                        float velocity = 0f;

                        while (Mathf.Abs(color.a - targetAlpha) > 0.01f)
                        {
                            color.a = Mathf.SmoothDamp(color.a, targetAlpha, ref velocity, fadeTime);
                            material.color = color;
                            yield return null;
                        }
                    }
                    else
                    {
                        for (float t = 0; t < fadeTime; t += Time.deltaTime)
                        {
                            color.a = Mathf.Lerp(1f, 0f, t / fadeTime);
                            material.color = color;
                            yield return null;
                        }
                    }

                    Destroy(shotMark);
                }
                break;

            case ModoDeteccionEnemigos.usandoCapaEnemy:
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
                {
                    GameObject impactado = hit.transform.gameObject;
                    EnemigoIA m = impactado.GetComponentInParent<EnemigoIA>();
                    m.Muere();

                    yield return new WaitForSeconds(waitUntilDestroy); 

                    Renderer renderer = shotMarkPrefab.GetComponent<Renderer>();
                    Material material = renderer.material;
                    Color color = material.color;

                    if (useSmoothDamp)
                    {
                        float currentAlpha = color.a;
                        float targetAlpha = 0f;
                        float velocity = 0f;

                        while (Mathf.Abs(color.a - targetAlpha) > 0.01f)
                        {
                            color.a = Mathf.SmoothDamp(color.a, targetAlpha, ref velocity, fadeTime);
                            material.color = color;
                            yield return null;
                        }
                    }
                    else
                    {
                        for (float t = 0; t < fadeTime; t += Time.deltaTime)
                        {
                            color.a = Mathf.Lerp(1f, 0f, t / fadeTime);
                            material.color = color;
                            yield return null;
                        }
                    }

                    Destroy(shotMarkPrefab);
                }
                break;
        }
    }
}
