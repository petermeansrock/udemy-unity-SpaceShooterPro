using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject[] engines;
    [SerializeField]
    private UnityEvent deathEvent;
    [SerializeField]
    private UnityEvent<int> livesUpdatedEvent;

    private void Start()
    {
        // Randomize engine order
        for (int i = 0; i < engines.Length - 1; i++)
        {
            int j = Random.Range(i, engines.Length);
            var temp = engines[i];
            engines[i] = engines[j];
            engines[j] = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case Tag.Enemy:
                Damage();
                break;
            case Tag.Powerup:
                var powerup = other.GetComponent<PowerupCollected>();
                if (powerup.Type == PowerupType.Shield)
                {
                    EnableShield();
                }
                break;
        }
    }

    private void Damage()
    {
        if (shield.activeSelf)
        {
            shield.SetActive(false);
        }
        else if (lives > 0)
        {
            lives--;
            livesUpdatedEvent.Invoke(lives);

            if (lives <= 0)
            {
                deathEvent.Invoke();
                Destroy(this.gameObject);
            }
            else
            {
                engines[lives - 1].SetActive(true);
            }
        }
    }

    private void EnableShield()
    {
        shield.SetActive(true);
    }
}
