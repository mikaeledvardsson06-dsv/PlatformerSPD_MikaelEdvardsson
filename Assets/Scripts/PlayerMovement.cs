using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float jumpForce = 300.0f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private int extraJumpValue = 1;
    private float horizontalValue;
    private bool isGrounded;
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private float rayDistance = 0.25f;
    private int extraJumps;

    private Animator anim;

    private int startingHealth = 5;
    private int currentHealth = 0;

    [SerializeField] Transform SpawnPosition;

    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;
    [SerializeField] private TMP_Text melonsText;
    [SerializeField] private GameObject melonParticles, dustParticles;


    private bool canMove;

    public int melonsCollected = 0;

    private AudioSource audiosource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip[] jumpSounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        currentHealth = startingHealth;
        melonsText.text = "" + melonsCollected; //lurar programmet till att tro att apples �r en string
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        
        if (horizontalValue < 0)
        {
            Flipsprite(true); //ifall player v�ljer att g� till v�nster blir velocity under 0 och d�rf�r flippas den
        }

        if (horizontalValue > 0) 
        { 
            Flipsprite(false); //ifall velocity �r �ver 0 betyder det att spelaren g�r �t h�ger och beh�ver inte flippas
        }
        
        if (CheckIfGrounded())
        {
            extraJumps = extraJumpValue;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (CheckIfGrounded())
            {
                Jump();
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

        
        
    }

    private void FixedUpdate()
    {
        if (canMove == false)
        {
            return; //loopar s� att den inte direkt g�r till hastighetsknapparna ifall knockback sker
        }
        rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.linearVelocity.y); //r�r sig v�nster/h�ger anv�nder movespeed f�r att spelaren ska manuellt kunna best�mma hastigheten, inte unity
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.CompareTag("Melon"))
        {
            Destroy(other.gameObject);
            melonsCollected++;
            melonsText.text = "" + melonsCollected; //lurar programmet till att tro att apples �r en string
            audiosource.pitch = Random.Range(0.8f, 1.2f);
            audiosource.PlayOneShot(pickupSound, 0.7f);
            Instantiate(melonParticles, other.transform.position, Quaternion.identity);//skapar nytt objekt i spelets g�ng vilket �r partikeln, 
        }

        if(other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject); //skickar in ett game objekt till restorehealth
        }
    }

    private void Flipsprite(bool direction) 
    {
        rend.flipX = direction; //ifall direction �r true s� kommer den att flipa sida i x axeln
    }

    private void Jump()
    {
        rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0f);
        rgbd.AddForce(new Vector2(0, jumpForce));
        int randomValue = Random.Range(0, jumpSounds.Length);
        audiosource.PlayOneShot(jumpSounds[randomValue], 0.7f);//spela en g�ng fada in och ut, randomiserar vilken av hoppljuded som spelas
        Instantiate(dustParticles, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }

    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        currentHealth = startingHealth; // respawna med samma health
        UpdateHealthBar();
        transform.position = SpawnPosition.position; 
        rgbd.linearVelocity = Vector2.zero;
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth) //inget sker i full health
        {
            return;
        }
        else
        {
            int healthToRestore = healthPickup.GetComponent<HealthPickup>().HealthAmount;//m�jligg�r redigering i unity
            currentHealth += healthToRestore;
            UpdateHealthBar();
            Destroy(healthPickup);

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }

        }
    }

    public void TakeKnockBack(float knockbackForce, float upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);//delay
    }

    private void UpdateHealthBar()
    {
        HealthSlider.value = currentHealth;


        if (currentHealth >= 2)
        {
            fillColor.color = greenHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }

    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);
        //skickar ett laser och kollar s� att den stoppar vid layern som �r ground

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
