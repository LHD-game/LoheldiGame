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

    //todo: �ڷΰ��� Ŭ�� ��(esc), ���� ���� �г� ����. �˾� �ϳ��� ���� �ϳ�.
    void Update()
    {
        
    }
    //todo:
    // ȭ�� ��ġ ��, �α��� ���θ� �Ǻ��Ͽ� �α������� ���� ��� welcomePanel Ȱ��ȭ
    public void WelcomePop()
    {
        if (!isLogin)
        {
            StartBtn.SetActive(false);
            WelcomePanel.SetActive(true);
        }

    }


    //welcomePanel���� �α��� ��ư Ŭ�� �� loginPanel Ȱ��ȭ �Լ�
    public void LoginPop()
    {
        LoginPanel.SetActive(true);
    }

    //�α��� ���� �Ǻ� �Լ�
    void LoginChk()
    {

    }
}
