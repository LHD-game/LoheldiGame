using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

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
    private bool isLPopup = false;  // �α��� �г� Ȱ��ȭ ����
    private bool isSPopup = false;  // ȸ������ �г� Ȱ��ȭ ����
    private bool isSSPopup = false;  // ȸ������ ���� ���� �г� Ȱ��ȭ ����

    void Start()
    {
        StartBtn.SetActive(true);
        WelcomePanel.SetActive(false);
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(false);
        SignupSucPanel.SetActive(false);

    }

    //todo: �ڷΰ��� Ŭ�� ��(esc), ���� ���� �г� ����. �˾� �ϳ��� ���� �ϳ�.
    void Update()
    {
        
    }
    //todo:
    // ȭ�� ��ġ ��, �α��� ���θ� �Ǻ��Ͽ� �α������� ���� ��� welcomePanel Ȱ��ȭ
    public void WelcomePop()
    {
        if (!isLogin)   //�ڵ��α���x
        {
            StartBtn.SetActive(false);
            WelcomePanel.SetActive(true);
        }
        else    //�ڵ��α���o
        {
            BackendReturnObject BRO = Backend.BMember.CustomLogin(PlayerPrefs.GetString("ID"), PlayerPrefs.GetString("PW"));
            SceneLoader.instance.GotoGameMove();
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
