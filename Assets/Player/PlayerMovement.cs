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

    private string horizontalAxis;
    private string verticalAxis;
    private Coroutine previousSpeedCoroutine;

    private void Start()
    {
        var playerId = GetComponent<PlayerIdentity>().Id;
        horizontalAxis = $"Horizontal{playerId}";
        verticalAxis = $"Vertical{playerId}";
    }

    private void Update()
    {
        // Translate based on user input
        float horizontalInput = CrossPlatformInputManager.GetAxis(horizontalAxis);
        float verticalInput = CrossPlatformInputManager.GetAxis(verticalAxis);
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
        if (other.CompareTag(Tag.Powerup))
        {
            var powerup = other.GetComponent<PowerupCollected>();
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
