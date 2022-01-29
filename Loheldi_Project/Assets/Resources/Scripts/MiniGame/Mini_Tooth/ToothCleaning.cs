using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothCleaning : MonoBehaviour
{
    int TouchCount = 0;


    private void OnMouseDown()
    {
        TouchCount++;
        if (TouchCount == 3)
        {
            ToothGameManager.BlackCount--;
            TouchCount = 0;
            //Debug.Log("this:" + this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
