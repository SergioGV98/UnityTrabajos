using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    private Camera cam;
    public enum ModoDeteccionEnemigos{
        usandoComponenteEnemy,
        usandoCapaEnemy
    }

    public ModoDeteccionEnemigos deteccionDeEnemigos = ModoDeteccionEnemigos.usandoComponenteEnemy;

    void OnGUI(){
        int size = 12;
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            float posX = cam.pixelWidth/2;
            float posY = cam.pixelHeight/2;
            Ray ray = cam.ScreenPointToRay(new Vector3(posX, posY, 0));
            RaycastHit hit;
            
            switch(deteccionDeEnemigos){
                case ModoDeteccionEnemigos.usandoComponenteEnemy:
                    // Detección de enemigo comprobando si el objeto impactado tiene el componente Enemigo
                    if (Physics.Raycast(ray, out hit)){
                        GameObject impactado = hit.transform.gameObject;

                        Enemigo m = impactado.GetComponentInParent<Enemigo>();
                        if (m!=null){
                            Debug.Log("Le he dado a un enemigo");
                            m.Muere();
                        }
                    }
                    break;
                case ModoDeteccionEnemigos.usandoCapaEnemy:
                    // Detección del enemigo haciendo raycasting solo sobre objetos que pertenecen a la
                    // capa "Enemy" (más eficiente, ya que Raycast() solo analizará objetos 
                    // pertenecientes a esta capa)
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"))){
                        GameObject impactado = hit.transform.gameObject;

                        // Asumimos que el objeto impactado tiene el componente Enemigo, ya que
                        // pertenece a la capa "Enemy"
                        Enemigo m = impactado.GetComponentInParent<Enemigo>();
                        m.Muere();
                    }
                    break;
            }
            

            
        }
    }
}
