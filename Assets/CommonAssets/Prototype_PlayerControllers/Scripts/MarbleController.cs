using UnityEngine;
using UnityEngine.InputSystem;

public class MarbleController : MonoBehaviour
{
    public GameObject CameraControl;
    public GameObject MarbleModel;

    public bool ShowDebug = false; 
    public float MouseSensitivty = 15;
    public float MoveAccelleration = 1;
    public float MoveMaxSpeed = 15;
    public float RotationRate = 90;
    public float PitchRate = 90;
    public Vector2 PitchRange = new Vector2(-89, 89);
    public bool InvertCamVerticle = true;

    Rigidbody rb;

    Vector3 flipVel = Vector3.zero;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        Keyboard kb = Keyboard.current;

        if ((mouse == null) || (kb == null))
        {
            Debug.LogError("No Keyboard or Mouse found");
            return;
        }

        Vector2 mouseDelta = mouse.delta.ReadValue();
        Vector2 moveDelta = Vector2.zero;

        if (kb.wKey.isPressed) { moveDelta.y = 1; }
        if (kb.sKey.isPressed) { moveDelta.y = -1; }
        if (kb.aKey.isPressed) { moveDelta.x = -1; }
        if (kb.dKey.isPressed) { moveDelta.x = 1; }


        //SetCamPitch(mouseDelta.y);
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
        nextPitch = nextPitch + delta;

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
        rb.linearVelocity += Direction * MoveAccelleration;

        if (rb.linearVelocity.magnitude > MoveMaxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * MoveMaxSpeed;
        }
        if (ShowDebug)
        { 
            Debug.Log("S/D: " + rb.linearVelocity.magnitude + " :: " + rb.linearVelocity);
        }
        // We need to flip the X and Z to get the correct Axis... 
        // Also the sign, to get the correct look of direction. 

        flipVel.x = -rb.linearVelocity.z;
        flipVel.z = -rb.linearVelocity.x;
        MarbleModel.transform.Rotate(flipVel * RotationRate * Time.deltaTime, Space.Self);
    }

    /* 
     * Marble Mat Credits: 
     * Hand Painted Stone Wall Texture
     * The Chayed
     * OGA-BY 3.0
     * https://opengameart.org/content/hand-painted-stone-wall-texture
    */
}
