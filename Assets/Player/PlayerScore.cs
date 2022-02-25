using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private int score;

    private UiManager uiManager;

    public const string TAG = "Player";

    private void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }
}
