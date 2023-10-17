using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tween : MonoBehaviour
{
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    //float elapsed = 0f;
    public float tweenTime;

    // Start is called before the first frame update
    public void ButtonPop()
    {
        StartCoroutine(EntryCurve());
    }

    IEnumerator EntryCurve()
    {
        float timePassed = 0f;
        while(timePassed <= tweenTime)
        {
            timePassed += Time.unscaledDeltaTime;
            float percent = Mathf.Clamp01(timePassed / tweenTime);
            float curvePercent = curve.Evaluate(percent);
            transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, curvePercent);

            yield return null;
        }
    }
}
