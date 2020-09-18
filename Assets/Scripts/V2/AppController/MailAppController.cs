using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailAppController : AppController
{

    public HomeAppController homeAppController;
    public TTS_Handler tts_handler;
    public TMP_Text TMP;
    public LoadingModalController loadingModalController;
    public ListeningModeController listeningModeController;
    public AudioButtonsController audioBtnsController;

    public override void loadApp()
    {
        appPanel.SetActive(true);
        playAnimation("open", () => { }, checkOtherAnimationPlaying: false);
    }

    public void nextView()
    {
        tts_handler.loadText(TMP.text);
        playAnimation("reproduce", () => StartCoroutine(waitForAudio()), checkOtherAnimationPlaying: false);
        audioBtnsController.defaultTime();
        ReproducePauseContinueBtns("init");
    }

    public void previousView()
    {
        stopAudio();
        if (loadingModalController.isActiveAndEnabled)
        {
            loadingModalController.complete(() => { });
            playAnimation("back", () => { }, checkOtherAnimationPlaying: false);
        }
        else
        {
            playAnimation("back", () => { });
        }
    }

    public void goHome()
    {
        playAnimation("close", () => {
            stopAudio();
            this.closeApp();
            homeAppController.loadApp();
        });
    }

    private IEnumerator waitForAudio()
    {
        loadingModalController.open();
        //yield return new WaitForSeconds(30);
        yield return new WaitUntil(() => tts_handler.isAudioReady());
        bool gotAudioWithoutProblems = playClip(tts_handler.getAudio());

        if (gotAudioWithoutProblems)
        {
            loadingModalController.complete(() => { ReproducePauseContinueBtns("play"); });
        }
        else
        {
            loadingModalController.error(() => { });
        }
    }

    private void ReproducePauseContinueBtns(string stateName)
    {
        if (stateName == "init")
        {
            StartCoroutine(
                MyAnimationController.playAnimation(
                    audioPauseContinueAnimator,
                    "donotletcontinue"
                )
            );
        }
        else if (stateName == "play")
        {
            StartCoroutine(
                MyAnimationController.playAnimation(
                    audioPauseContinueAnimator,
                    "play"
                )
            );
        }

    }

    GameObject audioObject = null;
    private AudioSource currentAudio = null;

    private bool playClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null && clip.length > 0f)
        {
            if (audioObject != null) DestroyObject(audioObject);
            audioObject = new GameObject("AudioObject");
            currentAudio = audioObject.AddComponent<AudioSource>();
            currentAudio.spatialBlend = 0.0f;
            currentAudio.loop = false;
            currentAudio.volume = 0.5f;
            currentAudio.clip = clip;
            currentAudio.Play();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopAudio()
    {
        if (Application.isPlaying && currentAudio != null)
        {
            currentAudio.Stop();
            currentAudio = null;
        }
    }

    public Animator audioPauseContinueAnimator;

    public void pauseClip(bool status)
    {
        if (Application.isPlaying && currentAudio != null)
        {
            if (status)
            {
                StartCoroutine(
                   MyAnimationController.playAnimation(
                       audioPauseContinueAnimator,
                       "pause"
                   )
               );
                currentAudio.Pause();
            }
            else
            {
                StartCoroutine(
                   MyAnimationController.playAnimation(
                       audioPauseContinueAnimator,
                       "play"
                   )
               );
                currentAudio.UnPause();
            }
        }
    }

    private void Update()
    {
        if (currentAudio != null)
        {
            //Debug.Log(currentAudio.isPlaying);
            audioBtnsController.setTime(currentAudio.time, currentAudio.clip.length);
        }
    }

    //Via voice

    public void nextViewViaVoice()
    {
        listeningModeController.close(nextView, succeded: true);
    }

    public void previousViewViaVoice()
    {
        listeningModeController.close(previousView, succeded: true);
    }

    public void closeViaVoice()
    {
        listeningModeController.close(goHome, succeded: true);
    }

    public void startListeningMode()
    {
        listeningModeController.open();
    }

    public void stopListeningMode()
    {
        listeningModeController.close(() => { }, succeded: false);
    }

    public void setSentenceListened(string sentence)
    {
        listeningModeController.setText(sentence);
    }

}
