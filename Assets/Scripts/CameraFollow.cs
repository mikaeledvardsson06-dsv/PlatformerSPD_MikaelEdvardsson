using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0,0,-10f);
    [SerializeField] private float smoothing = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        //lerp flyttar fr�n en punkt till en annan punkt �ver en viss tid, vart kameran �r j�mf�rt med vart spelaren �r + offseten * tiden p� lurpen
        transform.position = newPosition;
    }
}
