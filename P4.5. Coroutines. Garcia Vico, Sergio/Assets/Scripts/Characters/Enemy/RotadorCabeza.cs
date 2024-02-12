using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorCabeza : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    bool bAnimate = false;

    void Update()
    {
        if (bAnimate){
            Vector3 dirTarget = (target.transform.position - transform.position).normalized;
            Quaternion rotInicial = Quaternion.LookRotation(transform.parent.transform.forward);
            Quaternion rotTarget = Quaternion.LookRotation(dirTarget);

            if (Vector3.Dot(dirTarget, transform.parent.transform.forward)>0){
                if (Quaternion.Angle(transform.rotation, rotTarget)>0.5){
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime*10);
                }else{
                    transform.rotation = rotTarget;
                }
            }else{
                if (Quaternion.Angle(transform.rotation, rotInicial)>0.5){
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotInicial, Time.deltaTime*10);
                }else{
                    transform.rotation = rotInicial;
                }
            }
        }
    }

    public void StartAnimation(){
        bAnimate = true;
    }

    public void StopAnimation(){
        bAnimate = false;
        transform.forward = transform.parent.forward;
    }

    public void SetTarget(GameObject target){
        this.target = target;
    }

}
