using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable] public class OpenAppOnPointerClick : UnityEvent<GameObject, AppController> { }

public class MyOpenAppEventTrigger : MonoBehaviour, IPointerClickHandler
{
    public GameObject GOWithAnimator;
    public AppController appController;
    public OpenAppOnPointerClick openAppEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.openAppEvent.Invoke(GOWithAnimator, appController);
    }

    private void Start()
    {

    }
}
