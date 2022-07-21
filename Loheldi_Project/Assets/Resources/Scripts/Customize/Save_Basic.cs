using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Basic //초기값을 서버에 저장해주는 클래스
{
    public static void SaveBasicCustom()
    {
        string BasicCSV = ChartNum.BasicCustomItemChart;

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // 새 객체 생성

                param.Add("ICode", rows[i]["ICode"][0]);    //객체에 값 추가

                Backend.GameData.Insert("ACC_CUSTOM", param);   //객체를 서버에 업로드
            }
            PlayerCustomInit();
        } 
    }

    public static void SaveBasicClothes()
    {
        string BasicCSV = ChartNum.BasicClothesItemChart;

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // 새 객체 생성
                param.Add("ICode", rows[i]["ICode"][0]);    //객체에 값 추가

                Backend.GameData.Insert("ACC_CLOSET", param);   //객체를 서버에 업로드
            }
            PlayerClothesInit();
        }
    }

    static void PlayerCustomInit()  //유저의 초기 커스터마이징 정보를 서버에 저장
    {
        Param param = new Param();
        param.Add("Skin", "3030101");
        param.Add("Eyes", "3040101");
        param.Add("EColor", "gray");
        param.Add("Mouth", "3050101");
        param.Add("Hair", "3010101");
        param.Add("HColor", "black");

        Backend.GameData.Insert("USER_CUSTOM", param);
        Debug.Log("PlayerCustomInit");
    }

    static void PlayerClothesInit()  //유저의 초기 의상 정보를 서버에 저장
    {
        Param param = new Param();
        param.Add("Upper", "4010202");
        param.Add("UColor", "white");
        param.Add("Lower", "4020103");
        param.Add("LColor", "white");
        param.Add("Socks", "4030101");
        param.Add("SColor", "white");
        param.Add("Shoes", "4030106");
        param.Add("ShColor", "white");
        param.Add("Hat", "null");
        param.Add("Glasses", "null");
        param.Add("Bag", "null");

        Backend.GameData.Insert("USER_CLOTHES", param);
        Debug.Log("PlayerClothesInit");
    }

    //계정의 초기 재화, 레벨, 경험치, 최대 경험치량, 퀘스트 진행도, 지난 퀘스트 완료 시각 저장 메소드
    public static void PlayerInfoInit()
    {
        DateTime today = DateTime.Today;
        Param param = new Param();
        
        param.Add("Wallet", 10);
        param.Add("Level", 1);
        param.Add("NowExp", 0);
        param.Add("MaxExp", 10);
        param.Add("QuestPreg", "0_1");
        param.Add("LastQTime", today.Day);
        param.Add("HP", 5);
        param.Add("LastHPTime", today.Day);

        var bro = Backend.GameData.Insert("PLAY_INFO", param);

        if (bro.IsSuccess())
        {
            Debug.Log("PlayerInfoInit() Success");
        }
        else
        {
            Debug.Log("PlayerInfoInit() Fail");
        }
    }

    //play info 테이블 가져와 로컬에 저장하는 메소드
    public static void LoadPlayInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("PLAY_INFO", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                PlayInfo data = pj.ParseBackendData<PlayInfo>(json_data);
                Debug.Log("퀘스트 진행도:" + data.QuestPreg);
                PlayerPrefs.SetInt("Wallet", data.Wallet);
                PlayerPrefs.SetInt("Level", data.Level);
                PlayerPrefs.SetFloat("NowExp", data.NowExp);
                PlayerPrefs.SetFloat("MaxExp", data.MaxExp);
                PlayerPrefs.SetString("QuestPreg", data.QuestPreg);
                PlayerPrefs.SetInt("LastQTime", data.LastQTime);
                PlayerPrefs.SetInt("HP", data.HP);
                PlayerPrefs.SetInt("LastHPTime", data.LastHPTime);

            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //acc info 테이블 가져와 로컬에 저장하는 메소드: 닉네임, 생년월일, 보호자인증번호
    public static void LoadAccInfo()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("ACC_INFO", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                AccInfo data = pj.ParseBackendData<AccInfo>(json_data);

                PlayerPrefs.SetString("Nickname", data.NICKNAME);
                PlayerPrefs.SetString("Birth", data.BIRTH.ToString("yyyy년 M월 d일"));
                PlayerPrefs.SetString("ParentsNo", data.PARENTSNO);

            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

}
