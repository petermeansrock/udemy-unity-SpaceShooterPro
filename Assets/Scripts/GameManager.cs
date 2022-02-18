using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    void Update()
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

    void RestartGame()
    {
        isGameOver = false;
        SceneManager.LoadScene("Game");
    }
}
