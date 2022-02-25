using UnityEngine;

public class PowerupMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    private float minY = -5.7f;

    private void Update()
    {
        // Move down at a specific speed and despawn at bottom of screen
        transform.Translate(speed * Time.deltaTime * Vector3.down);
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
