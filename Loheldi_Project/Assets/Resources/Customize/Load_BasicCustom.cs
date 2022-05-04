using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_BasicCustom : MonoBehaviour
{
        
    // Start is called before the first frame update
    void Start()
    {

        /*BackendReturnObject BRO = Backend.Chart.GetChartContents("46285");

        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            Param param = new Param();
            for (int i = 0; i < rows.Count; i++)
            {
                Debug.Log("ItemCode:" + rows[i]["ItemCode"][0]);
                Debug.Log("ItemName:" + rows[i]["ItemName"][0]);
                
                //param.Add("Model:" + rows[i]["Model"][0]);
            }
        }*/
    }

    public void SaveBasicCustom()
    {
        BackendReturnObject BRO = Backend.Chart.GetChartContents("45823");
        if (BRO.IsSuccess())
        {         
            Param updateParam = new Param();
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            ParsingJSON pj = new ParsingJSON();
            
            for (int i = 0; i < rows.Count; i++)
            {
                Param param = new Param();
                CustomItem data = pj.ParseBackendData<CustomItem>(rows[i]);
                param.Add("ItemName", data.ItemName);
                param.Add("ItemCode", data.ItemCode);
                Backend.GameData.Insert("ACC_CUSTOM", param);
            }
            

            /*Backend.GameData.Insert("ACC_CUSTOM", param);*/

        }
    }

}
