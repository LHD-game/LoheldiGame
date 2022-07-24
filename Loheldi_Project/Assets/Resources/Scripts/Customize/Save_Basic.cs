using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Basic //�ʱⰪ�� ������ �������ִ� Ŭ����
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
                Param param = new Param();  // �� ��ü ����

                param.Add("ICode", rows[i]["ICode"][0]);    //��ü�� �� �߰�

                Backend.GameData.Insert("ACC_CUSTOM", param);   //��ü�� ������ ���ε�
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
                Param param = new Param();  // �� ��ü ����
                param.Add("ICode", rows[i]["ICode"][0]);    //��ü�� �� �߰�

                Backend.GameData.Insert("ACC_CLOSET", param);   //��ü�� ������ ���ε�
            }
            PlayerClothesInit();
        }
    }

    static void PlayerCustomInit()  //������ �ʱ� Ŀ���͸���¡ ������ ������ ����
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

    static void PlayerClothesInit()  //������ �ʱ� �ǻ� ������ ������ ����
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

    //������ �ʱ� ��ȭ, ����, ����ġ, �ִ� ����ġ��, ����Ʈ ���൵, ���� ����Ʈ �Ϸ� �ð� ���� �޼ҵ�
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

    public static void QuestInfoInit()
    {
        var bro = Backend.GameData.GetMyData("QUEST_INFO", new Where());
        Param param = new Param();
        if (bro.IsSuccess())
        {
            if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
            {
                string selectedProbabilityFileId = "55216";

                var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
                JsonData rows = bro3.GetReturnValuetoJSON()["rows"];
                string QID = rows[0]["QID"]["S"].ToString();
                string QName = rows[0]["QName"]["S"].ToString();
                string From = rows[0]["From"]["S"].ToString();
                string Content = rows[0]["Content"]["S"].ToString();
                string Reward = rows[0]["Reward"]["S"].ToString();
                string authorName = rows[0]["authorName"]["S"].ToString();

                Debug.Log(QID);
                Debug.Log(QName);
                Debug.Log(Content);

                param.Add("QID", QID);
                param.Add("QName", QName);
                param.Add("From", From);
                param.Add("Content", Content);
                param.Add("Reward", Reward);
                param.Add("authorName", authorName);
                Backend.GameData.Insert("QUEST_INFO", param);
            }
        }
    }

    //USER_GARDEN ���̺� �ʱⰪ ����
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



    //play info ���̺� ������ ���ÿ� �����ϴ� �޼ҵ�
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
                Debug.Log("����Ʈ ���൵:" + data.QuestPreg);
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
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //acc info ���̺� ������ ���ÿ� �����ϴ� �޼ҵ�: �г���, �������, ��ȣ��������ȣ
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
                PlayerPrefs.SetString("Birth", data.BIRTH.ToString("yyyy�� M�� d��"));
                PlayerPrefs.SetString("ParentsNo", data.PARENTSNO);
            }
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //user garden ���̺��� �� ������ ���ÿ� ����
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

                PlayerPrefs.SetString("G1", data.G1);
                PlayerPrefs.SetString("G1Time", data.G1Time.ToString("g"));
                PlayerPrefs.SetString("G2", data.G2);
                PlayerPrefs.SetString("G2Time", data.G2Time.ToString("g"));
                PlayerPrefs.SetString("G3", data.G3);
                PlayerPrefs.SetString("G3Time", data.G3Time.ToString("g"));
                PlayerPrefs.SetString("G4", data.G4);
                PlayerPrefs.SetString("G4Time", data.G4Time.ToString("g"));

            }
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

}
