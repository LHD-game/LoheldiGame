using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardRuleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool playerBool = false;
    private bool actBool = false;
    public GameObject Title;
    public GameObject Rule;
    public GameObject StartButton;

    void Update()
    {
        if (playerBool)
        {
            if (actBool)
            {
                Rule.SetActive(false);
                StartButton.SetActive(true);
                Title.SetActive(true);
                
                actBool = false;
            }
            else
            {
                Rule.SetActive(true);
                StartButton.SetActive(false);
                Title.SetActive(false);

                actBool = true;
            }
            playerBool = false;
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