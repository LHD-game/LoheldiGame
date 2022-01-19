using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothCleaning : MonoBehaviour
{
    ToothGameManager gameManager;
    int TouchCount = 0;

    public void Awake()
    {
        gameManager = GameObject.Find("EventSystem").GetComponent<ToothGameManager>();
    }

    private void OnMouseDown()
    {
        TouchCount++;
        if (TouchCount == 3)
        {
            gameManager.BlackCount--;
            TouchCount = 0;
            this.gameObject.SetActive(false);
        }
    }
}
