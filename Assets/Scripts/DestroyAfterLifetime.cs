using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{

    [SerializeField] private float lifetime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

 
}
