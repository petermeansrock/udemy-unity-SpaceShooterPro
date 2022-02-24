using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private float speedBoostFactor = 1.0f;

    private float maxY = 0.0f;
    private float minY = -3.8f;
    private float maxX = 11.3f;
    private float minX = -11.3f;

    private Coroutine previousSpeedCoroutine;

    private void Start()
    {
        // Initialize the starting position
        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Powerup.TAG))
        {
            var powerup = other.GetComponent<Powerup>();
            if (powerup.Type == PowerupType.Speed)
            {
                EnableSpeedBoost();
            }
        }
    }

    private void EnableSpeedBoost()
    {
        if (previousSpeedCoroutine != null)
        {
            StopCoroutine(previousSpeedCoroutine);
        }
        speedBoostFactor = 2.0f;
        previousSpeedCoroutine = StartCoroutine(Powerup.PowerDownRoutine(() => speedBoostFactor = 1.0f));
    }
}
