using UnityEngine;
using TMPro;
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

    public void AddDeath ()
    {
        deaths++;
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
