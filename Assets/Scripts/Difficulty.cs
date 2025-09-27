using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty current;

    public int playerHealth = 5;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    public void setEasy()
    {
        playerHealth = 10;
        Debug.Log("You selected Easy Mode!  Player Health:" + Difficulty.current.playerHealth);
    }

    public void setMedium()
    {
        playerHealth = 5;
        Debug.Log("You selected Medium Mode!  Player Health:" + Difficulty.current.playerHealth);
    }

    public void setHard()
    {
        playerHealth = 2;
        Debug.Log("You selected Hard Mode!  Player Health:" + Difficulty.current.playerHealth);
    }
}
