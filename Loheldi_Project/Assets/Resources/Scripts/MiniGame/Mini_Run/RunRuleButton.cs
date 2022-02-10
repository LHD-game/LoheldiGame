using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunRuleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool playerBool = false;
    private bool actBool = false;
    public GameObject Title;
    public GameObject Rule;

    void Update()
    {
        if (playerBool)
        {
            if (actBool)
            {
                Title.SetActive(true);
                Rule.SetActive(false);

                actBool = false;
            }
            else
            {
                Title.SetActive(false);
                Rule.SetActive(true);

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