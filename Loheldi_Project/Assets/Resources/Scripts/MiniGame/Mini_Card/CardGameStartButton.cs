using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardGameStartButton : MonoBehaviour
{

    public GameObject WelcomePanel;


    public void GameStart()
    {
        CardGameManager.GameStart = true;
        WelcomePanel.SetActive(false);
    }

}