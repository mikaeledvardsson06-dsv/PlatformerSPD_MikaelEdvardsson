using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    public static GameOver example;


    private void Awake()
    {
        if (example != null && example != this)
        {
            Destroy(gameObject);
            return;
        }

        example = this;

        DontDestroyOnLoad(gameObject);

        gameOver.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void OnDetsroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
