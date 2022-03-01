using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 8.0f;
    [SerializeField]
    private string ownerTag;
    public string OwnerTag => ownerTag;
    [SerializeField]
    private Vector3 direction = Vector3.up;

    private float maxY = 8.0f;
    private float minY = -5.7f;

    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(speed * Time.deltaTime * direction);
        ConditionallyDespawn();
    }

    private void ConditionallyDespawn()
    {
        if (transform.position.y > maxY || transform.position.y < minY)
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
            case Tag.Enemy:
            case Tag.Asteroid:
                // Do not allow lasers to be consumed by the owner
                if (!other.CompareTag(ownerTag))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
