using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image livesImage;
    [SerializeField]
    private Sprite[] liveSprites;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private float gameOverFlashRate = 0.5f;
    [SerializeField]
    private GameObject pausePanel;

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int lives)
    {
        livesImage.sprite = liveSprites[lives];
    }

    public void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(FlashGameOverTextRoutine());
    }

    private IEnumerator FlashGameOverTextRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameOverFlashRate);
            gameOverText.enabled = !gameOverText.enabled;
        }
    }

    public void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pausePanel.SetActive(false);
    }
}
