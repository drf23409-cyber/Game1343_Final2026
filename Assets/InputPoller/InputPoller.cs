using UnityEngine;
using UnityEngine.InputSystem;



// Player Number Refernce
// #0 : Gamepad Input 
// #1 : Keyboard mapped to Gamepad 
// #2 : Keyboard As Left JoyCon mapped to Gamepad
// #3 : Keyboard as Right JoyCon mapped to Gamepad

// !!!! Warning !!!!
// Multiple Gamepads requires much more code! 



public class InputPoller : MonoBehaviour
{

    public static InputPoller reference; 


    void Awake()
    {
        if (reference != null)
        {
            // we have another instance of this system. 
            // the Solution here is to delete the other version 
            Debug.LogWarning("Found another instance of InputPoller on " + reference.gameObject.name); 
            Destroy(reference); 
        }
        reference = this;
       
    }



    /// <summary>
    /// Call this function to get Input Data from your controlling script. 
    /// </summary>
    /// <param name="PlayerNumber"></param>
    /// <returns></returns>
    public InputData GetInput(int PlayerNumber)
    {
        if (PlayerNumber == 0) { return GetInputP0(); }
        if (PlayerNumber == 1) { return GetInputP1(); }
        if (PlayerNumber == 2) { return GetInputP2(); }
        if (PlayerNumber == 3) { return GetInputP3(); }

        // Defaut return 
        return InputData.CleanDataStruct();

    }


    
    // This is the First GamePad Connected Info
    public InputData GetInputP0()
    {
        InputData input = InputData.CleanDataStruct();

        Gamepad gpad = Gamepad.current;
        // verify we have gamepad data
        if (gpad != null)
        {
            input.leftStick = gpad.leftStick.ReadValue();
            input.rightStick = gpad.rightStick.ReadValue();
            input.leftStickButton = gpad.leftStickButton.wasPressedThisFrame;
            input.rightStickButton = gpad.rightStickButton.wasPressedThisFrame;
            input.dpad = gpad.dpad.ReadValue();
            input.leftTrigger = gpad.leftTrigger.ReadValue();
            input.rightTrigger = gpad.rightTrigger.ReadValue();
            input.leftShoulder = gpad.leftShoulder.wasPressedThisFrame;
            input.rightShoulder = gpad.rightShoulder.wasPressedThisFrame;
            input.buttonNorth = gpad.buttonNorth.wasPressedThisFrame;
            input.buttonSouth = gpad.buttonSouth.wasPressedThisFrame;
            input.buttonEast = gpad.buttonEast.wasPressedThisFrame;
            input.buttonWest = gpad.buttonWest.wasPressedThisFrame;
            input.startButton = gpad.startButton.wasPressedThisFrame;
            input.selectButton = gpad.selectButton.wasPressedThisFrame;
        }

        return input; 
    }



    // this is Keyboard only
    // WASD = Left Stick, IJKL = Right Stick, Arrows = Dpad
    public InputData GetInputP1()
    {
        InputData input = InputData.CleanDataStruct();

        Keyboard kb = Keyboard.current;
        // Verifiy we have Keyboard data 
        if (kb != null)
        {
            // WASD
            if (kb.wKey.isPressed) { input.leftStick.y = +1; }
            if (kb.sKey.isPressed) { input.leftStick.y = -1; }
            if (kb.aKey.isPressed) { input.leftStick.x = -1; }
            if (kb.dKey.isPressed) { input.leftStick.x = +1; }

            // QEZC = Face\Cardnal Buttons 
            if (kb.qKey.wasPressedThisFrame) { input.buttonNorth = true; }
            if (kb.eKey.wasPressedThisFrame) { input.buttonEast = true; }
            if (kb.cKey.wasPressedThisFrame) { input.buttonSouth = true; }
            if (kb.zKey.wasPressedThisFrame) { input.buttonWest = true; }

            // IJKL
            if (kb.iKey.isPressed) { input.rightStick.y = +1; }
            if (kb.kKey.isPressed) { input.rightStick.y = -1; }
            if (kb.jKey.isPressed) { input.rightStick.x = -1; }
            if (kb.lKey.isPressed) { input.rightStick.x = +1; }

            // ARROW KEYS
            if (kb.upArrowKey.isPressed) { input.dpad.y = +1; }
            if (kb.downArrowKey.isPressed) { input.dpad.y = -1; }
            if (kb.leftArrowKey.isPressed) { input.dpad.x = -1; }
            if (kb.rightArrowKey.isPressed) { input.dpad.x = +1; }
        }
        return input;
    }

 

    // this is Keyboard only \\ Left Joy Con Simulation
    // WASD = ThumbStick 
    public InputData GetInputP2()
    {
        InputData input = InputData.CleanDataStruct();

        Keyboard kb = Keyboard.current;
        // Verifiy we have Keyboard data 
        if (kb != null)
        {
            // WASD = ThumbStick
            if (kb.wKey.isPressed) { input.leftStick.y = +1; }
            if (kb.sKey.isPressed) { input.leftStick.y = -1; }
            if (kb.aKey.isPressed) { input.leftStick.x = -1; }
            if (kb.dKey.isPressed) { input.leftStick.x = +1; }

            // QEZC = Face\Cardnal Buttons 
            if (kb.qKey.wasPressedThisFrame) { input.buttonNorth = true; }
            if (kb.eKey.wasPressedThisFrame) { input.buttonEast = true; }
            if (kb.cKey.wasPressedThisFrame) { input.buttonSouth = true; }
            if (kb.zKey.wasPressedThisFrame) { input.buttonWest = true; }

            // xr =  start select
            if (kb.xKey.wasPressedThisFrame) { input.startButton = true; }
            if (kb.rKey.wasPressedThisFrame) { input.selectButton= true; }
        }
        return input;
    }



    // this is Keyboard only \\ Right Joy Con Simulation
    // maps same way as P2 for our convience. 
    // IJKL = ThumbStick 
    // uo.m = Face Buttons 
    public InputData GetInputP3()
    {
        InputData input = InputData.CleanDataStruct();

        Keyboard kb = Keyboard.current;
        // Verifiy we have Keyboard data 
        if (kb != null)
        {
            // ijkl = ThumbStick
            if (kb.iKey.isPressed) { input.leftStick.y = +1; }
            if (kb.kKey.isPressed) { input.leftStick.y = -1; }
            if (kb.jKey.isPressed) { input.leftStick.x = -1; }
            if (kb.lKey.isPressed) { input.leftStick.x = +1; }

            // uo.m = Face\Cardnal Buttons 
            if (kb.uKey.wasPressedThisFrame) { input.buttonNorth = true; }
            if (kb.oKey.wasPressedThisFrame) { input.buttonEast = true; }
            if (kb.periodKey.wasPressedThisFrame) { input.buttonSouth = true; }
            if (kb.mKey.wasPressedThisFrame) { input.buttonWest = true; }

            // ,p =  start select
            if (kb.commaKey.wasPressedThisFrame) { input.startButton = true; }
            if (kb.pKey.wasPressedThisFrame) { input.selectButton = true; }
        }
        return input;
    }

}



/*
JOY CON REFERENCE DATA 

 Joy-Con L: 
     Left Stick(clickable)   
     directional buttons, 
     Select (as -), 
     Capture, 
     L, ZL, 
     SL, SR, 

 Joy-Con R: 
     Right Stick(clickable)
     A, B, X, Y, 
     Start(as +), 
     HOME, 
     R, ZR,   // 
     SL, SR,  // inside rail buttons 

 */