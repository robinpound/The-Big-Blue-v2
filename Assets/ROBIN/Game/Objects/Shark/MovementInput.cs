using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
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
        Debug.Log(RightStick.ReadValue<Vector2>());
        transform.position += new Vector3(LeftStick.ReadValue<Vector2>().x, 0f, LeftStick.ReadValue<Vector2>().y) * 0.2f;
        transform.position += new Vector3(RightStick.ReadValue<Vector2>().x, 0f, RightStick.ReadValue<Vector2>().y) * 0.2f;
    }
}
