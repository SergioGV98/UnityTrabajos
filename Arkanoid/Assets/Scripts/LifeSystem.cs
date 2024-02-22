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
        } else if (levelController.lives == 2)
        {
            lives[2].SetActive(false);
        }
        else if (levelController.lives == 1)
        {
            lives[1].SetActive(false);
            lives[2].SetActive(false);
        } else
        {
            lives[0].SetActive(false);
            lives[1].SetActive(false);
            lives[2].SetActive(false);
        }
    }
}
