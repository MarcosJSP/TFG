using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class reactToMicrophone : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float minScale = 0.5f;
    public float maxScale = 1.3f;
    public float loudness = 0;
    public float expandDecreaseRatio = 1f;
    public float vibrateAmount = 0.1f;
    public bool mute = false;

    public AudioSource audioSource;
    public AudioMixerGroup mixerGroupMicrophone;

    bool micNotReady = true;
    

    public void start()
    {
        if(mute)audioSource.outputAudioMixerGroup = mixerGroupMicrophone;
        audioSource.Play();
        micNotReady = false;
    }

    public void stop()
    {
        audioSource.Stop();
    }

    void Update()
    {

        if (audioSource != null && audioSource.clip!=null)
        {
         
            float[] data = new float[256];
            float a = 0f;
            audioSource.GetOutputData(data, 0);
            foreach (float s in data)
            {
                a += Mathf.Abs(s);
            }

            float rms =(a * sensitivity / 256f);

            //Debug.Log(sensitivity);
            //Debug.Log(rms);

            Vector3 scale = transform.localScale;

            scale.x += rms - expandDecreaseRatio;
            scale.y += rms - expandDecreaseRatio;

            if (scale.x > maxScale)
            {
                float random = Random.Range(0, vibrateAmount);
                scale.x = maxScale - random;
                scale.y = maxScale - random;
            }
            else if (scale.x < minScale)
            {
                scale.x = minScale;
                scale.y = minScale;
            }


            transform.localScale = scale;

        }
    }
}


