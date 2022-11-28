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

    //������ �ʱ� ��ȭ, ����, ����ġ, �ִ� ����ġ��, ����Ʈ ���൵, ���� ����Ʈ �Ϸ� �ð� ���� �޼ҵ�
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
        param.Add("HouseLv", 1);
        param.Add("HouseShape", "Plane");

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
    //USER_HOUSE ���̺� �ʱⰪ ����
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
    public static void UserHousingInit2()
    {
        Param param = new Param();
        var bro = Backend.GameData.Insert("USER_HOUSE2F", param);
        if (bro.IsSuccess())
        {
            Debug.Log("UserHousingInit() Success");
        }
        else
        {
            Debug.Log("UserHousingInit() Fail");
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
                PlayerPrefs.SetInt("HouseLv", data.HouseLv);
                PlayerPrefs.SetString("HouseShape", data.HouseShape);
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
                PlayerPrefs.SetString("Tree", data.Tree);

            }
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }

    //user house ���̺��� �� ������ ���ÿ� ����
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

                PlayerPrefs.SetString("bed", data.bed);
                PlayerPrefs.SetString("closet", data.closet);
                PlayerPrefs.SetString("table", data.table);
                PlayerPrefs.SetString("chair", data.chair);
                PlayerPrefs.SetString("chair2", data.chair2);
                PlayerPrefs.SetString("side table", data.sidetable);
                PlayerPrefs.SetString("kitchen", data.kitchen);
                PlayerPrefs.SetString("fridge", data.fridge);
                PlayerPrefs.SetString("standsink", data.standsink);
                PlayerPrefs.SetString("wallshelf", data.wallshelf);
            }
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
    public static void LoadUserHousing2()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData("USER_HOUSE2F", new Where(), 10);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();

            try
            {
                var json_data = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                HousingData data = pj.ParseBackendData<HousingData>(json_data);

                PlayerPrefs.SetString("bookshelf", data.bookshelf);
                PlayerPrefs.SetString("desk", data.desk);
                PlayerPrefs.SetString("coffee table", data.coffeetable);
                PlayerPrefs.SetString("chair3", data.chair3);
                PlayerPrefs.SetString("sunbed", data.sunbed);
                PlayerPrefs.SetString("sunbed2", data.sunbed2);
                PlayerPrefs.SetString("sofa", data.sofa);
            }
            catch (Exception ex) //��ȸ���� ����������, �ش� ���� ����(NullPointException)
            {
                Debug.Log(ex);
            }
        }
    }
}
