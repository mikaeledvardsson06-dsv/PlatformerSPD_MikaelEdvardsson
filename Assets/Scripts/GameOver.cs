using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private AudioClip deathSound;

    public static GameOver example;
    private AudioSource audioSource;


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

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowGameOver()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            AudioSource musicScore = music.GetComponent<AudioSource>();
            if (musicScore != null)
            {
                musicScore.Stop();
            }
        }

        if (DeathCounter.current != null)
        {
            DeathCounter.current.AddDeath();
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound, 1f); 
        }

        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            AudioSource musicScore = music.GetComponent<AudioSource>();
            if (musicScore != null)
            {
                musicScore.Play();
            }
        }

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
