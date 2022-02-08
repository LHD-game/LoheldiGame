using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    [SerializeField]
    GameObject StartBtn;
    [SerializeField]
    GameObject WelcomePanel;
    [SerializeField]
    GameObject LoginPanel;

    private bool isLogin = false;

    void Start()
    {
        StartBtn.SetActive(true);
        WelcomePanel.SetActive(false);
        LoginPanel.SetActive(false);
    }

    //todo: 뒤로가기 클릭 시(esc), 종료 여부 패널 띄우기. 팝업 하나당 스택 하나.
    void Update()
    {
        
    }
    //todo:
    // 화면 터치 시, 로그인 여부를 판별하여 로그인하지 않은 경우 welcomePanel 활성화
    public void WelcomePop()
    {
        if (!isLogin)
        {
            StartBtn.SetActive(false);
            WelcomePanel.SetActive(true);
        }

    }


    //welcomePanel에서 로그인 버튼 클릭 시 loginPanel 활성화 함수
    public void LoginPop()
    {
        LoginPanel.SetActive(true);
    }

    //로그인 여부 판별 함수
    void LoginChk()
    {

    }
}
