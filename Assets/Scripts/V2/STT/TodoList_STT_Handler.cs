using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoList_STT_Handler : STT_Handler
{

    public TodoListAppController todoListAppController;

    public override void onStartedListening()
    {
        todoListAppController.startListeningMode();
    }

    public override void onNewWordListened(string sentence)
    {
        todoListAppController.setSentenceListened(sentence);
    }

    public override void onFinishedListening(bool operationStatus)
    {
        if (!operationStatus) todoListAppController.stopListeningMode();
    }

    public override void onRecognizedSentence(string sentence)
    {
        bool goToDeberes = sentence.Contains("deberes");
        bool goBack = sentence.Contains("vuelve") || sentence.Contains("volver") || sentence.Contains("atrás");
        bool close = sentence.Contains("cierra") || sentence.Contains("menú");

        if (goToDeberes)
        {
            todoListAppController.nextViewViaVoice();
        }
        else if (goBack)
        {
            todoListAppController.previousViewViaVoice();
        }
        else if (close)
        {
            todoListAppController.closeViaVoice();
        }

        if (goToDeberes || goBack || close) recognitionSucceded();
    }

}

