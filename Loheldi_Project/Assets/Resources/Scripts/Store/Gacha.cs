using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    public GameObject TextObject;
    public GameObject ResultTxt;

    List<Dictionary<string, object>> gachaItem = new List<Dictionary<string, object>>();

    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("ItemType", data.ItemType);
    }

    private void Start()
    {
        GetChartContents("53206");
    }

    public void ItemGacha(int GachaTime)  //랜덤한 아이템 이름 띄우기
    {
        Transform[] childList = TextObject.GetComponentsInChildren<Transform>();  //이미 있는 아이템 이름 초기화
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform) Destroy(childList[i].gameObject);
            }
        }

        int x = 1490;
        int y = 1140;

        for (int i = 0; i < GachaTime; i++)  //아이템 이름들의 위치 지정
        {
            int k = 0;
            k = Random.Range(0, gachaItem.Count); //아이템 가챠 부분

            if (GachaTime == 1)
            {
                y = 1020;
            }
            else if (GachaTime == 5)
            {
                y = y - 120;
            }
            else if (GachaTime == 10)
            {
                if (i <= 4)
                {
                    x = 1140;
                }
                else if (i > 4)
                {
                    if (i == 5)
                    {
                        y = 1140;
                    }
                    x = 1840;
                }
                y = y - 120;
            }
            CreateText(new Vector3(x, y, 0), gachaItem[k]["IName"].ToString());  //아이템 이름 출력
        }
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.

        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int g = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(rows[i]);
            if (data.Category.Equals("gacha"))
            {
                //가챠 아이템을 리스트에 저장.
                gachaItem.Add(new Dictionary<string, object>());
                initItem(gachaItem[g], data);
                g++;
            }
        }
    }
    public void CreateText(Vector3 Position, string Result)  //아이템 이름 프리펩("Text")에서 생성
    {
        GameObject ResultText = Instantiate(ResultTxt, Position, Quaternion.identity, TextObject.transform);
        ResultText.GetComponent<Text>().text = Result;
    }
}
