using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AktiveraLight : MonoBehaviour
{
    public Light2D globalLight;
    //public Light2D[] extraLights;
    public float onIntensity = 0f;
    public float offIntensity = 1.0f;



    private void Start()
    {
        if (globalLight != null)
        {
            globalLight.intensity = offIntensity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          if (globalLight != null)
            {
                globalLight.intensity = onIntensity;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)

    {
        if (other.CompareTag("Player"))
        {
            if (globalLight != null)
            {
                globalLight.intensity = offIntensity;
            }
        }
    }
}

