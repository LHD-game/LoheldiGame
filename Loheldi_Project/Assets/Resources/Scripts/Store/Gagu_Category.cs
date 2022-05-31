using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Gagu_Category : StoreCategoryControl
{

    //category
    [SerializeField]
    private GameObject c_wood;
    [SerializeField]
    private GameObject c_modern;
    [SerializeField]
    private GameObject c_kitsch;
    [SerializeField]
    private GameObject c_classic;
    [SerializeField]
    private GameObject c_wallpaper;

    List<Dictionary<string, object>> woodItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> modernItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> kitschItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> classicItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> wallpaperItem = new List<Dictionary<string, object>>();

    private void Start()
    {
        GetChartContents("51350");
    }

    public void PopGaguStore()
    {
        MakeCategory(c_wood, woodItem);
        MakeCategory(c_modern, modernItem);
        MakeCategory(c_kitsch, kitschItem);
        MakeCategory(c_classic, classicItem);
        MakeCategory(c_wallpaper, wallpaperItem);
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        
        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int w = 0, m = 0, k = 0, c = 0, wa = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("wood"))
            {
                woodItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(woodItem[w], data);
                w++;
            }
            else if (data.Category.Equals("modern"))
            {
                modernItem.Add(new Dictionary<string, object>());
                initItem(modernItem[m], data);
                m++;
            }
            else if (data.Category.Equals("kitsch"))
            {
                kitschItem.Add(new Dictionary<string, object>());
                
                initItem(kitschItem[k], data);
                k++;
            }
            else if (data.Category.Equals("classic"))
            {
                classicItem.Add(new Dictionary<string, object>());
                
                initItem(classicItem[c], data);
                c++;
            }
            else if (data.Category.Equals("wallpaper"))
            {
                wallpaperItem.Add(new Dictionary<string, object>());
                
                initItem(wallpaperItem[wa], data);
                wa++;
            }
        }
        print("wood: " + woodItem.Count);
        print("modern: " + modernItem.Count);
        print("kitsch: " + kitschItem.Count);
        print("classic: " + classicItem.Count);
        print("wallpaper: " + wallpaperItem.Count);
    }
    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
