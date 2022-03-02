using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    private void Update()
    {
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
