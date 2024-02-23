using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieDetector : MonoBehaviour
{
    LevelController levelController;
    void Start()
    {
        levelController = FindAnyObjectByType<LevelController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            levelController.OnDie();
        }
    }
}
