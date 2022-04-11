using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_BasicCustom : MonoBehaviour
{
    BackendReturnObject BRO = Backend.Chart.GetChartContents("46285");
    // Start is called before the first frame update
    void Start()
    {
        
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            for(int i = 0; i < rows.Count; i++)
            {
                Debug.Log("ItemCode:" + rows[i]["ItemCode"][0]);
                Debug.Log("ItemName:" + rows[i]["ItemName"][0]);
                Debug.Log("Model:" + rows[i]["Model"][0]);
            }
            
        }
    }

    public void SaveBasicCustom()
    {
        Param param = new Param();
        if(BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            for(int i = 0; i < rows.Count; i++)
            {
                param.Add("ItemCode", rows[i]["ItemCode"][0]);
                param.Add("ItemName", rows[i]["ItemName"][0]);
                param.Add("Model", rows[i]["Model"][0]);
                
            }
        }
        Backend.GameData.Insert("ACC_CUSTOM", param);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
