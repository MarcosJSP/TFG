using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoListAppController : AppController
{
    public HomeAppController homeAppController;

    public ListeningModeController listeningModeController;


    public override void loadApp()
    {
        appPanel.SetActive(true);
        playAnimation("open", () => { }, checkOtherAnimationPlaying: false);
    }

    public void nextView()
    {
        playAnimation("next",()=> { });
    }

    public void previousView()
    {
        playAnimation("back", () => { });
    }

    public void goHome()
    {
        playAnimation("close", () => {
            this.closeApp();
            homeAppController.loadApp();
        });
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
