using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject LandmarkTitle;
    [SerializeField] GameObject LandmarkBottomText;

    public void FadeLandmarkTitleTo(float alpha, float speed) => StartCoroutine(LerpAlpha(alpha, LandmarkTitle, speed));
    public void FadeLandmarkBottomTextTo(float alpha, float speed) => StartCoroutine(LerpAlpha(alpha, LandmarkBottomText, speed));
    

    private IEnumerator LerpAlpha(float targetAlpha, GameObject image, float lerpSpeed) {
        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        
        while (Mathf.Abs(cg.alpha - targetAlpha) > 0.1f)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, targetAlpha, lerpSpeed * Time.deltaTime);
            yield return null;
        }
        cg.alpha = targetAlpha;
    }
}
