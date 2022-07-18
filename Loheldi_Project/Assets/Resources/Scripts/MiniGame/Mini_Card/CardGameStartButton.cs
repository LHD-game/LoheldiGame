using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardGameStartButton : MonoBehaviour
{

    public GameObject WelcomePanel;
    public GameObject HPDisablePanel;   //hp 부족으로 인한 팝업 패널 오브젝트


    public void GameStart()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        if (now_hp > 0)  //현재 hp가 0보다 크다면
        {
            //hp 1 감소
            PlayInfoManager.GetHP(-1);
            CardGameManager.GameStart = true;
            WelcomePanel.SetActive(false);
        }
        else    //0 이하라면: 게임 플레이 불가
        {
            // hp가 부족합니다! 팝업 띄우기
            HPDisablePanel.SetActive(true);
        }

    }
}