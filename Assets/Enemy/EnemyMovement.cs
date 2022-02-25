using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;

    private EnemyHealth health;

    private float maxY = 8.0f;
    private float minY = -5.7f;
    private float maxX = 8.0f;
    private float minX = -8.0f;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        // Move down
        transform.Translate(speed * Time.deltaTime * Vector3.down);

        // Respawn at top in new random X position
        if (health.IsAlive && transform.position.y < minY)
        {
            float x = Random.Range(minX, maxX);
            transform.position = new(x, maxY, transform.position.z);
        }
    }
}
