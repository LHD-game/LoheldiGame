using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_Log : MonoBehaviour
{
    private static Save_Log _instance;
    public static Save_Log instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Save_Log>();
            }
            return _instance;
        }
    }

    static string last_play_date;
    static bool is_update_log = false;

    //�α��� ��,
    //1. �������� �����ߴ�(�ִ��� �˻絵��) �α��� �ð��� ���� ���� �ð��� ������ ������ ���ε�
    //2. �α��� �ð��� ����
    //30�� �������� ���� ���� �ð��� ����

    private void Start()
    {
        StartCoroutine(PlayTimeChk());
    }
    /*    private void Update()
        {
            if (!is_update_log) //���� 1ȸ�� �����ϱ� ����
            {
                if (SceneManager.GetActiveScene().name != "Welcome")
                {
                    StartCoroutine(PlayTimeChk());
                    is_update_log = true;
                    Debug.Log("save_log_start");
                }
            }


        }*/
    IEnumerator PlayTimeChk()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(20.0f); //30s ���

            DateTime now_time = DateTime.Now;
            last_play_date = now_time.ToString("F");    //�ڼ��� ��¥, �ð�
            PlayerPrefs.SetString("LastPlayDate", last_play_date);
            Debug.Log("���� ���� ��¥: " + last_play_date);
        }

    }

    public void UpdatePlayLog()    //PLAY_LOG ���̺� ���� ���� ���ε�
    {
        if(PlayerPrefs.HasKey("LastPlayDate")) //������ �÷��� �ð��� ���� ���� �ִٸ�
        {
            //������ ���ε�
            Param param = new Param();
            param.Add("LoginDate", PlayerPrefs.GetString("LoginDate"));
            param.Add("LastPlayDate", PlayerPrefs.GetString("LastPlayDate"));

            var bro = Backend.GameData.Insert("PLAY_LOG", param);

            if (bro.IsSuccess())
            {
                Debug.Log("UpdatePlayLog() Success");
                PlayerPrefs.DeleteKey("LastPlayDate");  //Ű�� �����������ν� 20�� �������� ���� �ߺ� ����
                
            }
            else
            {
                Debug.Log("UpdatePlayLog() Fail");
                return;
            }
        }

        DateTime now_time = DateTime.Now;
        string login_date = now_time.ToString("F");

        PlayerPrefs.SetString("LoginDate", login_date);
        Debug.Log("�α��� ��¥: "+ login_date);
    }
}
