using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerUpPrefabs;
    [SerializeField]
    private UnityEvent<int> enemyReportedScoreEvent;

    private bool stopSpawning = false;

    // TODO: Refactor so that Enemy & SpawnManager share these bounds
    private float maxY = 8.0f;
    private float maxX = 8.0f;
    private float minX = -8.0f;

    private float maxEnemyDelay = 3.0f;
    private float minEnemyDelay = 0.5f;
    private float maxPowerupDelay = 7.0f;
    private float minPowerupDelay = 3.0f;

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!stopSpawning)
        {
            // Spawn at a random x position
            float x = Random.Range(minX, maxX);

            // Wire up events between enemy and configured event receiver
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, maxY, 0), Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().destroyedByLaserEvent.AddListener(
                score => enemyReportedScoreEvent.Invoke(score)
            );

            // Group enemies under the spawn manager
            enemy.transform.parent = gameObject.transform;

            // Delay spawns by a random amount
            float delay = Random.Range(minEnemyDelay, maxEnemyDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!stopSpawning)
        {
            // Spawn a random powerup
            int powerupIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerupPrefab = powerUpPrefabs[powerupIndex];

            // Spawn at a random x position
            float x = Random.Range(minX, maxX);
            GameObject powerup = Instantiate(powerupPrefab, new Vector3(x, maxY, 0), Quaternion.identity);

            // Group powerups under the spawn manager
            powerup.transform.parent = gameObject.transform;

            // Delay spawns by a random amount
            float delay = Random.Range(minPowerupDelay, maxPowerupDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    public void StartSpawning()
    {
        stopSpawning = false;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }
}
