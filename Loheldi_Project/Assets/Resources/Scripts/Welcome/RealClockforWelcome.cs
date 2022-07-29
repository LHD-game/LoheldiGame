using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class RealClockforWelcome : MonoBehaviour
{
    public Material DaySky;             //스카이박스 메터리얼 불러옴
    public Material NoonSky;
    public Material NightSky;

    public GameObject Light;            //시간에따라 변경될 빛
    public GameObject NightLight;

    private int PreTime;                //시간이 변경됬는지 확인하기 위한 변수
    public int Time;                    //시간 변수

    QuestDontDestroy QDD;
    void Start()
    {
        QDD= GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        GetCurrentDate();               //시간 불러는 함수
        TimeSetting(Time);
        PreTime = Time;                 //변경 함수 초기화

        DateTime nowDT = DateTime.Now;
        if (nowDT.DayOfWeek == DayOfWeek.Sunday || nowDT.DayOfWeek == DayOfWeek.Saturday)
            QDD.weekend = true;
        else
            QDD.weekend = false;
        QDD.ToDay= Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));   //퀘스트용 오늘날짜 저장 
    }

    private void Update()
    {
        GetCurrentDate();               //매 프레임마다 시간을 불러옴
        if (PreTime != Time)            //시간이 변경 될 때마다
        {
            PreTime = Time;             //변경 함수 초기화
            TimeSetting(Time);          //시간에 따른 변경 함수
        }
    }

    public void GetCurrentDate()
    {
        Time = int.Parse(DateTime.Now.ToString("HH"));                    //String을 Int로 변경 (HH는 24시간 개념)
    }

    public void TimeSetting(float Time)
    {
        switch (Time)
        {
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:                     //아침
                RenderSettings.skybox = DaySky;                                                           //스카이박스 변경
                Light.GetComponent<Light>().color = new Color(255f / 255f, 244f / 255f, 214f / 255f);     //빛 색 변경
                NightLight.SetActive(false);                                                              //광원은 아침에 꺼짐
                Light.transform.eulerAngles = new Vector3(50, (Time - 11) * 15, 0);                       //빛을 15도씩 돌림
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:                    //밤
                RenderSettings.skybox = NightSky;
                Light.GetComponent<Light>().color = new Color(68f / 255f, 68f / 255f, 128f / 255f);
                NightLight.SetActive(true);
                Light.transform.eulerAngles = new Vector3(50, (Time - 23) * 15, 0);
                break;
        }
        if (Time == 5 || Time == 17)    //새벽, 저녁
        {
            RenderSettings.skybox = NoonSky;
            Light.GetComponent<Light>().color = new Color(139f / 255f, 9f / 255f, 202f / 255f);
            Light.transform.rotation = Quaternion.Euler(50, -90, 0);    //빛의 위치를 초기화함 (해가 뜨면 볕을 쬐고, 달이 뜨면 달을 쬐고)
            if (Time == 17)             //광원은 저녁에 켜짐
            {
                NightLight.SetActive(true);
            }
        }
    }
}