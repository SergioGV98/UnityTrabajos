using UnityEngine;
public class RotadorCabeza : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float lookAtPlayerTime = 0.2f;
    private float returnToInitialTime = 1.0f;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 direccionPersonaje = transform.parent.forward;
        Vector3 direccionPlayer = player.transform.position - transform.position;
        float gradoAlinPersPlayer = Vector3.Dot(direccionPersonaje.normalized, direccionPlayer.normalized);

        if (gradoAlinPersPlayer > 0.3f)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtPlayerTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, returnToInitialTime * Time.deltaTime);
        }
    }
}
