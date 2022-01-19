using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Difficulty : MonoBehaviour
{
    public void ReturnEasy()
    {
        RunGameManager.difficulty = 1;
        Debug.Log("difficulty" + 1);
    }
    public void ReturnNormal()
    {
        RunGameManager.difficulty = 2;
        Debug.Log("difficulty" + 2);
    }
    public void ReturnHard()
    {
        RunGameManager.difficulty = 3;
        Debug.Log("difficulty" + 3);
    }
}
