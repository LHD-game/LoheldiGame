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
                Param param = new Param();  // 새 객체 생성

                param.Add("ICode", rows[i]["ICode"][0]);    //객체에 값 추가
                param.Add("IName", rows[i]["IName"][0]);
                param.Add("Model", rows[i]["Model"][0]);
                param.Add("Material", rows[i]["Material"][0]);
                param.Add("Texture", rows[i]["Texture"][0]);

                Backend.GameData.Insert("ACC_CUSTOM", param);   //객체를 서버에 업로드
            }
        } 
    }
}
