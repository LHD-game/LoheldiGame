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

    private bool isLogin = false;   //�α��� ����
    private bool isLPopup = false;  // �α��� �г� Ȱ��ȭ ����
    private bool isSPopup = false;  // ȸ������ �г� Ȱ��ȭ ����
    private bool isSSPopup = false;  // ȸ������ ���� ���� �г� Ȱ��ȭ ����
    private bool isAccPopup = false;  // ���̵�/��й�ȣ ã�� �г� Ȱ��ȭ ����
    public GameObject SoundManager;


    void Start()
    {
        BeforePanel.SetActive(true);
        StartBtn.SetActive(true);
        WelcomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(false);
        SignupSucPanel.SetActive(false);
        FindAccPanel.SetActive(false);
        SoundManager.GetComponent<SoundEffect>().Sound("BGMOpening");

    }

    //todo: �ڷΰ��� Ŭ�� ��(esc), ���� ���� �г� ����. �˾� �ϳ��� ���� �ϳ�.
    void Update()
    {
        
    }
    //todo:
    // ȭ�� ��ġ ��, �α��� ���θ� �Ǻ��Ͽ� �α������� ���� ��� welcomePanel Ȱ��ȭ
    public void WelcomePop()
    {
        Destroy(BeforePanel);   //���� ���� ������ �� �����Ѵ�.
        if (!isLogin)   //�ڵ��α���x
        {
            StartBtn.SetActive(false);
            WelcomePanel.SetActive(true);
        }
        else    //�ڵ��α���o
        {
            BackendReturnObject BRO = Backend.BMember.CustomLogin(PlayerPrefs.GetString("ID"), PlayerPrefs.GetString("PW"));
            SceneLoader.instance.GotoMainField();
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

    public void Restart()   //ȸ������ �Ϸ� �� �����
    {
        SceneManager.LoadScene("Scenes/Welcome");
    }

    //�α��� ���� �Ǻ� �Լ�
    void LoginChk()
    {
        if (PlayerPrefs.HasKey("ID"))
        {
            if (PlayerPrefs.HasKey("PW"))
            {
                isLogin = true;
            }
        }
        else
        {
            isLogin = false;
        }

    }
}
