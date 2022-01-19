using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToothButtonHard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool playerBool = false;
    public bool CountStart = false;

    void Update()
    {
        if (playerBool)
        {
            ToothGameManager.difficulty = 3;
            ToothCountDown.CountStart = true;
        }
        playerBool = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerBool = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerBool = false;
    }
}
