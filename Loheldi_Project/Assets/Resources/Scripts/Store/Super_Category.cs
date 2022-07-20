using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Super_Category : StoreCategoryControl
{
    //category
    [SerializeField]
    private GameObject c_seed;
    [SerializeField]
    private GameObject c_tree;


    List<Dictionary<string, object>> seedItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> treeItem = new List<Dictionary<string, object>>();

    private void Start()
    {
        PopSuperStore();
    }

    public void PopSuperStore()
    {
        GetChartContents("55031");
        MakeCategory(c_seed, seedItem);
        MakeCategory(c_tree, treeItem);
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        
        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0, t = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("seed"))
            {
                seedItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(seedItem[s], data);
                s++;
            }
            else if (data.Category.Equals("tree"))
            {
                treeItem.Add(new Dictionary<string, object>());
                initItem(treeItem[t], data);
                t++;
            }
        }
        //print("seed: " + seedItem.Count);
        //print("tree: " + treeItem.Count);

    }
    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
