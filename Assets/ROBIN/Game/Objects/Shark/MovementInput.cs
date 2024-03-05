using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    public InputAction movementControls;

    private void OnEnable() {
        Debug.Log("Enabled");
        movementControls.Enable();  
    }
    private void OnDisable() {
        Debug.Log("Disabled");
        movementControls.Disable();
    }
    void FixedUpdate()
    {
        Debug.Log(movementControls.actionMap.actions);
        //transform.position += new Vector3(movementControls.ReadValue<Vector2>().x, 0f, movementControls.ReadValue<Vector2>().y) * 0.2f;
    }
}
