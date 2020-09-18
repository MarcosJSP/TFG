using IBM.Watson.SpeechToText.V1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class STT_Handler : MonoBehaviour
{
    // activationSentence: the sentence that activates makes Mono start
    // listening
    private string activationSentence = "oye mono";

    // listening: indicates that Mono is listening
    private bool listening = false;

    // listeningTime: indicates the time Mono has been listening (in 
    // seconds)
    private float listeningTime = 0f;

    // maxNoSoundListenedTime: indicates the time Mono keeps listening,
    // if the user doesn't talk  (in seconds)
    private float maxNoSoundListenedTime = 10f;

    // success: indicates how the recognition ended, true->succeeded, 
    // false->failed
    private bool success = false;

    public IBM_streaming STT;


    // Resumen:
    //      OnEnable() gets called everytime the GameObject linked to
    //      this Script gets activated, and it mainly sets the
    //      current STTHandler on the IBM_streaming class, so the
    //      next time a voice is recognized, the OnRecognize function
    //      will be called with the recognition result as a param
    private void OnEnable()
    {
        if (STT != null)
        {
            STT.setSTTHandler(this);
        }
        else
        {
            Debug.LogError("IBM STT hasn't been initialized.");
        }
    }


    // Resumen:
    //      startListeningTimer() is responsible to make Mono stop 
    //      listening once the listeningTime surpasses the 
    //      maxNoSoundListenedTime or a recognition succeeded.
    private IEnumerator startListeningTimer()
    {
        listeningTime = 0f;
        listening = true;
        success = false;
        print("Started listening...");
        onStartedListening();

        while (listeningTime < maxNoSoundListenedTime && listening && !success)
        {
            listeningTime += Time.deltaTime;
            yield return null;
        }

        listening = false;
        print("Stoped listening...");
        onFinishedListening(success);
    }


    private void resetTimer()
    {
        listeningTime = 0;
    }


    // Resumen:
    //      OnRecognize() is called once a recognition has 
    //      been done. If it detects the activationSentence
    //      startListeningTimer() will be executed.
    public void OnRecognize(SpeechRecognitionEvent _event)
    {
        if (_event != null && _event.results.Length > 0)
        {
            foreach (var res in _event.results)
            {
                var lastAlt = res.alternatives[res.alternatives.Length - 1];
                foreach (var alt in res.alternatives)
                {
                    //print("STT_Handler: sentence -> " + alt.transcript + " confidence -> " + alt.confidence);
                    if (listening)
                    {
                        if (res.final)
                        {
                            print(alt.transcript);
                            onRecognizedSentence(alt.transcript);
                            if (alt.GetHashCode() == lastAlt.GetHashCode()){
                                listening = false;
                            }
                        }
                        else
                        {
                            resetTimer();
                        }
                        onNewWordListened(alt.transcript);
                    }
                    else
                    {
                        if (res.final && alt.transcript.Contains( activationSentence ))
                        {
                            StartCoroutine(startListeningTimer());
                        }
                    }

                }
            }
        }
    }

    public virtual void onStartedListening()
    {
    }

    public virtual void onRecognizedSentence(string sentence)
    {
    }

    public virtual void onFinishedListening(bool operationStatus)
    {
    }

    public virtual void onNewWordListened(string sentence)
    {
    }

    public void recognitionSucceded()
    {
        success = true;
    }

}
