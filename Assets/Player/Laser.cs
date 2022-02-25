using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 8.0f;

    private float maxY = 8.0f;

    public const string TAG = "Laser";

    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        ConditionallyDespawn();
    }

    private void ConditionallyDespawn()
    {
        if (transform.position.y > maxY)
        {
            Transform parent = transform.parent;
            if (parent != null)
            {
                Destroy(parent.gameObject);
            } else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Enemy.TAG:
            case Asteroid.TAG:
                Destroy(gameObject);
                break;
        }
    }
}
