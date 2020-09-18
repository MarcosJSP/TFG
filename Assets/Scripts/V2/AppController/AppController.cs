using UnityEngine;
using System;

[System.Serializable]
public class AppController : MonoBehaviour
{
    // These variables need to be setted in the inspector
    public GameObject appPanel;
    public Animator animator;


    // Resumen:
    //      closeApp() is called when another view has been loaded and
    //      the view controlled by this AppController is no longer seen
    //      by the user. Therefore, this method deactivates the appPanel.
    public virtual void closeApp()
    {
        appPanel.SetActive(false);
    }


    // Resumen:
    //      loadApp() is called when another the view controlled by this
    //      AppController is going to be loaded. Therefore, this method
    //      activates the appPanel.
    public virtual void loadApp()
    {
        appPanel.SetActive(true);
    }


    // Resumen:
    //      setLastApp(AppController lastApp) saves the AppController
    //      reference that leaded to the current view.
    public virtual void setLastApp(AppController lastApp)
    {
        throw new System.NotImplementedException();
    }

    // Resumen:
    //      playAnimation(string animationName, Action callBack, bool
    //      checkOtherAnimationPlaying = true) plays the animation 
    //      indicated by animationName on the animator, then the 
    //      callBack function is executed. The 
    //      checkOtherAnimationPlaying parameter prevents the execution
    //      of a new animation if there is any currently playing.
    protected void playAnimation(string animationName, Action callBack, bool checkOtherAnimationPlaying = true)
    {
        if(checkOtherAnimationPlaying && MyAnimationController.isAnyAnimationPlaying()) return;

        StartCoroutine(
            MyAnimationController.playAnimationThenDoSomething(
                animator,
                animationName,
                () => callBack()
            )
        );
    }
}
