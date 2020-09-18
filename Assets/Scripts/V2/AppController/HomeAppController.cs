using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomeAppController : AppController
{
    private bool isMenuOpen = false;
    public App[] apps;
    public ListeningModeController listeningModeController;

    private void Start()
    {
        loadApp();
    }

    public override void loadApp()
    {
        isMenuOpen = false;
        appPanel.SetActive(true);
        playAnimation("start", () => { }, checkOtherAnimationPlaying: false);
    }

    public void openMainMenu()
    {
        if (isMenuOpen) return;
        playAnimation("open menu", () => { isMenuOpen = true; });
    }

    public void closeMainMenu()
    {
        if (!isMenuOpen) return;
        playAnimation("close menu", () => { isMenuOpen = false; });
    }

    public void openApp(App app)
    {
        GameObject menuAppGO = app.gameObject;
        menuAppGO.transform.SetAsLastSibling();
        AppController nextAppController = app.appController;
        playAnimation("open app from menu",
                () => {
                    this.closeApp();
                    nextAppController.loadApp();
                }
        );
    }

    

    // VIA VOICE

    public void openAppViaVoice(string appName)
    {
        foreach (App app in apps)
        {
            if (app.name == appName)
            {
                listeningModeController.close(() => { openApp(app); }, succeded: true);
                break;
            }
        }
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
