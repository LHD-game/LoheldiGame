using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Custom_Category : StoreCategoryControl
{
    private static Custom_Category _instance;
    public static Custom_Category instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Custom_Category>();
            }
            return _instance;
        }
    }

    //category
    [SerializeField]
    private GameObject c_hair;

    List<Dictionary<string, object>> hairItem = new List<Dictionary<string, object>>();


    JsonData myCustom_rows = null;
    List<GameObject> hair_list = new List<GameObject>();   //머리 아이템을 저장하는 변수


    private void Start()
    {
        PopCustomStore();
    }

    public void PopCustomStore()
    {
        hairItem.Clear();
        for (int i = 0; i < hair_list.Count; i++)
        {
            Destroy(hair_list[i]);
        }


        GetChartContents(ChartNum.CustomItemChart);
        MakeCategory(c_hair, hairItem, hair_list);

    }

    void GetChartContents(string itemChart)  //전체 아이템 목록과 보유 아이템 목록을 가져온다.
    {
        var allClothesChart = Backend.Chart.GetChartContents(itemChart); //서버의 엑셀파일을 불러온다.
        var myClothes = Backend.GameData.GetMyData("ACC_CUSTOM", new Where(), 100);

        JsonData allClothes_rows = allClothesChart.GetReturnValuetoJSON()["rows"];
        myCustom_rows = myClothes.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int h = 0;

        for (int i = 0; i < allClothes_rows.Count; i++)
        {
            CustomStoreItem data = pj.ParseBackendData<CustomStoreItem>(allClothes_rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("hair"))
            {
                hairItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(hairItem[h], data);
                h++;
            }
        }
    }

    //make category item list on game//
    //전체 아이템을 띄우되, 보유 아이템일 경우와 미보유 아이템일 경우 다른 처리를 합니다.
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/ItemBtn_CustomS");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;

            //create caltalog box
            child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
            //아이템 박스 크기 재설정
            RectTransform rt = child.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1f, 1f, 1f);

            itemObject.Add(child);


            GameObject ItemBtn = child.transform.Find("ItemBtn2").gameObject;


            //change catalog box img
            GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Customize/" + dialog[i]["ICode"] + "_catalog");


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
            for (int j = 0; j < myCustom_rows.Count; j++)
            {
                MyCustomItem data = pj.ParseBackendData<MyCustomItem>(myCustom_rows[j]);

                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //비활성 창 오브젝트(Disable)를 활성화
                    disable_img.SetActive(true);
                    
                    break;
                }
            }



        }
    }

}
