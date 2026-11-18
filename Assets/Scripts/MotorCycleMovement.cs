using UnityEngine;
using UnityEngine.InputSystem;

public class MotorCycleMovement : MonoBehaviour
{
    public float laneDistance = 2f;
    public float switchSpeed = 8f;

    int currentLane = 1;

    
    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb == null)
            return;
        if (kb.aKey.wasPressedThisFrame && currentLane > 0)
        {
            currentLane--;
        }

        if (kb.dKey.wasPressedThisFrame && currentLane < 2)
        {
            currentLane++;
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x = (currentLane - 1) * laneDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, switchSpeed * Time.deltaTime);
    }
}
