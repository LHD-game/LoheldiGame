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

        BackendReturnObject BRO = Backend.Chart.GetChartContents("45823");
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                Debug.Log("CID:" + rows[i]["CID"][0]);
                Debug.Log("Name:" + rows[i]["Name"][0]);
                Debug.Log("Model:" + rows[i]["Model"][0]);
                Debug.Log("Meterial:" + rows[i]["Meterial"][0]);
                Debug.Log("Texture:" + rows[i]["Texture"][0]);

            }
        }
    }
    public class Item
    {
        string CID;
        string Name;
        string Model;
        string Meterial;
        string Texture;
        public Item(JsonData json)
        {
            BackendReturnObject BRO = Backend.Chart.GetChartContents("45823");
            if (BRO.IsSuccess())
            {
                JsonData rows = BRO.GetReturnValuetoJSON()["rows"];

                for (int i = 0; i < rows.Count; i++)
                {
                    CID = json["CID"][0].ToString();
                    Name = json["Name"][0].ToString();
                    Model = json["Model"][0].ToString();
                    Meterial = json["Material"][0].ToString();
                    Texture = json["Texture"][0].ToString();
                }

            }
            /* public Item(string cid, string name, string model, string material, string texture)
             {
                 CID = cid;
                 Name = name;
                 Model = model;
                 Meterial = material;
                 Texture = texture;
             }*/
        }
        /* public Item(string cid, string name, string model, string material, string texture)
         {
             CID = cid;
             Name = name;
             Model = model;
             Meterial = material;
             Texture = texture;
         }*/
        public void SaveBasicCustom()
        {

            Param param = new Param();
            BackendReturnObject BRO = Backend.Chart.GetChartContents("45823");
            if (BRO.IsSuccess())
            {

                JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
                ParsingJSON pj = new ParsingJSON();
                List<Item> list = new List<Item>();
                for (int i = 0; i < rows.Count; i++)
                {

                    CustomItem data = pj.ParseBackendData<CustomItem>(rows[i]);

                    list.Add(new Item(rows[i]));
                    /*list.Add("Name", rows[i]["Name"][0]);
                    list.Add("Model", rows[i]["Model"][0]);
                    list.Add("Meterial", rows[i]["Meterial"][0]);
                    list.Add("Texture", rows[i]["Texture"][0]);*/

                }
                param.Add("ItemList", list);
                Backend.GameData.Insert("ACC_CUSTOM", param);
                /*Backend.GameData.Insert("ACC_CUSTOM", param);*/

            }
        }

    }
}
