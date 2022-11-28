using BackEnd;
using LitJson;
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

    IEnumerator PlayTimeChk()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(180.0f); //3min ���

            DateTime now_time = DateTime.Now;
            last_play_date = now_time.ToString("F");    //�ڼ��� ��¥, �ð�
            PlayerPrefs.SetString("LastPlayDate", last_play_date);
            Debug.Log("���� ���� ��¥: " + last_play_date);

            UpdatePlayLog();
        }

    }

    void UpdatePlayLog()    //PLAY_LOG ���̺� ���� ���� ���ε�
    {
        Where where = new Where();
        where.Equal("LoginDate", PlayerPrefs.GetString("LoginDate"));   //�α��� �� �����ߴ� �ʱ��࿡ ������Ʈ
        var bro = Backend.GameData.GetMyData("PLAY_LOG", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            Param param = new Param();
            param.Add("LoginDate", PlayerPrefs.GetString("LoginDate"));
            param.Add("LastPlayDate", PlayerPrefs.GetString("LastPlayDate"));

            var update_bro = Backend.GameData.UpdateV2("PLAY_LOG", rowIndate, Backend.UserInDate, param);
            if (update_bro.IsSuccess())
            {
                Debug.Log("UpdatePlayLog() Success");
            }
            else
            {
                Debug.Log("UpdatePlayLog() Fail");
            }
        } 
    }

    public void SaveLoginLog() //�α��� ��, �ʱ� �� ����
    {
        DateTime now_time = DateTime.Now;
        string login_date = now_time.ToString("F");

        PlayerPrefs.SetString("LoginDate", login_date);
        Debug.Log("�α��� ��¥: " + login_date);

        Param param = new Param();
        param.Add("LoginDate", PlayerPrefs.GetString("LoginDate"));
        param.Add("LastPlayDate", "null");

        var insert_bro = Backend.GameData.Insert("PLAY_LOG", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("SaveLoginLog() Success");
            PlayerPrefs.DeleteKey("LastPlayDate");  //Ű�� �����������ν� 20�� �������� ���� �ߺ� ����
            StartCoroutine(PlayTimeChk());
        }
        else
        {
            Debug.Log("SaveLoginLog() Fail");
            return;
        }
    }

    //����Ʈ ���۽�, QuestLog�� �ʱ��� ����
    public void SaveQStartLog()
    {
        DateTime now_time = DateTime.Now;
        string q_start_date = now_time.ToString("F");

        PlayerPrefs.SetString("QStartDate", q_start_date);
        Debug.Log("����Ʈ ���� ��¥: " + q_start_date);

        Param param = new Param();
        param.Add("QID", PlayerPrefs.GetString("NowQID"));
        param.Add("StartTime", PlayerPrefs.GetString("QStartDate"));
        param.Add("EndTime", "null");

        var insert_bro = Backend.GameData.Insert("QUEST_LOG", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("SaveQStartLog() Success");
        }
        else
        {
            Debug.Log("SaveQStartLog() Fail");
        }
        return;
    }
    // ����Ʈ ���� �� ����ð� ������Ʈ
    public void SaveQEndLog()
    {
        DateTime now_time = DateTime.Now;
        string q_end_date = now_time.ToString("F");

        Where where = new Where();
        where.Equal("StartTime", PlayerPrefs.GetString("QStartDate"));
        var bro = Backend.GameData.Get("QUEST_LOG", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            Param param = new Param();
            param.Add("QID", bro.FlattenRows()[0]["QID"].ToString());
            param.Add("StartTime", PlayerPrefs.GetString("QStartDate"));
            param.Add("EndTime", q_end_date);

            var update_bro = Backend.GameData.UpdateV2("QUEST_LOG", rowIndate, Backend.UserInDate, param);
            if (update_bro.IsSuccess())
            {
                Debug.Log("SaveQEndLog() Success");
            }
            else
            {
                Debug.Log("SaveQEndLog() Fail");
            }
        }
    }
}
