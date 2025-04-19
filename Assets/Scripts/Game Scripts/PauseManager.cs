using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject gameOverPanel;
    public FloatValue playerHealth;

    void Start() {
        isPaused = false;    
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("pause")) {
            PauseGame();
        }

        if (Input.GetButtonDown("Inventory")) {
            OpenInventory();
        }

        if (playerHealth.runtimeValue <= 0) {
            gameOverPanel.SetActive(true);
        }

        // if cheese is not in inventory AND NPC quest is 'completed'...
    }

    public void PauseGame() {
        isPaused = !isPaused;
        if (isPaused) {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OpenInventory() {
        isPaused = !isPaused;
        if (isPaused) {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void GameOver() {
        isPaused = !isPaused;
        if (isPaused) {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void TryAgain() {
        SceneManager.LoadScene("LoadingScreen");
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitToMain() {
        SceneManager.LoadScene("MainMenu");
    }
}
