using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private AudioClip explosionAudioClip;
    [SerializeField]
    public UnityEvent<int> destroyedByLaserEvent;

    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;

    private bool isAlive = true;
    public bool IsAlive => isAlive;

    // Start is called before the first frame update
    void Start()
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
        isAlive = false;
        boxCollider.enabled = false;
        animator.SetTrigger("OnEnemyDeath");
        audioSource.Play();
        Destroy(gameObject, 2.8f);
    }
}
