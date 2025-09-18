using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    [SerializeField] private GameObject textPopUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(true); //stänger av check för objektet
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(false);
        }
    }
}
