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
            Debug.Log("�ҷ����� �Ϸ�");
            Debug.Log(BRO);

            /*JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            string ChartName, ChartContents;

            for (int i = 0; i < rows.Count; i++)
            {
                ChartName = rows[i]["chartName"]["S"].ToString();

                // �����տ� ����� ������ �ҷ��´�.
                ChartContents = PlayerPrefs.GetString(ChartName);
                Debug.Log(string.Format("{0}\n{1}", ChartName, ChartContents));

                GetPlayerPrefs(ChartName);
            }*/
        }
        else
        {
            Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
        }
    }

    public void SaveItem()
    {
        
        Param param = new Param();
        param.Add("item", super);

        var bro = Backend.GameData.Insert("Item", param);
        if (bro.IsSuccess())
        {
            print("�����Է� ����");
        }
    }

    // Ŭ���̾�Ʈ�� ����� ������ �ҷ�����
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
            Debug.Log("������ �̸�: " + rows[2]["name"][0]);
            itemgold.text = ("����:"+ rows[2]["price"][0]);

            /*itemgold.text = (string)rows[2]["price"][0];*/
        }
        else
        {
            switch (BRO.GetStatusCode())
            {

                case "400":
                    Debug.Log("�ùٸ��� ���� { uuid | id } �� �Է��� ���");
                    break;

                default:
                    Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
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
            itemgold.text = ("����:" + rows[2]["price"][0]);

            /*itemgold.text = (string)rows[2]["price"][0];*/
        }
        else
        {
            switch (BRO.GetStatusCode())
            {

                case "400":
                    Debug.Log("�ùٸ��� ���� { uuid | id } �� �Է��� ���");
                    break;

                default:
                    Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
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
