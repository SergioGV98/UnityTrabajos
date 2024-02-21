using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public LevelController levelController;
    public GameObject[] lives;

    void Update()
    {
        if(levelController.lives == 3)
        {
            foreach(GameObject go in lives)
            {
                go.SetActive(true);
            }
        }
    }
}
