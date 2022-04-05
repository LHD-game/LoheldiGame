using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothCleaning : MonoBehaviour
{
    int TouchCount = 0;
    private bool pause = ToothGameManager.isPause;

    private void OnMouseDown()
    {
        if (!ToothGameManager.isPause) // 종료 후 검은 이빨 상호작용X
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
}
