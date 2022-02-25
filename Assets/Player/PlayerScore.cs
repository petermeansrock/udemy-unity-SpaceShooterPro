using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private UnityEvent<int> scoreUpdatedEvent;

    public void IncreaseScore(int points)
    {
        score += points;
        scoreUpdatedEvent.Invoke(score);
    }
}
