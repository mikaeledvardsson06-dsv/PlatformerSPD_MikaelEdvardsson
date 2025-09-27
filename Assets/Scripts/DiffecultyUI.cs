using UnityEngine;

public class DiffecultyUI : MonoBehaviour
{
    [SerializeField] private GameObject diffecultyCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    public void ShowDiffecultyCanvas()
    {
        if (diffecultyCanvas != null)
        {
            diffecultyCanvas.SetActive(true);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
    }

    public void HideDiffecultyCanvas()
    {
        if (diffecultyCanvas != null)
        {
            diffecultyCanvas.SetActive(false);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
    }
}
