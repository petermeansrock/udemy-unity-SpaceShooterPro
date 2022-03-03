using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
    private UnityEvent mainMenuRequestedEvent;
    [SerializeField]
    private GameObject pausePanel;

    private bool isPaused = false;

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            PauseGame();
        }
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

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        mainMenuRequestedEvent.Invoke();
    }
}
