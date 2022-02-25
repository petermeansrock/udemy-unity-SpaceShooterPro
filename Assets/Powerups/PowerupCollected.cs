using UnityEngine;

public class PowerupCollected : MonoBehaviour
{
    [SerializeField]
    private PowerupType type;
    public PowerupType Type => type;
    [SerializeField]
    private AudioClip collectedAudioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only ever consider player collisions
        if (!other.CompareTag(Tag.Player))
        {
            return;
        }

        // Play pick-up sound and destroy object
        AudioSource.PlayClipAtPoint(collectedAudioClip, transform.position);
        Destroy(gameObject);
    }
}
