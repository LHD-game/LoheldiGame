using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

public class WelcomeManager : MonoBehaviour
{
    [SerializeField]
    GameObject BeforePanel;
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
    [SerializeField]
    GameObject FindAccPanel;
    [SerializeField]
    GameObject FindID;
    [SerializeField]
    GameObject FindPW;
    public GameObject FindSuccIDPopup;
    public GameObject InitSuccPWPopup;

    private bool isLogin = false;   //로그인 여부
    private bool isLPopup = false;  // 로그인 패널 활성화 여부
    private bool isSPopup = false;  // 회원가입 패널 활성화 여부
    private bool isSSPopup = false;  // 회원가입 성공 문구 패널 활성화 여부
    private bool isAccPopup = false;  // 아이디/비밀번호 찾기 패널 활성화 여부
    private static bool isIDSPopup = false;  // 아이디 찾기 성공 패널 활성화 여부
    private static bool isPWSPopup = false;  // 비밀번호 초기화 성공 패널 활성화 여부


    void Start()
    {
        BeforePanel.SetActive(true);
        StartBtn.SetActive(true);
        WelcomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(false);
        SignupSucPanel.SetActive(false);
        FindAccPanel.SetActive(false);

    }

    //todo: 뒤로가기 클릭 시(esc), 종료 여부 패널 띄우기. 팝업 하나당 스택 하나.

    //todo:
    // 화면 터치 시, 로그인 여부를 판별하여 로그인하지 않은 경우 welcomePanel 활성화
    public void WelcomePop()
    {
        Destroy(BeforePanel);   //이제 쓸모 없어진 건 삭제한다.
        if (PlayerPrefs.HasKey("ID"))   //자동로그인o
        {
            Debug.Log("자동로그인 실행");
            Debug.Log(PlayerPrefs.GetString("ID"));
            Debug.Log(PlayerPrefs.GetString("PW"));

            BackendReturnObject BRO = Backend.BMember.CustomLogin(PlayerPrefs.GetString("ID"), PlayerPrefs.GetString("PW"));
            if (BRO.IsSuccess())
            {
                SceneLoader.instance.GotoMainField();
            }
            else
            {
                Debug.Log(BRO.GetMessage());
                StartBtn.SetActive(false);
                WelcomePanel.SetActive(true);
            }
            
        }
        else    //자동로그인x
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
        if (Register.allOK && Register.idDupChk) //모두 정규식 만족 시
        {
            SignupPanel.SetActive(false);   //회원가입 창 비활성화
            WelcomePanel.SetActive(true);   //초기화면 활성화

            isSSPopup = !isSSPopup;       //false -> true, true -> false
            SignupSucPanel.SetActive(isSSPopup);
            Register.allOK = false;
        }
    }

    //아이디/비밀번호 찾기 패널 popup 관리
    public void FindAccPopup()
    {
        isAccPopup = !isAccPopup;
        FindAccPanel.SetActive(isAccPopup);
        FindID.SetActive(true);
        FindPW.SetActive(false);
    }

    //아이디 초기화 화면
    public void FindIDPop()
    {
        FindID.SetActive(true);
        FindPW.SetActive(false);
    }
    //비밀번호 초기화 화면
    public void FindPWPop()
    {
        FindID.SetActive(false);
        FindPW.SetActive(true);
    }

    public void FindIDSucPop()
    {
        if (Register.isIDFind)
        {
            isIDSPopup = !isIDSPopup;
            FindSuccIDPopup.SetActive(isIDSPopup);
            if (!isIDSPopup)
            {
                FindAccPopup();
            }
        }
    }

    public void InitPWSucPop()
    {
        if (Register.isPWFind)
        {
            isPWSPopup = !isPWSPopup;
            InitSuccPWPopup.SetActive(isPWSPopup);
            if (!isPWSPopup)
            {
                FindAccPopup();
            }
        }

    }

    public void Restart()   //회원가입 완료 후 재시작
    {
        SceneManager.LoadScene("Scenes/Welcome");
    }
}
