using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private AudioClip explosionAudioClip;
    [SerializeField]
    public UnityEvent<int> destroyedByLaserEvent;

    private float maxY = 8.0f;
    private float minY = -5.7f;
    private float maxX = 8.0f;
    private float minX = -8.0f;

    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;

    public const string TAG = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource.clip = explosionAudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        // Move down
        transform.Translate(speed * Time.deltaTime * Vector3.down);

        // Respawn at top in new random X position
        if (transform.position.y < minY && boxCollider.enabled)
        {
            float x = Random.Range(minX, maxX);
            transform.position = new(x, maxY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Player.TAG:
                DestroySelf();
                break;
            case Laser.TAG:
                destroyedByLaserEvent.Invoke(10);
                DestroySelf();
                break;
        }
    }

    void DestroySelf()
    {
        boxCollider.enabled = false;
        animator.SetTrigger("OnEnemyDeath");
        audioSource.Play();
        Destroy(gameObject, 2.8f);
    }
}
