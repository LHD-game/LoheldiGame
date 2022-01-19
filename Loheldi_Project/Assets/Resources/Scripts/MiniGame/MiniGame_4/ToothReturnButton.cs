using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToothReturnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool playerBool = false;

    void Update()
    {
        if (playerBool)
        {
            SceneManager.LoadScene("MiniGame_ToothMenu");
        }
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