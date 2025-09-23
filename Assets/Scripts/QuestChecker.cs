using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{

    [SerializeField] private GameObject dialougeBox, finishedText, unFinishedText;
    [SerializeField] private int questGoal = 7;
    [SerializeField] private LevelLoader levelLoader;

    private Animator anim;
    private bool levelIsLoading = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().melonsCollected >= questGoal)//checkar variabeln i melons från spelarens kod
            {

                dialougeBox.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");

                levelIsLoading = true;

                Invoke(nameof(StartLevelTransition), 3f);


            }
            else
            {
                dialougeBox.SetActive(true);
                unFinishedText.SetActive(true);
            }
        }
    }

 private void StartLevelTransition()
    {
        levelLoader.LoadNextLevel();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialougeBox.SetActive(false);
            finishedText.SetActive(false);
            unFinishedText.SetActive(false);
        }
    }

}
