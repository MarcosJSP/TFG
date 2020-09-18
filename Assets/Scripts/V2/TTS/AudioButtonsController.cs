using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AudioButtonsController : MonoBehaviour
{

    public TMP_Text currentAudioTime;
    public TMP_Text audioLength;
    public GameObject progressBar;
    public float progressBarMaxWidth, progressBarMinWidth;
    public float progressBarMinX, progressBarMaxX;
    private float progressBarLimitedWidth, progressBarLimitedX;

    // Start is called before the first frame update
    void Start()
    {
        progressBarLimitedWidth = progressBarMaxWidth - progressBarMinWidth;
        progressBarLimitedX = progressBarMaxX - progressBarMinX;
        defaultTime();
    }

    private void Awake()
    {
        defaultTime();
    }

    public void defaultTime()
    {
        //text that showes current audio instant & audio duration
        currentAudioTime.text = "00:00";
        audioLength.text = "00:00";

        //progress bar progress percentage
        RectTransform rt = progressBar.transform as RectTransform;
        rt.localPosition = new Vector3(progressBarMinX, rt.localPosition.y, rt.localPosition.z);
        rt.sizeDelta = new Vector2(progressBarMinWidth, rt.sizeDelta.y);

        (progressBar.transform as RectTransform).localPosition = rt.localPosition;
        (progressBar.transform as RectTransform).sizeDelta = rt.sizeDelta;
    }

    public void setAudioLenght(float lenght)
    {
        currentAudioTime.text = ""+lenght;
    }

    public void setCurrentTime(float time)
    {
        currentAudioTime.text = ""+time;
    }

    public void setTime(float currentTime, float length)
    {
        //text that showes current audio instant & audio duration
        currentAudioTime.text = TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss");
        audioLength.text = TimeSpan.FromSeconds(length).ToString(@"mm\:ss");

        //progress bar progress percentage
        float progressPercentage = currentTime / length;
        RectTransform rt = progressBar.transform as RectTransform;
        rt.localPosition = new Vector3(progressBarMinX + progressBarLimitedX * progressPercentage, rt.localPosition.y, rt.localPosition.z);
        rt.sizeDelta = new Vector2(progressBarMinWidth + progressBarLimitedWidth * progressPercentage, rt.sizeDelta.y);

        (progressBar.transform as RectTransform).localPosition = rt.localPosition;
        (progressBar.transform as RectTransform).sizeDelta = rt.sizeDelta;
    }

}
