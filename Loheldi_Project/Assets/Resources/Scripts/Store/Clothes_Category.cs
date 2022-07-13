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

    JsonData myClothes_rows = null;
    List<GameObject> upper_list = new List<GameObject>();   //상의 아이템을 저장하는 변수
    List<GameObject> lower_list = new List<GameObject>();   //하의 아이템을 저장하는 변수
    List<GameObject> shoes_list = new List<GameObject>();   //신발 아이템을 저장하는 변수
    List<GameObject> acce_list = new List<GameObject>();   //악세서리 아이템을 저장하는 변수

    private void Start()
    {
        PopClothesStore();
    }

    public void PopClothesStore()
    {
        GetChartContents(ChartNum.ClothesItemChart);
        MakeCategory(c_upper, upperItem, upper_list);
        MakeCategory(c_lower, lowerItem, lower_list);
        MakeCategory(c_shoes, shoesItem, shoes_list);
        MakeCategory(c_acce, acceItem, acce_list);
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

    //make category item list on game//
    //전체 아이템을 띄우되, 보유 아이템일 경우와 미보유 아이템일 경우 다른 처리를 합니다.
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/ItemBtn3");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;

            if (itemObject.Count != dialog.Count)    //만약 처음 여는 것이면 새 객체 생성
            {
                //create caltalog box
                child = Instantiate(itemBtn);    //create itemBtn instance
                child.transform.SetParent(category.transform);  //move instance: child
                //아이템 박스 크기 재설정
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.localScale = new Vector3(1f, 1f, 1f);

                itemObject.Add(child);
            }
            else    //아니라면 기존 객체 재활용
            {
                child = itemObject[i];
            }

            GameObject ItemBtn = child.transform.Find("ItemBtn2").gameObject;


            //change catalog box img
            GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Store/Catalog_Images/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = ItemBtn.transform.Find("ItemName").gameObject;
            //print(dialog[i]["IName"]);
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["IName"].ToString();

            //change catalog box item code
            GameObject item_code = ItemBtn.transform.Find("ItemCode").gameObject;
            Text item_code_txt = item_code.GetComponent<Text>();
            item_code_txt.text = dialog[i]["ICode"].ToString();

            //change catalog box price
            GameObject price_parent = ItemBtn.transform.Find("CostImg").gameObject;
            GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
            Text price_txt = item_price.GetComponent<Text>();
            price_txt.text = dialog[i]["Price"].ToString();

            GameObject disable_img = child.transform.Find("Disable").gameObject;
            disable_img.SetActive(false);
            for (int j = 0; j < myClothes_rows.Count; j++)
            {
                MyCustomItem data = pj.ParseBackendData<MyCustomItem>(myClothes_rows[j]);

                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //비활성 창 오브젝트(Disable)를 비활성화
                    disable_img.SetActive(true);
                    
                    break;
                }
            }



        }
    }

}
