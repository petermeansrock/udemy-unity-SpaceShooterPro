using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private AudioClip explosionAudioClip;
    public UnityEvent<int> destroyedByLaserEvent;

    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;

    private bool isAlive = true;
    public bool IsAlive => isAlive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource.clip = explosionAudioClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Tag.Player:
                DestroySelf();
                break;
            case Tag.Laser:
                destroyedByLaserEvent.Invoke(10);
                DestroySelf();
                break;
        }
    }

    private void DestroySelf()
    {
        isAlive = false;
        boxCollider.enabled = false;
        animator.SetTrigger("OnEnemyDeath");
        audioSource.Play();
        Destroy(gameObject, 2.8f);
    }
}
