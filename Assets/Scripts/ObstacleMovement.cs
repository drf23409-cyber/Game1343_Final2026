using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float obstacleSpeed = 10f;
    public float destroyZ = -10f;

    
    void Update()
    {
        transform.position += Vector3.back * obstacleSpeed * Time.deltaTime;

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
