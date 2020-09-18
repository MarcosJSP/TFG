using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ListeningModeController : MonoBehaviour
 {
    public TMP_Text listeningModeText;
    public GameObject panel;
    public Animator animator;
    public AudioSource listeningAudioSource, errorAudioSource, successAudioSource;
    public reactToMicrophone bgExpander;

    private bool inAnimation = false;

    public void open()
    {
        panel.SetActive(true);
        inAnimation = true;
        listeningModeText.text = "Escuchando ...";
        playAnimation("open", () => { inAnimation = false; bgExpander.start(); }, checkOtherAnimationPlaying: false);
        listeningAudioSource.Play();
    }

    public void close(Action callBack, bool succeded)
    {
        if (inAnimation) return;
        bgExpander.stop();

        string animationName = "";
        AudioSource audioSource = null;

        if (succeded)
        {
            animationName = "close";
            audioSource = successAudioSource;
        }
        else
        {
            animationName = "error";
            audioSource = errorAudioSource;
        }

        //play animation
        playAnimation(
                animationName,
                () => {
                    panel.SetActive(false);
                    callBack();
                },
                checkOtherAnimationPlaying: false
            );

        //play audio
        audioSource.Play();
    }

    public void setText(string sentence)
    {
        if (!inAnimation) listeningModeText.SetText(sentence);
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
