using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject Shoot;
    public Transform bulletPos;

    private float timer;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

        
        float distance = Vector2.Distance(transform.position, player.transform.position);//hur långt ifrån ska enemy kunna skjuta
        Debug.Log(distance);

        if (distance < 5)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                Fire();
            }
        }




        

    }
    void Fire()
    {
        Instantiate(Shoot, bulletPos.position, Quaternion.identity);
    }
}
