using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToothDifficulty : MonoBehaviour
{
    public void ReturnEasy()
    {
        ToothGameManager.difficulty = 1;
        Debug.Log("difficulty" + 1);
    }
    public void ReturnNormal()
    {
        ToothGameManager.difficulty = 2;
        Debug.Log("difficulty" + 2);
    }
    public void ReturnHard()
    {
        ToothGameManager.difficulty = 3;
        Debug.Log("difficulty" + 3);
    }
}
