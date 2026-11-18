using UnityEngine;
using UnityEngine.InputSystem; 

public class FPSController : MonoBehaviour
{
    public GameObject CameraControl;

    public float MouseSensitivty = 15;
    public float MoveRate = 5;
    public float RotationRate = 90;
    public float PitchRate = 90;
    public Vector2 PitchRange = new Vector2(-89, 89);
    public bool InvertCamVerticle = true; 

    Rigidbody rb; 
     

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        Keyboard kb = Keyboard.current; 

        if ( (mouse == null) || (kb == null) )
        {
            Debug.LogError("No Keyboard or Mouse found"); 
            return; 
        }

        Vector2 mouseDelta = mouse.delta.ReadValue();
        Vector2 moveDelta = Vector2.zero; 

        if (kb.wKey.isPressed) { moveDelta.y = 1;  }
        if (kb.sKey.isPressed) { moveDelta.y = -1; }
        if (kb.aKey.isPressed) { moveDelta.x = -1; }
        if (kb.dKey.isPressed) { moveDelta.x = 1; }

       
        SetCamPitch(mouseDelta.y);
        RotatePlayer(mouseDelta.x);
        Move(moveDelta.x, moveDelta.y); 

    }

    void SetCamPitch(float value)
    {
        if (value == 0)
        {
            return; 
        }

        if (InvertCamVerticle)
        {
            value *= -1; 
        }

        float nextPitch = CameraControl.transform.rotation.eulerAngles.x;
        if (nextPitch > 180)
        {
            nextPitch -= 360; 
        }
       
        float delta = (value * MouseSensitivty * PitchRate * Time.deltaTime);
        nextPitch = nextPitch + delta ;
        
        // Restrain with in Riange
        if (nextPitch < PitchRange.x)
        {
               nextPitch = PitchRange.x;
        }

        if (nextPitch > PitchRange.y)
        {
            nextPitch = PitchRange.y;
        }
        
        Quaternion r = Quaternion.Euler(nextPitch, 0, 0); 
        CameraControl.transform.localRotation = r;
    }

    void RotatePlayer(float value)
    {
        gameObject.transform.Rotate(Vector3.up * value * MouseSensitivty * RotationRate * Time.deltaTime); 
    }

    void Move(float horizontal, float vertical)
    {
        Vector3 Direction = (gameObject.transform.forward * vertical) + (gameObject.transform.right * horizontal);
        Direction = Direction.normalized;
        rb.linearVelocity = Direction * MoveRate; 
    }
}
