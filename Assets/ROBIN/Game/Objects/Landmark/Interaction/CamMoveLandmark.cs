using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveLandmark : MonoBehaviour
{
    [SerializeField] GameObject destination;

    // Adjust this value to control the speed of movement
    [SerializeField] float movementSpeed = 1.0f;

    public void StartMoveSequence()
    {
        Invoke("Corout", 1f);
    }

    private void Corout() => StartCoroutine(Lerping());

    IEnumerator Lerping()
    {
        while (transform.position != destination.transform.position){
            transform.position = Vector3.Lerp(transform.position, destination.transform.position, movementSpeed * Time.deltaTime);
            Debug.Log("Whiling");
            yield return null;
        }
    }
}
