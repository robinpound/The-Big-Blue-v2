using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public InputAction controls;
    
    private void OnEnable() {
        Debug.Log("Enabled");
        controls.Enable();  
    }
    private void OnDisable() {
        Debug.Log("Disabled");
        controls.Disable();
    }
    public void Update() {
        if(controls.triggered) {
            Debug.Log("Pressed");
            SceneManager.LoadSceneAsync(0);
        }
    }
    // Hello Owen!
}
