using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_BasicCustom
{
    public static void SaveBasicCustom()
    {
        string BasicCSV = "48461";

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();  // �� ��ü ����

                param.Add("ICode", rows[i]["ICode"][0]);    //��ü�� �� �߰�
                param.Add("IName", rows[i]["IName"][0]);
                param.Add("Model", rows[i]["Model"][0]);
                param.Add("Material", rows[i]["Material"][0]);
                param.Add("Texture", rows[i]["Texture"][0]);

                Backend.GameData.Insert("ACC_CUSTOM", param);   //��ü�� ������ ���ε�
            }
        } 
    }
}
