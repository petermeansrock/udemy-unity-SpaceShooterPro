using System.Collections;
using System.Collections.Generic;
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

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
        gameManager.EndGame();
    }

    IEnumerator FlashGameOverTextRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameOverFlashRate);
            gameOverText.enabled = !gameOverText.enabled;
        }
    }
}
