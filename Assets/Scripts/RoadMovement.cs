  using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public float roadSpeed = 5f;
    public float roadLength = 20f;
    public float resetZ = -10f;

    
    void Update()
    {
        // Move the road backward
        transform.position += Vector3.back * roadSpeed * Time.deltaTime;
        // Move the road back to the front
        if (transform.position.z <= resetZ)
        {
            transform.position += Vector3.forward * roadLength * 2f;
        }
    }
}
