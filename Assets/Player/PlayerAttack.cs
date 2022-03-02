using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.15f;
    [SerializeField]
    private AudioClip laserAudioClip;

    private AudioSource audioSource;

    private float nextAllowedFire = 0.0f;
    private bool isTripleShotActive = false;

    private int id;
    private KeyCode fireButton;
    private Coroutine previousTripleshotCoroutine;

    private static readonly Dictionary<int, KeyCode> FIRE_CONTROLS = new()
    {
        [1] = KeyCode.Space,
        [2] = KeyCode.Return,
    };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        id = GetComponent<PlayerIdentity>().Id;
        fireButton = FIRE_CONTROLS[id];
    }

    private void Update()
    {
        // Throttle rate of laser fire
        float currentTime = Time.time;

        if (IsFireButtonPressed() && currentTime > nextAllowedFire)
        {
            // Calculate next allowed time to fire
            nextAllowedFire = currentTime + fireRate;

            // Spawn laser
            GameObject prefab = isTripleShotActive ? tripleShotPrefab : laserPrefab;
            Vector3 laserPosition = transform.position + new Vector3(0f, 1.05f, 0f);
            Instantiate(prefab, laserPosition, Quaternion.identity);

            // Play laser sound
            audioSource.PlayOneShot(laserAudioClip);
        }
    }

    private bool IsFireButtonPressed()
    {
        #if UNITY_IOS
            if (id == 1)
            {
                return Input.GetKeyDown(fireButton) || CrossPlatformInputManager.GetButtonDown("Fire");
            }
            else
            {
                Input.GetKeyDown(fireButton);
            }
        #else
            return Input.GetKeyDown(fireButton);
        #endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag.Powerup))
        {
            var powerup = other.GetComponent<PowerupCollected>();
            if (powerup.Type == PowerupType.TripleShot)
            {
                EnableTripleShot();
            }
        }
    }

    private void EnableTripleShot()
    {
        if (previousTripleshotCoroutine != null)
        {
            StopCoroutine(previousTripleshotCoroutine);
        }
        isTripleShotActive = true;
        previousTripleshotCoroutine = StartCoroutine(Powerup.PowerDownRoutine(() => isTripleShotActive = false));
    }
}
