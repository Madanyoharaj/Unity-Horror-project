using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gameOverText;
    private bool isGameOver = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    private void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        // Add logic to restart your game, e.g., reloading the scene
    }
}
