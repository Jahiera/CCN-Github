using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    
    public Vector3 offset = new Vector3(0, 2, 10);
    private Vector3 currentVelocity;

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
    }
}
