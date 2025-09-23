using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageCooldown = 1f;

    private bool triggered = false;
    private bool active = false;
    private float lastDamageTime;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                StartCoroutine(ActiveTrap());
            }

            if (active)
            {
                PlayerMovement health = other.GetComponent<PlayerMovement>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
        }
    }

    private IEnumerator ActiveTrap()
    {
        if (anim != null)
        {
            anim.SetTrigger("Extend");
        }

        yield return new WaitForSeconds(activationDelay);

        active = true;

        if (anim != null)
        {
            anim.SetBool("TrapActive", true);
        }

        yield return new WaitForSeconds(activeTime);

        active = false;
        if (anim != null)
        {
            anim.SetBool("TrapActive", false);
        }   

        triggered = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && active)
        {
            PlayerMovement health = other.GetComponent<PlayerMovement>();
            if (health != null && Time.time >= lastDamageTime + damageCooldown)
            {
                health.TakeDamage(damageAmount);
                health.StartBurn(1, 2f, 1f);
                lastDamageTime = Time.time;
            }
        }
    }
}   