using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float powerupPeriod = 5.0f;
    [SerializeField]
    private int score;
    [SerializeField]
    private GameObject[] engines;
    [SerializeField]
    private AudioClip laserAudioClip;
    [SerializeField]
    private UnityEvent deathEvent;

    private AudioSource audioSource;

    private UiManager uiManager;

    private float nextAllowedFire = 0.0f;
    private bool isTripleShotActive = false;
    private float speedBoostFactor = 1.0f;

    private float maxY = 0.0f;
    private float minY = -3.8f;
    private float maxX = 11.3f;
    private float minX = -11.3f;

    private Coroutine previousTripleshotCoroutine;
    private Coroutine previousSpeedCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the starting position
        transform.position = new Vector3(0, 0, 0);

        // Randomize engine order
        for (int i = 0; i < engines.Length - 1; i++)
        {
            int j = UnityEngine.Random.Range(i, engines.Length);
            var temp = engines[i];
            engines[i] = engines[j];
            engines[j] = temp;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserAudioClip;

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        HandleFire();
    }

    void CalculateMovement()
    {
        // Translate based on user input
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 direction = new(horizontalInput, verticalInput, 0);
        transform.Translate(speed * speedBoostFactor * Time.deltaTime * direction);

        // Bound player movement on the y-axis
        float y = Mathf.Clamp(transform.position.y, minY, maxY);

        // Wrap player movement on the x-axis
        float x = transform.position.x;
        if (x > maxX)
        {
            x = (x - maxX) + minX;
        }
        else if (x < minX)
        {
            x = maxX - (minX - x);
        }

        // Apply bounds restriction
        transform.position = new(x, y, transform.position.z);
    }

    void HandleFire()
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

    bool IsFireButtonPressed()
    {
        #if UNITY_IOS
            return CrossPlatformInputManager.GetButtonDown("Fire");
        #else
            return Input.GetKeyDown(KeyCode.Space);
        #endif
    }

    public void Damage()
    {
        if (shield.activeSelf)
        {
            shield.SetActive(false);
        }
        else
        {
            lives--;
            uiManager.UpdateLives(lives);

            if (lives <= 0)
            {
                deathEvent.Invoke();
                Destroy(this.gameObject);
            } else
            {
                engines[lives - 1].SetActive(true);
            }
        }
    }

    public void EnableTripleShot()
    {
        if (previousTripleshotCoroutine != null)
        {
            StopCoroutine(previousTripleshotCoroutine);
        }
        isTripleShotActive = true;
        previousTripleshotCoroutine = StartCoroutine(PowerDownRoutine(() => isTripleShotActive = false));
    }

    public void EnableSpeedBoost()
    {
        if (previousSpeedCoroutine != null)
        {
            StopCoroutine(previousSpeedCoroutine);
        }
        speedBoostFactor = 2.0f;
        previousSpeedCoroutine = StartCoroutine(PowerDownRoutine(() => speedBoostFactor = 1.0f));
    }

    public void EnableShield()
    {
        shield.SetActive(true);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    IEnumerator PowerDownRoutine(Action action)
    {
        yield return new WaitForSeconds(powerupPeriod);
        action.Invoke();
    }
}
