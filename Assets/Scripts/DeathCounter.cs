using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text deathText;

    public static DeathCounter current;

    private int deaths = 0;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
}
