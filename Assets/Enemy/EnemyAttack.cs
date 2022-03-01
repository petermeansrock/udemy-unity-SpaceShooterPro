using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private AudioClip laserAudioClip;
    [SerializeField]
    private float minFireDelay = 1.0f;
    [SerializeField]
    private float maxFireDelay = 5.0f;

    private EnemyHealth health;
    private AudioSource audioSource;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FireLaserRoutine());
    }

    private IEnumerator FireLaserRoutine()
    {
        while (health.IsAlive)
        {
            yield return new WaitForSeconds(Random.Range(minFireDelay, maxFireDelay));
            var laserPosition = transform.position;
            Instantiate(laserPrefab, laserPosition, Quaternion.identity);
            audioSource.PlayOneShot(laserAudioClip);
        }
    }
}
