using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    float xAngle = 0;
    public float dir = 1;
    public float limit = 30f;
    public float velocity = 600f;

    bool bAnimationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bAnimationStarted) DoAnimation();
    }

    void DoAnimation()
    {
        if (xAngle > 30) dir = -1;
        if (xAngle < -30) dir = 1;

        xAngle += velocity * Time.deltaTime * dir;

        transform.localEulerAngles = new Vector3(xAngle, 0, 0);
    }

    public void StartAnimation()
    {
        bAnimationStarted = true;
    }

    public void StopAnimation()
    {
        bAnimationStarted = false;
        xAngle = 0;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
