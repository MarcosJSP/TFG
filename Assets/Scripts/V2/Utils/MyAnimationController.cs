using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyAnimationController
{
    private static bool isAnimationPlaying = false;

    public delegate void callBackFuntionNoParams();

    public static float checkAnimationClipLenght(Animator animator, string animationClipName)
    {
        RuntimeAnimatorController rac = animator.runtimeAnimatorController;
        foreach (AnimationClip animationClip in rac.animationClips)
        {
            if (animationClip.name == animationClipName)
            {
                return animationClip.length;
            }
        }
        return 0;
    }

    public static IEnumerator playAnimation(Animator animator, string stateName)
    {
        isAnimationPlaying = true;

        animator.Play(stateName);

        float animationLenght = checkAnimationClipLenght(animator, stateName);
        yield return new WaitForSeconds(animationLenght);
        isAnimationPlaying = false;
    }

    public static IEnumerator playAnimationThenDoSomething(Animator animator, string stateName, Action callBack)
    {
        isAnimationPlaying = true;

        animator.Play(stateName);

        float animationLenght = checkAnimationClipLenght(animator, stateName);
        yield return new WaitForSeconds(animationLenght);
        isAnimationPlaying = false;

        callBack();
    }

    public static bool isAnyAnimationPlaying()
    {
        return isAnimationPlaying;
    }

}
