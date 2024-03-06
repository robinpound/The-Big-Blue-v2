using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    [Header("X-box Controller Inputs")]
    public InputAction LeftStick;
    public InputAction RightStick;
    public InputAction LT;
    public InputAction LB;
    public InputAction RT;
    public InputAction RB;
    public InputAction BButton;
    public InputAction YButton;
    public InputAction XButton;
    public InputAction AButton;

    [Header("References")]
    public Rigidbody rb;
    public GameObject sharkCamera;

    [Header("Settings")]
    public float rotationSpeed;

    public void OnEnable() {
        LeftStick.Enable();  
        RightStick.Enable();  
        LT.Enable();  LB.Enable();  
        RT.Enable();  RB.Enable();  
        BButton.Enable();  
        YButton.Enable(); 
        XButton.Enable();  
        AButton.Enable();  
        
    }
    public void OnDisable() {
        LeftStick.Disable();  
        RightStick.Disable();  
        LT.Disable();  LB.Disable();  
        RT.Disable();  RB.Disable();  
        BButton.Disable();  
        YButton.Disable(); 
        XButton.Disable();  
        AButton.Disable();
    }
    public void FixedUpdate()
    {
        Vector2 leftStickInput = LeftStick.ReadValue<Vector2>();
        if (leftStickInput != new Vector2(0f,0f)){
            Vector3 cameraSharkForwardDirection = sharkCamera.transform.forward;
            cameraSharkForwardDirection.y = 0f;
            cameraSharkForwardDirection.Normalize();

            // Read input from the left stick of the controller
            Vector3 inputDirection = sharkCamera.transform.right * leftStickInput.x + cameraSharkForwardDirection * leftStickInput.y;
            
            // Calculate the rotation based on the input direction
            Quaternion newRotation = Quaternion.LookRotation(inputDirection, Vector3.up);

            // Assign the new rotation to the transform
            //transform.rotation = newRotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime);

            //rb.AddForce( inputDirection * FORCEMULTIPLIER, ForceMode.Acceleration);
        }
    }
}