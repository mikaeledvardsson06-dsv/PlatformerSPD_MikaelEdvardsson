using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2f;

    private Transform currentTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTarget = target1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == target1.position)
        {
            currentTarget = target2;
        }

        if (transform.position == target2.position)
        {
            currentTarget = target1;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);//transform är bra för punkt a till b

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);//prioriterar platformens egenskaper över ground, behövs för att ge platform ground egenskaper och följer platformen
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
