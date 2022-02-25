using UnityEngine;

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

    private Coroutine previousTripleshotCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserAudioClip;
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
            audioSource.Play();
        }
    }

    private bool IsFireButtonPressed()
    {
        #if UNITY_IOS
            return CrossPlatformInputManager.GetButtonDown("Fire");
        #else
            return Input.GetKeyDown(KeyCode.Space);
        #endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Powerup.TAG))
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
