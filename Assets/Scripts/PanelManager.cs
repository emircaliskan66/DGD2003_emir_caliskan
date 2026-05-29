using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausedPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    private bool isPaused = false;

    private void Start()
    {
        OpenGamePanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverPanel.activeSelf)
        {
            if (isPaused)
                OpenGamePanel();
            else
                OpenPausedPanel();
        }
    }

    public void OpenGamePanel()
    {
        gamePanel.SetActive(true);
        pausedPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenPausedPanel()
    {
        gamePanel.SetActive(false);
        pausedPanel.SetActive(true);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenSettingsPanel()
    {
        gamePanel.SetActive(false);
        pausedPanel.SetActive(false);
        settingsPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenGameOverPanel()
    {
        gamePanel.SetActive(false);
        pausedPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        gamePanel.SetActive(true);
        pausedPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenWinPanel()
    {
        gamePanel.SetActive(false);
        pausedPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}