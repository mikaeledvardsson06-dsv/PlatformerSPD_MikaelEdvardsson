using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform SpawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (DeathCounter.current != null)
            {
                DeathCounter.current.AddDeath();
            }

            other.transform.position = SpawnPosition.position; //reagerar på killboxen av spelaren
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

}
