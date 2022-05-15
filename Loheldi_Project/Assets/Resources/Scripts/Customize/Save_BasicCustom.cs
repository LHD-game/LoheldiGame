using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_BasicCustom
{
    public static void SaveBasicCustom()
    {
        string BasicCSV = "49390";

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // �� ��ü ����

                param.Add("ICode", rows[i]["ICode"][0]);    //��ü�� �� �߰�
                param.Add("IName", rows[i]["IName"][0]);
                param.Add("KorName", rows[i]["KorName"][0]);
                param.Add("Model", rows[i]["Model"][0]);
                param.Add("Material", rows[i]["Material"][0]);
                param.Add("Texture", rows[i]["Texture"][0]);

                Backend.GameData.Insert("ACC_CUSTOM", param);   //��ü�� ������ ���ε�
            }
            PlayerCustomInit();
        } 
    }

    public static void SaveBasicClothes()
    {
        string BasicCSV = "48997";

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // �� ��ü ����

                param.Add("ICode", rows[i]["ICode"][0]);    //��ü�� �� �߰�
                param.Add("IName", rows[i]["IName"][0]);
                param.Add("KorName", rows[i]["KorName"][0]);
                param.Add("Model", rows[i]["Model"][0]);
                param.Add("Material", rows[i]["Material"][0]);
                param.Add("Texture", rows[i]["Texture"][0]);

                Backend.GameData.Insert("ACC_CLOSET", param);   //��ü�� ������ ���ε�
            }
            PlayerClothesInit();
        }
    }

    static void PlayerCustomInit()  //������ �ʱ� Ŀ���͸���¡ ������ ������ ����
    {
        Param param = new Param();
        param.Add("Skin", "skin1");
        param.Add("Eyes", "eyes1");
        param.Add("EColor", "gray");
        param.Add("Mouth", "mouthI");
        param.Add("Hair", "hair1");
        param.Add("HColor", "black");

        Backend.GameData.Insert("USER_CUSTOM", param);
        Debug.Log("PlayerCustomInit");
    }

    static void PlayerClothesInit()  //������ �ʱ� �ǻ� ������ ������ ����
    {
        Param param = new Param();
        param.Add("Upper", "shPlane");
        param.Add("UColor", "white");
        param.Add("Lower", "pants_l");
        param.Add("LColor", "white");
        param.Add("Socks", "socks_plane");
        param.Add("SColor", "white");
        param.Add("Shoes", "shoes_sqare");
        param.Add("ShColor", "white");

        Backend.GameData.Insert("USER_CLOTHES", param);
        Debug.Log("PlayerClothesInit");
    }
}
