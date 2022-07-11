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

    public GameObject Machine;
    public GameObject BackGround;

    List<Dictionary<string, object>> gachaItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaClothes = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaTable = new List<Dictionary<string, object>>();


    private void Start()
    {
        GetChartContents("53552", gachaItem);
        GetChartContents("53550", gachaClothes);
        gachaTable.AddRange(gachaItem);
        gachaTable.AddRange(gachaClothes);
    }

    public void ItemGacha(int GachaTime)  //랜덤한 아이템 이름 띄우기
    {
        Machine.SetActive(true);
        BackGround.SetActive(false);

        Transform[] childList = TextObject.GetComponentsInChildren<Transform>();  //이미 있는 아이템 이름 초기화
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform) Destroy(childList[i].gameObject);
            }
        }

        int x = 745;
        int y = 570;

        for (int i = 0; i < GachaTime; i++)  //아이템 이름들의 위치 지정
        {
            int k = 0;
            k = Random.Range(0, gachaTable.Count); //아이템 가챠 부분
            Debug.Log(gachaTable[k]["IName"]);
            if (GachaTime == 1)
            {
                y = 510;
            }
            else if (GachaTime == 5)
            {
                y = y - 60;
            }
            else if (GachaTime == 10)
            {
                if (i <= 4)
                {
                    x = 570;
                }
                else if (i > 4)
                {
                    if (i == 5)
                    {
                        y = 570;
                    }
                    x = 920;
                }
                y = y - 60;
            }
            CreateText(new Vector3(x, y, 0), gachaTable[k]["IName"].ToString());  //아이템 이름 출력
            this.GetComponent<GachaMachineMovement>().LeverSpin();
        }
    }

    void GetChartContents(string chartNum, List<Dictionary<string, object>> ItemList)  //서버상에 차트를 불러와 저장
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
                ItemList.Add(new Dictionary<string, object>());
                initItem(ItemList[g], data);
                g++;
            }
        }
    }

    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("ItemType", data.ItemType);
    }

    public void CreateText(Vector3 Position, string Result)  //아이템 이름 프리펩("Text")에서 생성
    {
        GameObject ResultText = Instantiate(ResultTxt, Position, Quaternion.identity, TextObject.transform);
        ResultText.GetComponent<Text>().text = Result;
    }
}
