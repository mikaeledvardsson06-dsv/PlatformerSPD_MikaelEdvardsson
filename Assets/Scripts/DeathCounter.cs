using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text deathText;

    public static DeathCounter current;

    private int deaths = 0;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);

        if (deathText == null)
        {
            deathText = GetComponentInChildren<TMP_Text>();
        }

        UpdateDeathUI();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (deathText != null)
        {
            if (scene.buildIndex == 0)
            {
                deathText.gameObject.SetActive(false);
            }
            else
            {
                deathText.gameObject.SetActive(true);
            }
        }
    }

    public void AddDeath ()
    {
        deaths++;
        UpdateDeathUI();
    }

    public void ResetDeaths()
    {
        deaths = 0;
        UpdateDeathUI();
    }

    private void UpdateDeathUI()
    {
        if (deathText != null)
        {
            deathText.text = deaths.ToString();
        }
    }

    private void OnDestroy()
    {
        if (current == this) current = null;
    }
}
