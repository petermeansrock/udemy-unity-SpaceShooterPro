using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void EndGame()
    {
        isGameOver = true;
    }

    private void RestartGame()
    {
        isGameOver = false;
        SceneManager.LoadScene("Game");
    }
}
