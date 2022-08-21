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

    //로그인 시,
    //1. 지난번에 저장했던(있는지 검사도함) 로그인 시간과 최종 접속 시간을 가져와 서버에 업로드
    //2. 로그인 시간을 저장
    //30초 간격으로 최종 접속 시간을 저장

    IEnumerator PlayTimeChk()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(180.0f); //3min 대기

            DateTime now_time = DateTime.Now;
            last_play_date = now_time.ToString("F");    //자세한 날짜, 시간
            PlayerPrefs.SetString("LastPlayDate", last_play_date);
            Debug.Log("최종 접속 날짜: " + last_play_date);

            UpdatePlayLog();
        }

    }

    void UpdatePlayLog()    //PLAY_LOG 테이블에 로컬 값을 업로드
    {
        Where where = new Where();
        where.Equal("LoginDate", PlayerPrefs.GetString("LoginDate"));   //로그인 시 삽입했던 초기행에 업데이트
        var bro = Backend.GameData.GetMyData("PLAY_LOG", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
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

    public void SaveLoginLog() //로그인 시, 초기 행 삽입
    {
        DateTime now_time = DateTime.Now;
        string login_date = now_time.ToString("F");

        PlayerPrefs.SetString("LoginDate", login_date);
        Debug.Log("로그인 날짜: " + login_date);

        Param param = new Param();
        param.Add("LoginDate", PlayerPrefs.GetString("LoginDate"));
        param.Add("LastPlayDate", "null");

        var insert_bro = Backend.GameData.Insert("PLAY_LOG", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("SaveLoginLog() Success");
            PlayerPrefs.DeleteKey("LastPlayDate");  //키를 삭제해줌으로써 20초 안지났을 때의 중복 방지
            StartCoroutine(PlayTimeChk());
        }
        else
        {
            Debug.Log("SaveLoginLog() Fail");
            return;
        }
    }

    //퀘스트 시작시, QuestLog에 초기행 삽입
    public void SaveQStartLog()
    {
        DateTime now_time = DateTime.Now;
        string q_start_date = now_time.ToString("F");

        PlayerPrefs.SetString("QStartDate", q_start_date);
        Debug.Log("퀘스트 시작 날짜: " + q_start_date);

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
    // 퀘스트 종료 시 종료시간 업데이트
    public void SaveQEndLog()
    {
        DateTime now_time = DateTime.Now;
        string q_end_date = now_time.ToString("F");

        Where where = new Where();
        where.Equal("StartTime", PlayerPrefs.GetString("QStartDate"));
        var bro = Backend.GameData.Get("QUEST_LOG", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
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
