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
    [SerializeField]
    GameObject SignupPanel;
    [SerializeField]
    GameObject SignupSucPanel;

    private bool isLogin = false;
    private bool isLPopup = false;  // 로그인 패널 활성화 여부
    private bool isSPopup = false;  // 회원가입 패널 활성화 여부
    private bool isSSPopup = false;  // 회원가입 성공 문구 패널 활성화 여부

    void Start()
    {
        StartBtn.SetActive(true);
        WelcomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(false);
        SignupSucPanel.SetActive(false);

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
    //blocker 클릭 시 Panel 비활성화
    public void LoginPop()
    {
        isLPopup = !isLPopup;       //false -> true, true -> false
        LoginPanel.SetActive(isLPopup);
    }

    public void SignUpPopup()
    {
        isSPopup = !isSPopup;       //false -> true, true -> false
        SignupPanel.SetActive(isSPopup);
    }

    public void SignUPSucPopup()
    {
        if (Register.allOK) //모두 정규식 만족 시
        {
            SignupPanel.SetActive(false);   //회원가입 창 비활성화
            WelcomePanel.SetActive(true);   //초기화면 활성화

            isSSPopup = !isSSPopup;       //false -> true, true -> false
            SignupSucPanel.SetActive(isSSPopup);
            Register.allOK = false;
        }
        else
        {
            SignupSucPanel.SetActive(false);
        }
    }

    //로그인 여부 판별 함수
    void LoginChk()
    {

    }
}
