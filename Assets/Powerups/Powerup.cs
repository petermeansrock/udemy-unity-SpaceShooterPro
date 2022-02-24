using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private PowerupType type;
    public PowerupType Type => type;
    [SerializeField]
    private AudioClip collectedAudioClip;

    private float minY = -5.7f;

    public const string TAG = "Powerup";
    private const float POWERUP_PERIOD = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Move down at a specific speed and despawn at bottom of screen
        transform.Translate(speed * Time.deltaTime * Vector3.down);
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only ever consider player collisions
        if (!other.CompareTag(Player.TAG))
        {
            return;
        }

        // Play pick-up sound and destroy object
        AudioSource.PlayClipAtPoint(collectedAudioClip, transform.position);
        Destroy(gameObject);
    }

    public static IEnumerator PowerDownRoutine(Action action)
    {
        yield return new WaitForSeconds(POWERUP_PERIOD);
        action.Invoke();
    }
}
