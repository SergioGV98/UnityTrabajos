using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject enemyPrefab;
    public float initialDelay = 1f;
    [Range(4f, 300f)]
    public float spawnIntervalSlider = 10;
    public int maxEnemies = 5;

    [Range(1f, 300f)]
    public float emergenceTime = 3f;

    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float spawnInterval = spawnIntervalSlider;
            yield return new WaitForSeconds(spawnInterval);

            if (currentEnemies < maxEnemies)
            {
                StartCoroutine(SpawnEnemyV2());
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        currentEnemies++;

        float randomAngle = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, randomRotation);
        EnemigoIA enemyIA = newEnemy.GetComponent<EnemigoIA>();

        if (enemyIA != null)
        {
            enemyIA.estadoEnemigo = EnemigoIA.EstadoEnemigo.Parado; 
            yield return new WaitForSeconds(initialDelay);
            enemyIA.estadoEnemigo = EnemigoIA.EstadoEnemigo.Andando; 
        }
    }


    IEnumerator SpawnEnemyV2()
    {
        currentEnemies++;

        float randomAngle = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);

        Vector3 spawnPosition = spawnPoint.position - Vector3.up * 10f;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, randomRotation);

        Vector3 targetPosition = spawnPoint.position;

        float timer = 0f;
        while (timer < emergenceTime)
        {
            timer += Time.deltaTime;
            float progress = timer / emergenceTime;
            newEnemy.transform.position = Vector3.Lerp(spawnPosition, targetPosition, progress);
            yield return null;
        }

        EnemigoIA enemyIA = newEnemy.GetComponent<EnemigoIA>();
        if (enemyIA != null)
        {
            enemyIA.estadoEnemigo = EnemigoIA.EstadoEnemigo.Parado; 
            yield return new WaitForSeconds(initialDelay);
            enemyIA.estadoEnemigo = EnemigoIA.EstadoEnemigo.Andando; 
        }
    }



    public void DecreaseEnemyCount()
    {
        currentEnemies--;
    }
}
