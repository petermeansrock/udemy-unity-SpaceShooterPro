using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;
    [SerializeField]
    private GameObject pausePanel;

    private bool isPaused = false;

    private void Update()
    {
        // Handle input after "Game Over" has been reached
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMainMenu();
            }
        }
        // Handle input to exit application
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // Handle input to pause game
        else if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            PauseGame();
        }
    }

    public void EndGame()
    {
        isGameOver = true;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
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
}
