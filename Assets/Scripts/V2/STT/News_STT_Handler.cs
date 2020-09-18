using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class News_STT_Handler : STT_Handler
{
    public NewsAppController newsAppController;

    public override void onStartedListening()
    {
        newsAppController.startListeningMode();
    }

    public override void onNewWordListened(string sentence)
    {
        newsAppController.setSentenceListened(sentence);
    }

    public override void onFinishedListening(bool operationStatus)
    {
        if(!operationStatus) newsAppController.stopListeningMode();
    }

    public override void onRecognizedSentence(string sentence)
    {
        bool goNext =  sentence.Contains("reproduc") || sentence.Contains("dime las noticias");
        bool goBack = sentence.Contains("vuelve") || sentence.Contains("atrás");
        bool close = sentence.Contains("cierra") || sentence.Contains("menú");

        if (goNext)
        {
            newsAppController.nextViewViaVoice();
        }
        else if (goBack)
        {
            newsAppController.previousViewViaVoice();
        }else if (close)
        {
            newsAppController.closeViaVoice();
        }

        if (goNext || goBack || close) recognitionSucceded();
    }
}