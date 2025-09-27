using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
public void StartGame()
    {
        if (DeathCounter.current != null)
        {
            DeathCounter.current.ResetDeaths();
        }

        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void CloseCredits() 
    {
        creditsPanel.SetActive(false);
    }
}

