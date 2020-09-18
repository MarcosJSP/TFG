using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingModalController : MonoBehaviour
{
    public GameObject panel;
    public Animator animator;
    bool stopLoading = false;

    public void open()
    {
        panel.SetActive(true);
        stopLoading = false;
        playAnimation("open", () => { }, checkOtherAnimationPlaying: false);
    }

    public void error(Action callBack)
    {
        stopLoading = true;
        playAnimation("error", () => { panel.SetActive(false); callBack(); }, checkOtherAnimationPlaying: false);
    }

    public void complete(Action callBack)
    {
        stopLoading = true;
        playAnimation("", () => { panel.SetActive(false); callBack(); }, checkOtherAnimationPlaying: false);
        //playAnimation("completed", () => { panel.SetActive(false); }, checkOtherAnimationPlaying: false);
    }

    protected void playAnimation(string animationName, Action callBack, bool checkOtherAnimationPlaying = true)
    {
        if (checkOtherAnimationPlaying && MyAnimationController.isAnyAnimationPlaying()) return;

        StartCoroutine(
            MyAnimationController.playAnimationThenDoSomething(
                animator,
                animationName,
                () => callBack()
            )
        );
    }
}
