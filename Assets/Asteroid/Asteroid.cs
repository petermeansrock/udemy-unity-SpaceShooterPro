using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15.0f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private UnityEvent destroyedEvent;

    private Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag.Laser))
        {
            myCollider.enabled = false;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            destroyedEvent.Invoke();
            Destroy(gameObject, 0.15f);
        }
    }
}
