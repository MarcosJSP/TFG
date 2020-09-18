using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail_STT_Handler : STT_Handler
{

    public MailAppController mailAppController;

    public override void onStartedListening()
    {
        mailAppController.startListeningMode();
    }

    public override void onNewWordListened(string sentence)
    {
        mailAppController.setSentenceListened(sentence);
    }

    public override void onFinishedListening(bool operationStatus)
    {
        if (!operationStatus) mailAppController.stopListeningMode();
    }

    public override void onRecognizedSentence(string sentence)
    {
        bool goNext = sentence.Contains("reprod") || sentence.Contains("lee");
        bool goBack = sentence.Contains("para") || sentence.Contains("vuelve") || sentence.Contains("atrás");
        bool close = sentence.Contains("cierra") || sentence.Contains("menú");

        if (goNext)
        {
            mailAppController.nextViewViaVoice();
        }
        else if (goBack)
        {
            mailAppController.previousViewViaVoice();
        }
        else if (close)
        {
            mailAppController.closeViaVoice();
        }

        if (goNext || goBack || close) recognitionSucceded();
    }

}
