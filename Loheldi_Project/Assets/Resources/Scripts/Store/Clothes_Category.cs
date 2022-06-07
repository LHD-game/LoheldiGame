using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Clothes_Category : StoreCategoryControl
{
    //category
    [SerializeField]
    private GameObject c_upper;
    [SerializeField]
    private GameObject c_lower;
    [SerializeField]
    private GameObject c_shoes;
    [SerializeField]
    private GameObject c_acce;


    List<Dictionary<string, object>> upperItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> lowerItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> shoesItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> acceItem = new List<Dictionary<string, object>>();

    JsonData myClothes_rows = new JsonData();
    List<GameObject> upper_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    List<GameObject> lower_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    List<GameObject> shoes_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    List<GameObject> acce_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수

    private void Start()
    {
        PopClothesStore();
    }

    public void PopClothesStore()
    {
        GetChartContents("51350");
        MakeCategory(c_upper, upperItem);
        MakeCategory(c_lower, lowerItem);
        MakeCategory(c_shoes, shoesItem);
        MakeCategory(c_acce, acceItem);
    }

    void GetChartContents(string itemChart)  //전체 아이템 목록과 보유 아이템 목록을 가져온다.
    {
        var allClothesChart = Backend.Chart.GetChartContents(itemChart); //서버의 엑셀파일을 불러온다.
        var myClothes = Backend.GameData.GetMyData("ACC_CLOSET", new Where(), 100);

        JsonData allClothes_rows = allClothesChart.GetReturnValuetoJSON()["rows"];
        myClothes_rows = myClothes.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0, g = 0, c = 0, m = 0;



        for (int i = 0; i < allClothes_rows.Count; i++)
        {
            CustomStoreItem data = pj.ParseBackendData<CustomStoreItem>(allClothes_rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("upper"))
            {
                upperItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(upperItem[s], data);
                s++;
            }
            
            else if (data.Category.Equals("lower"))
            {
                lowerItem.Add(new Dictionary<string, object>());
                initItem(lowerItem[g], data);
                g++;
            }
            else if (data.Category.Equals("shoes"))
            {
                shoesItem.Add(new Dictionary<string, object>());

                initItem(shoesItem[c], data);
                c++;
            }
            
            else if(data.Category.Equals("accessory"))
            {
                acceItem.Add(new Dictionary<string, object>());

                initItem(acceItem[m], data);
                m++;
            }
        }
    }


}
