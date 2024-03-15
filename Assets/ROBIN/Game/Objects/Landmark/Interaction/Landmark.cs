using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour
{
    private bool HasTriggered;
    [SerializeField] private GameObject Shark;
    [SerializeField] private GameObject SharkInteractor;
    [SerializeField] private GameObject LandmarkCamera;
    [SerializeField] private UIManager uIManager;

    private void Start() {
        HasTriggered = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == SharkInteractor && !HasTriggered){
            MovementInput mInput = Shark.GetComponent<MovementInput>();
            if (mInput.BButton.ReadValue<float>() == 1f){
                HasTriggered = true;
                StartCoroutine(PerformShark());
            }
        }
    }
    IEnumerator PerformShark()
    {
        float elapsedTime = 0f;

        MovementInput mInput = Shark.GetComponent<MovementInput>();
        mInput.animator.SetBool("Bite", true);
        mInput.Invoke("SetIsBitingFalse", 2.5f); // 2.12 seconds is how long the bite animation takes

        while (elapsedTime < 2.12f)
        {
            mInput.rb.AddForce(mInput.transform.forward * mInput.BiteJumpForce , ForceMode.Impulse);
            Quaternion targetRotation = Quaternion.LookRotation(transform.position - Shark.transform.position);
            Shark.transform.rotation = targetRotation;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CameraPan();
    }

    private void CameraPan()
    {
        uIManager.FadeLandmarkTitleTo(1f, 0.8f);
        LandmarkCamera.SetActive(true);
        LandmarkCamera.GetComponent<CamMoveLandmark>().StartMoveSequence();
        Invoke ("SetCameraFalse", 10f);
    }
    private void SetCameraFalse() {
        uIManager.FadeLandmarkTitleTo(0f, 6f);
        uIManager.FadeLandmarkBottomTextTo(1f, 1f);
        LandmarkCamera.SetActive(false);
    }
}
