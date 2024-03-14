using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour
{
    private bool HasTriggered;
    [SerializeField] private GameObject Shark;
    [SerializeField] private GameObject SharkInteractor;

    private void Start() {
        HasTriggered = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject == SharkInteractor && !HasTriggered){
            MovementInput mInput = Shark.GetComponent<MovementInput>();
            if (mInput.BButton.ReadValue<float>() == 1f){
                HasTriggered = false;
                StartCoroutine(PerformShark());
            }
        }
    }
    IEnumerator PerformShark()
    {
        float elapsedTime = 0f;

        MovementInput mInput = Shark.GetComponent<MovementInput>();
        mInput.animator.SetBool("Bite", true);
        mInput.Invoke("SetIsBitingFalse", 2.12f); // 2.12 seconds is how long the bite animation takes
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - Shark.transform.position);

        while (elapsedTime < 2.12f)
        {
            mInput.rb.AddForce(mInput.transform.forward * mInput.BiteJumpForce , ForceMode.Impulse);
            Shark.transform.rotation = targetRotation;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
