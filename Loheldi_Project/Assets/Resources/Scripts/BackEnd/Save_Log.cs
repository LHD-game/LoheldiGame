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

    //로그인 시,
    //1. 지난번에 저장했던(있는지 검사도함) 로그인 시간과 최종 접속 시간을 가져와 서버에 업로드
    //2. 로그인 시간을 저장
    //30초 간격으로 최종 접속 시간을 저장

    private void Start()
    {
        StartCoroutine(PlayTimeChk());
    }
    /*    private void Update()
        {
            if (!is_update_log) //씬당 1회만 수행하기 위해
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
            yield return new WaitForSecondsRealtime(20.0f); //30s 대기

            DateTime now_time = DateTime.Now;
            last_play_date = now_time.ToString("F");    //자세한 날짜, 시간
            PlayerPrefs.SetString("LastPlayDate", last_play_date);
            Debug.Log("최종 접속 날짜: " + last_play_date);
        }

    }

    public void UpdatePlayLog()    //PLAY_LOG 테이블에 로컬 값을 업로드
    {
        if(PlayerPrefs.HasKey("LastPlayDate")) //지난번 플레이 시간에 대한 값이 있다면
        {
            //서버에 업로드
            Param param = new Param();
            param.Add("LoginDate", PlayerPrefs.GetString("LoginDate"));
            param.Add("LastPlayDate", PlayerPrefs.GetString("LastPlayDate"));

            var bro = Backend.GameData.Insert("PLAY_LOG", param);

            if (bro.IsSuccess())
            {
                Debug.Log("UpdatePlayLog() Success");
                PlayerPrefs.DeleteKey("LastPlayDate");  //키를 삭제해줌으로써 20초 안지났을 때의 중복 방지
                
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
        Debug.Log("로그인 날짜: "+ login_date);
    }
}
