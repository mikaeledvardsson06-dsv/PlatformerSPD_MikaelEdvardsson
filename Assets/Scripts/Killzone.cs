using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform SpawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (GameOver.example != null)
            {
                GameOver.example.ShowGameOver();    
            }

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

}
