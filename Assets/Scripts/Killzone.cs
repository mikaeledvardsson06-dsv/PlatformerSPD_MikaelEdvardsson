using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform SpawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = SpawnPosition.position; //reagerar p� killboxen av spelaren
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

}
