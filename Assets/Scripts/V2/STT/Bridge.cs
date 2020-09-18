using IBM.Watson.SpeechToText.V1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private STT_Handler currentSTTHandler;

    public void OnRecognize(SpeechRecognitionEvent result)
    {
        if(currentSTTHandler!=null)currentSTTHandler.OnRecognize(result);
    }

    public void setSTTHandler(STT_Handler sttHandler)
    {
        currentSTTHandler = sttHandler;
    }
}
