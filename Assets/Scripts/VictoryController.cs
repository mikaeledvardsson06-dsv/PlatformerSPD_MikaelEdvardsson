using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

}


