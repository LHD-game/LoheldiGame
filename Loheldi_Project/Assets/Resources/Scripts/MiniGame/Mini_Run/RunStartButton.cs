using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RunStartButton : MonoBehaviour
{
    public GameObject WelcomePanel;     //시작화면 패널 오브젝트
    public GameObject DifficultyPanel;  // 난이도 선택 화면 패널 오브젝트

    public void GameStart()
    {
        //RunGameManager.GameStart = true;
        WelcomePanel.SetActive(false);
        DifficultyPanel.SetActive(true);

    }



}