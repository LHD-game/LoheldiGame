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

        Backend.GameData.Insert("USER_CLOTHES", param);
        Debug.Log("PlayerClothesInit");
    }

    //������ �ʱ� ��ȭ, ����, ����ġ, �ִ� ����ġ��, ����Ʈ ���൵, ���� ����Ʈ �Ϸ� �ð� ���� �޼ҵ�
    public static void PlayerInfoInit()
    {
        Param param = new Param();
        
        param.Add("Wallet", 10);
        param.Add("Level", 1);
        param.Add("NowExp", 0);
        param.Add("MaxExp", 100);
        param.Add("QuestPreg", "0");
        param.Add("LastQTime", DateTime.Today);

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

}