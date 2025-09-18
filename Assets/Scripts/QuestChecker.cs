using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{

    [SerializeField] private GameObject dialougeBox, finishedText, unFinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;

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
                Invoke("LoadNextLevel", 3f);// 2 sec delay på att byta level
                levelIsLoading = true;
                

            }
            else
            {
                dialougeBox.SetActive(true);
                unFinishedText.SetActive(true);
            }
        }
    }

 private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);//byter level
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
