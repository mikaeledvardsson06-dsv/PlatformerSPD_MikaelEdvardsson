using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float bounce = 100;
    private SpriteRenderer rend;

    [SerializeField] private int damageGiven = 1;

    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;

    private bool canMove = true;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0) 
        { 
            rend.flipX = true;
        }
        if (moveSpeed > 0)
        {
            rend.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))//är objektet jag kolliderar med av typen enemyblock?
        {
            moveSpeed = -moveSpeed; //-- = +
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed; //-- = +
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);//hittar koden från andra scriptet och skickar över till denna då den e public 

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockbackForce, upwardForce);
            }

            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockbackForce, upwardForce);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(other.GetComponent<Rigidbody2D>().linearVelocity.x, 0);

            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce));

            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;//stänger av colliders för spelaren så att den inte stör en efter den är död 
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;// dock då stängs collidersen även av för spelarens ground, därför ska scale för gravity defaultas
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;//stannar fienden
            canMove = false;

            Destroy(gameObject,0.6f);//tar bort hela spelobjektet när box triggas

            
        }
    }
}
