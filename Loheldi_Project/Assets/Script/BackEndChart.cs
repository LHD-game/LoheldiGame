using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackEndChart : MonoBehaviour
{
    [Header("Gold")]
    public Text usergold;
    public Text itemgold;

    Dictionary<string, int> super = new Dictionary<string, int>
    {
        {"potato", 3},
        {"blueberry", 3},
        {"pumpkin", 3}
    };

    
    public void OnClickGetChartAndSave()
    {
        var BRO = Backend.Chart.GetOneChartAndSave("41766");

        if (BRO.IsSuccess())
        {
            Debug.Log("불러오기 완료");
            Debug.Log(BRO);

            /*JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            string ChartName, ChartContents;

            for (int i = 0; i < rows.Count; i++)
            {
                ChartName = rows[i]["chartName"]["S"].ToString();

                // 프리팹에 저장된 정보를 불러온다.
                ChartContents = PlayerPrefs.GetString(ChartName);
                Debug.Log(string.Format("{0}\n{1}", ChartName, ChartContents));

                GetPlayerPrefs(ChartName);
            }*/
        }
        else
        {
            Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
        }
    }

    public void SaveItem()
    {
        
        Param param = new Param();
        param.Add("item", super);

        var bro = Backend.GameData.Insert("Item", param);
        if (bro.IsSuccess())
        {
            print("정보입력 성공");
        }
    }

    // 클라이언트에 저장된 정보를 불러오기
    /*void GetPlayerPrefs(string chartName)
    {
        string chartString = PlayerPrefs.GetString(chartName);

        // rows[id]
        JsonData chartJson = JsonMapper.ToObject(chartString)["rows"][1];
        Debug.Log(chartJson["name"][0]);
    }*/

    public void OnClickGetChartContents()
    {
        BackendReturnObject BRO = Backend.Chart.GetChartContents("41766");

        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            Debug.Log("아이템 이름: " + rows[2]["name"][0]);
            itemgold.text = ("가격:"+ rows[2]["price"][0]);

            /*itemgold.text = (string)rows[2]["price"][0];*/
        }
        else
        {
            switch (BRO.GetStatusCode())
            {

                case "400":
                    Debug.Log("올바르지 못한 { uuid | id } 를 입력한 경우");
                    break;

                default:
                    Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
                    break;
            }
        }
    }

    public void Shopping()
    {
        BackendReturnObject BRO = Backend.Chart.GetChartContents("41766");

        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            itemgold.text = ("가격:" + rows[2]["price"][0]);

            /*itemgold.text = (string)rows[2]["price"][0];*/
        }
        else
        {
            switch (BRO.GetStatusCode())
            {

                case "400":
                    Debug.Log("올바르지 못한 { uuid | id } 를 입력한 경우");
                    break;

                default:
                    Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
                    break;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.GameData.Get("Item", new Where(), 10);
        foreach(int Value in super.Values)
        {
            /*itemgold.text = */
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
