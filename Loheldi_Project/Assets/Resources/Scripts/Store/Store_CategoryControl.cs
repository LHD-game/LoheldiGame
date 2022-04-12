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
    private GameObject c_skin;
    [SerializeField]
    private GameObject c_eyes;
    [SerializeField]
    private GameObject c_mouth;
    [SerializeField]
    private GameObject c_hair;

    List<Dictionary<string, object>> woodItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> modernItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> kitschItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> classicItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> wallpaperItem = new List<Dictionary<string, object>>();
    //ListDictionary woodItem

    private void Start()
    {
        PopGaguStore();
    }

    public void PopGaguStore()
    {
        GetChartContents("46292");
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        
        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();
        for (int i = 0; i < rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(rows[i]);
            print(data.itemCode);
            print(data.itemTheme);

            int w = 0, m = 0, k = 0, c = 0, wa = 0;

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.itemTheme.Equals("wood"))
            {
                w++;
                print(w);
                initItem(woodItem[w], data); //오류 발생 구간. 해결 필요.
            }
            else if (data.itemTheme.Equals("modern"))
            {
                m++;
                initItem(modernItem[m], data);
            }
            else if (data.itemTheme.Equals("kitsch"))
            {
                k++;
                initItem(kitschItem[k], data);
            }
            else if (data.itemTheme.Equals("classic"))
            {
                c++;
                initItem(classicItem[c], data);
            }
            else if (data.itemTheme.Equals("wallpaper"))
            {
                wa++;
                initItem(wallpaperItem[wa], data);
            }

            print(w);
            print(m);
            print(k);
            print(c);
            print(wa);
        }
        
        

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
        /*itemBtn = (GameObject)Resources.Load("Prefebs/Customize/ItemBtn");
        print(dialog.Count);
        for(int i=0; i < dialog.Count; i++)
        {
            //create caltalog box
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
            
            //change catalog box img
            GameObject item_img= child.transform.Find("ItemImage").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Customize/Catalog_Images/"+ dialog[i][CommonField.nName] + "_catalog");
            print(dialog[i][CommonField.nName]);

            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i][CommonField.nName].ToString();
        }*/
    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
