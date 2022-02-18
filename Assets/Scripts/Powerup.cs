using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private PowerupType type;
    [SerializeField]
    private AudioClip collectedAudioClip;

    private float minY = -5.7f;

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
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Apply power-up and destroy object
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log(type + " powerup collected!");
            switch (type)
            {
                case PowerupType.TripleShot:
                    player.EnableTripleShot();
                    break;
                case PowerupType.Speed:
                    player.EnableSpeedBoost();
                    break;
                case PowerupType.Shield:
                    player.EnableShield();
                    break;
            }
        }

        AudioSource.PlayClipAtPoint(collectedAudioClip, transform.position);
        Destroy(gameObject);
    }

    enum PowerupType
    {
        TripleShot,
        Speed,
        Shield,
    }
}
