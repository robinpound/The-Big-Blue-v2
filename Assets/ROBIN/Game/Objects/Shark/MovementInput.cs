using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    private Vector3 previousRotation;

    public void Start() {
        previousRotation = transform.rotation.eulerAngles;
        //animator.SetFloat("Rx", xAxisAnimation, 0f, Time.deltaTime);
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
        Vector3 cameraSharkForwardDirection = sharkCamera.transform.forward;
        cameraSharkForwardDirection.y = 0f;
        cameraSharkForwardDirection.Normalize();

        Vector3 inputDirection = sharkCamera.transform.right * leftStickInput.x + cameraSharkForwardDirection * leftStickInput.y;
        Quaternion desiredRotation = Quaternion.LookRotation(inputDirection, Vector3.up);

        float t = 1f - Mathf.Exp(-rotationSpeed * Time.fixedDeltaTime);               // Apply Exponential rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, t); // Rotate
    }

    public void Animate(){
        float xRotationChange = transform.rotation.eulerAngles.x - previousRotation.x;
        float yRotationChange = transform.rotation.eulerAngles.y - previousRotation.y; // left, right 
        float zRotationChange = transform.rotation.eulerAngles.z - previousRotation.z;
        //animator.SetFloat("xANI", yRotationChange/8, 0f, Time.deltaTime);
        Debug.Log(yRotationChange);
        previousRotation = transform.rotation.eulerAngles; // update for next time        
    }
}