using UnityEngine;

public class RisingWater : MonoBehaviour
{
  	public float riseSpeed = 2f;
  

    void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
    }

	
	
}
