using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_STT_Handler : STT_Handler
{
    public HomeAppController homeAppController;

    public override void onStartedListening()
    {
        homeAppController.startListeningMode();
    }

    public override void onNewWordListened(string sentence)
    {
        homeAppController.setSentenceListened(sentence);
    }

    public override void onFinishedListening(bool operationStatus)
    {
        if (!operationStatus) homeAppController.stopListeningMode();
    }

    public override void onRecognizedSentence(string sentence)
    {
        bool openTodoListApp = sentence.Contains("quehaceres");
        bool openNewsApp = sentence.Contains("noticia");
        bool openMailApp = sentence.Contains("mail") || sentence.Contains("correo");

        if (openTodoListApp)
        {
            homeAppController.openAppViaVoice("TodoList");
        }
        else if (openNewsApp)
        {
            homeAppController.openAppViaVoice("News");
        }
        else if (openMailApp)
        {
            homeAppController.openAppViaVoice("Mail");
        }

        if (openTodoListApp || openNewsApp || openMailApp) recognitionSucceded();
    }

}
