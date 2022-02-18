using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private AudioClip explosionAudioClip;

    private float maxY = 8.0f;
    private float minY = -5.7f;
    private float maxX = 8.0f;
    private float minX = -8.0f;

    private bool isDestroyed = false;

    private Player player;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        if (transform.position.y < minY && !isDestroyed)
        {
            float x = Random.Range(minX, maxX);
            transform.position = new(x, maxY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDestroyed)
        {
            switch (other.tag)
            {
                case "Player":
                    other.GetComponent<Player>().Damage();
                    DestroySelf();
                    break;
                case "Laser":
                    Destroy(other.gameObject);
                    player.IncreaseScore(10);
                    DestroySelf();
                    break;
            }
        }
    }

    void DestroySelf()
    {
        isDestroyed = true;
        animator.SetTrigger("OnEnemyDeath");
        audioSource.Play();
        Destroy(gameObject, 2.8f);
    }
}
