using System;
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
    public Animator animator;

    [Header("Settings")]
    public float rotationSpeed;
    public float swimSpeed;

    [Header("InternalVaribles")]
    private float previousyRotation;

    public void Start() {
        previousyRotation = transform.rotation.eulerAngles.y;
    }

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
        Animate();
        if (LeftStick.ReadValue<Vector2>() != new Vector2(0f,0f)){
            RotateTo();
            SwimForward();
        }
    }

    private void SwimForward()
    {
        rb.AddForce(transform.forward * swimSpeed, ForceMode.Force); // Swim
    }

    private void RotateTo()
    {
        Vector2 leftStickInput = LeftStick.ReadValue<Vector2>();

        // Read LT and RT input values
        float ltInput = LT.ReadValue<float>();
        float rtInput = RT.ReadValue<float>();

        Vector3 cameraSharkForwardDirection = sharkCamera.transform.forward;
                cameraSharkForwardDirection.y = 0f;
                cameraSharkForwardDirection.Normalize();
        //Vector3 inputDirection = sharkCamera.transform.right * leftStickInput.x + cameraSharkForwardDirection * leftStickInput.y;
        Vector3 inputDirection = (sharkCamera.transform.right * leftStickInput.x + cameraSharkForwardDirection * leftStickInput.y) + sharkCamera.transform.up * (rtInput - ltInput);

        Quaternion desiredRotation = Quaternion.LookRotation(inputDirection, Vector3.up);

        float t = 1f - Mathf.Exp(-rotationSpeed * Time.fixedDeltaTime);               // Apply Exponential rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, t); // Rotate
    }

    public void Animate(){
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float yRotation = Mathf.Repeat(currentRotation.y, 360f);
        float yRotationChange = yRotation - previousyRotation;
        if (Mathf.Abs(yRotationChange) > 180f)
        {
            yRotationChange -= Mathf.Sign(yRotationChange) * 360f;
        }
        float value = Mathf.Lerp(animator.GetFloat("yANI"), yRotationChange/4, Time.deltaTime * 10);
        animator.SetFloat("yANI", value, 0f, Time.deltaTime);
        animator.SetFloat("zANI", -(LT.ReadValue<float>() - RT.ReadValue<float>()) * 0.5f, 0f, Time.deltaTime);

        previousyRotation = yRotation; // update for next time     
    }
}