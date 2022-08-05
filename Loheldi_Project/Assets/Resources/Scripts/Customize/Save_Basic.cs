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
        param.Add("Upper", "4010102");
        param.Add("UColor", "white");
        param.Add("Lower", "4020104");
        param.Add("LColor", "white");
        param.Add("Socks", "4030101");
        param.Add("SColor", "white");
        param.Add("Shoes", "4030105");
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
        param.Add("QuestPreg", "0_0");
        param.Add("WeeklyQuestPreg", "22_0");
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

    //USER_GARDEN 테이블 초기값 저장
    public static void UserGardenInit()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_GARDEN", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserGardenInit() Success");
        }
        else
        {
            Debug.Log("UserGardenInit() Fail");
        }
    }
    //USER_HOUSE 테이블 초기값 저장
    public static void UserHousingInit()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_HOUSE", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserHousingInit() Success");
        }
        else
        {
            Debug.Log("UserHousingInit() Fail");
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
                PlayerPrefs.SetString("WeeklyQuestPreg", data.WeeklyQuestPreg);

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

    //user garden 테이블에서 값 가져와 로컬에 저장
    public static void LoadUserGarden()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_GARDEN", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                GardenData data = pj.ParseBackendData<GardenData>(json_data);

                string g1_t = data.G1Time.ToString("g");
                string g2_t = data.G2Time.ToString("g");
                string g3_t = data.G3Time.ToString("g");
                string g4_t = data.G4Time.ToString("g");

                PlayerPrefs.SetString("G1", data.G1);
                PlayerPrefs.SetString("G1Time", g1_t);
                PlayerPrefs.SetString("G2", data.G2);
                PlayerPrefs.SetString("G2Time", g2_t);
                PlayerPrefs.SetString("G3", data.G3);
                PlayerPrefs.SetString("G3Time", g3_t);
                PlayerPrefs.SetString("G4", data.G4);
                PlayerPrefs.SetString("G4Time", g4_t);

            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //user house 테이블에서 값 가져와 로컬에 저장
    public static void LoadUserHousing()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_HOUSE", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                HousingData data = pj.ParseBackendData<HousingData>(json_data);

                PlayerPrefs.SetInt("HouseLv", data.HouseLv);

                PlayerPrefs.SetString("bed", data.bed);
                PlayerPrefs.SetString("closet", data.closet);
                PlayerPrefs.SetString("table", data.table);
                PlayerPrefs.SetString("chair", data.chair);
                PlayerPrefs.SetString("kitchen", data.kitchen);

            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
}
