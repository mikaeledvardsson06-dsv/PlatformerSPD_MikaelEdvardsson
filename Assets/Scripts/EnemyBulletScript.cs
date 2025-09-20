using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public int damageGiven = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() //skottet f�ljer spelaren
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2 (direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);//skottet roterar och uppdateras snabbare spelarens position
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {

            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);//hittar koden fr�n andra scriptet och skickar �ver till denna d� den e public 
            Destroy(gameObject);
        }
    }
}
