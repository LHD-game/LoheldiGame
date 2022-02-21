using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEndChart : MonoBehaviour
{
        
    public void OnClickGetChartAndSave()
    {
        var BRO = Backend.Chart.GetOneChartAndSave("41696");

        if (BRO.IsSuccess())
        {
            Debug.Log("�ҷ����� �Ϸ�");
            Debug.Log(BRO);

            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            string ChartName, ChartContents;

            for (int i = 0; i < rows.Count; i++)
            {
                ChartName = rows[i]["chartName"]["S"].ToString();

                // �����տ� ����� ������ �ҷ��´�.
                ChartContents = PlayerPrefs.GetString(ChartName);
                Debug.Log(string.Format("{0}\n{1}", ChartName, ChartContents));

                GetPlayerPrefs(ChartName);
            }
        }
        else
        {
            Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
        }
    }

    // Ŭ���̾�Ʈ�� ����� ������ �ҷ�����
    void GetPlayerPrefs(string chartName)
    {
        string chartString = PlayerPrefs.GetString(chartName);

        // rows[id]
        JsonData chartJson = JsonMapper.ToObject(chartString)["rows"][1];
        Debug.Log(chartJson["name"][0]);
    }

    public void OnClickGetChartContents()
    {
        BackendReturnObject BRO = Backend.Chart.GetChartContents("41696");

        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            for (int i = 0; i < rows.Count; i++)
            {
                Debug.Log("������ �̸�: " + rows[i]["name"][0]);
                Debug.Log("������ ����: " + rows[i]["price"][0]);
            }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
