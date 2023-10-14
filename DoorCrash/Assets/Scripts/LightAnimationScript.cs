using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightAnimationScript : MonoBehaviour
{
    public AnimationClip animationClip;
    private bool animationComplete = false;

    void Update()
    {
        if (animationClip != null)
        {
            // check the time of where the animation is at
            float normalizedTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normalizedTime >= 1f && !animationComplete)
            {
                animationComplete = true;
                SceneManager.LoadScene("YouLose");
            }
        }
    }
}

