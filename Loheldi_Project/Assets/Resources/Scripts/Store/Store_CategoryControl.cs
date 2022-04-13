using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Store_CategoryControl : MonoBehaviour
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

    GameObject itemBtn;

    private void Start()
    {
        
    }

    public void PopGaguStore()
    {
        GetChartContents("46292");
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
            print(data.itemCode);
            print(data.itemTheme);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.itemTheme.Equals("wood"))
            {
                woodItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(woodItem[w], data);
                w++;
            }
            else if (data.itemTheme.Equals("modern"))
            {
                modernItem.Add(new Dictionary<string, object>());
                initItem(modernItem[m], data);
                m++;
            }
            else if (data.itemTheme.Equals("kitsch"))
            {
                kitschItem.Add(new Dictionary<string, object>());
                
                initItem(kitschItem[k], data);
                k++;
            }
            else if (data.itemTheme.Equals("classic"))
            {
                classicItem.Add(new Dictionary<string, object>());
                
                initItem(classicItem[c], data);
                c++;
            }
            else if (data.itemTheme.Equals("wallpaper"))
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

    //---init list---//
    //itemTheme 별로 모아서 저장
    void initItem(Dictionary<string, object> item, StoreItem data) 
    {
        print("initItem");
        item.Add("itemCode", data.itemCode);
        item.Add("name", data.name);
        item.Add("price", data.price);
        item.Add("itemTheme", data.itemTheme);
        item.Add("itemType", data.itemTheme);
    }


    //make category item list on game//
    void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog)   
    {
        itemBtn = (GameObject)Resources.Load("Prefebs/UI/ItemBtn2");
        print(dialog.Count);
        for(int i=0; i < dialog.Count; i++)
        {
            //create caltalog box
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
            
            //change catalog box img
            GameObject item_img= child.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Store/Catalog_Images/"+ dialog[i]["itemCode"] + "_catalog");
            print(dialog[i]["itemCode"]);

            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["name"].ToString();
            print(dialog[i]["name"]);

            //change catalog box price
            GameObject price_parent = child.transform.Find("CostImg").gameObject;
            GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
            Text price_txt = item_price.GetComponent<Text>();
            price_txt.text = dialog[i]["price"].ToString();
            print(dialog[i]["price"]);
        }
    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
