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

    private bool isLogin = false;   //�α��� ����
    private bool isLPopup = false;  // �α��� �г� Ȱ��ȭ ����
    private bool isSPopup = false;  // ȸ������ �г� Ȱ��ȭ ����
    private bool isSSPopup = false;  // ȸ������ ���� ���� �г� Ȱ��ȭ ����
    private bool isAccPopup = false;  // ���̵�/��й�ȣ ã�� �г� Ȱ��ȭ ����
    private static bool isIDSPopup = false;  // ���̵� ã�� ���� �г� Ȱ��ȭ ����
    private static bool isPWSPopup = false;  // ��й�ȣ �ʱ�ȭ ���� �г� Ȱ��ȭ ����


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

    //todo: �ڷΰ��� Ŭ�� ��(esc), ���� ���� �г� ����. �˾� �ϳ��� ���� �ϳ�.

    //todo:
    // ȭ�� ��ġ ��, �α��� ���θ� �Ǻ��Ͽ� �α������� ���� ��� welcomePanel Ȱ��ȭ
    public void WelcomePop()
    {
        Destroy(BeforePanel);   //���� ���� ������ �� �����Ѵ�.
        if (PlayerPrefs.HasKey("ID"))   //�ڵ��α���o
        {
            Debug.Log("�ڵ��α��� ����");
            Debug.Log(PlayerPrefs.GetString("ID"));
            Debug.Log(PlayerPrefs.GetString("PW"));

            BackendReturnObject BRO = Backend.BMember.CustomLogin(PlayerPrefs.GetString("ID"), PlayerPrefs.GetString("PW"));
            if (BRO.IsSuccess())
            {
                // play_info�� �������� �ҷ��� ���ÿ� ����
                Save_Basic.LoadPlayInfo();
                Save_Log.instance.SaveLoginLog();
                Save_Basic.LoadUserGarden();

                if (Register.AccChk())   //���� ���� ������� ������, ���� ������ ���ÿ� �����ϰ�, �ʵ��
                {
                    Save_Basic.LoadAccInfo();  //���� ������ ���ÿ� ����
                    SceneLoader.instance.GotoMainField();
                }
                else    //������ ���� ���� ����
                {
                    SceneLoader.instance.GotoCreateAcc();
                }
            }
            else
            {
                Debug.Log(BRO.GetMessage());
                StartBtn.SetActive(false);
                WelcomePanel.SetActive(true);
            }
            
        }
        else    //�ڵ��α���x
        {
            StartBtn.SetActive(false);
            WelcomePanel.SetActive(true);
        }

    }


    //welcomePanel���� �α��� ��ư Ŭ�� �� loginPanel Ȱ��ȭ �Լ�
    //blocker Ŭ�� �� Panel ��Ȱ��ȭ
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
        if (Register.allOK && Register.idDupChk) //��� ���Խ� ���� ��
        {
            SignupPanel.SetActive(false);   //ȸ������ â ��Ȱ��ȭ
            WelcomePanel.SetActive(true);   //�ʱ�ȭ�� Ȱ��ȭ

            isSSPopup = !isSSPopup;       //false -> true, true -> false
            SignupSucPanel.SetActive(isSSPopup);
            Register.allOK = false;
        }
    }

    //���̵�/��й�ȣ ã�� �г� popup ����
    public void FindAccPopup()
    {
        isAccPopup = !isAccPopup;
        FindAccPanel.SetActive(isAccPopup);
        FindID.SetActive(true);
        FindPW.SetActive(false);
    }

    //���̵� �ʱ�ȭ ȭ��
    public void FindIDPop()
    {
        FindID.SetActive(true);
        FindPW.SetActive(false);
    }
    //��й�ȣ �ʱ�ȭ ȭ��
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

    public void Restart()   //ȸ������ �Ϸ� �� �����
    {
        SceneManager.LoadScene("Scenes/Welcome");
    }

    public void GoGoogleStoreBtn()
    {
        //�ش� ���� ������ ���� ���ִ� ����Ƽ �Լ�
        #if UNITY_ANDROID
                Application.OpenURL("market://details?id=" + Application.identifier);
        #elif UNITY_IOS
                Application.OpenURL("https://itunes.apple.com/kr/app/apple-store/" + "id1461432877");
        #endif
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
