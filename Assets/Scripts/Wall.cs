using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject Box;
    private Animator anim;
    private bool hasPlayedAnim = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnim)
        {
            Box.SetActive(false);
            hasPlayedAnim = true;
            anim.SetTrigger("Move");
        }
    }
}
