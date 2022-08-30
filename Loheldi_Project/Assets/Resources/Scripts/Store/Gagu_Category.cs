using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Gagu_Category : StoreCategoryControl
{
    private static Gagu_Category _instance;
    public static Gagu_Category instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Gagu_Category>();
            }
            return _instance;
        }
    }

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
    [SerializeField]
    GameObject[] UpgradeDisable = new GameObject[3];

    List<Dictionary<string, object>> woodItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> modernItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> kitschItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> classicItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> wallpaperItem = new List<Dictionary<string, object>>();

    JsonData myInven_rows = null;
    List<GameObject> wood_list = new List<GameObject>();   //상의 아이템을 저장하는 변수
    List<GameObject> modern_list = new List<GameObject>();   //하의 아이템을 저장하는 변수
    List<GameObject> kitsch_list = new List<GameObject>();   //신발 아이템을 저장하는 변수
    List<GameObject> classic_list = new List<GameObject>();   //악세서리 아이템을 저장하는 변수
    List<GameObject> wallpaper_list = new List<GameObject>();   //악세서리 아이템을 저장하는 변수

    int cnt_my_gagu = 0;
    int all_gagu_cnt = 0;

    private void Start()
    {
        ChkUpgrade();
        PopGaguStore();
    }

    public void PopGaguStore()
    {
        
        woodItem.Clear();
        modernItem.Clear();
        kitschItem.Clear();
        classicItem.Clear();
        wallpaperItem.Clear();

        for (int i = 0; i < wood_list.Count; i++)
        {
            Destroy(wood_list[i]);
        }
        for (int i = 0; i < modern_list.Count; i++)
        {
            Destroy(modern_list[i]);
        }
        for (int i = 0; i < kitsch_list.Count; i++)
        {
            Destroy(kitsch_list[i]);
        }
        for (int i = 0; i < classic_list.Count; i++)
        {
            Destroy(classic_list[i]);
        }
        for (int i = 0; i < wallpaper_list.Count; i++)
        {
            Destroy(wallpaper_list[i]);
        }
        cnt_my_gagu = 0;

        GetChartContents("55031");
        MakeCategory(c_wood, woodItem, wood_list);
        MakeCategory(c_modern, modernItem, modern_list);
        MakeCategory(c_kitsch, kitschItem, kitsch_list);
        MakeCategory(c_classic, classicItem, classic_list);
        MakeCategory(c_wallpaper, wallpaperItem, wallpaper_list);

        Debug.Log("cnt" + cnt_my_gagu);
        if (cnt_my_gagu == 5)
        {
            BadgeManager.GetBadge("B10");
        }
        else if (cnt_my_gagu == all_gagu_cnt)
        {
            BadgeManager.GetBadge("B13");
        }
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
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
        all_gagu_cnt = w + m + k + c + wa;
    }
    //make category item list on game//
    //전체 아이템을 띄우되, 보유 아이템일 경우와 미보유 아이템일 경우 다른 처리를 합니다.
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/ItemBtn3(gagu)");
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
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


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

            

            for (int j = 0; j < myInven_rows.Count; j++)
            {
                MyItem data = pj.ParseBackendData<MyItem>(myInven_rows[j]);

                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //비활성 창 오브젝트(Disable)를 활성화
                    disable_img.SetActive(true);
                    cnt_my_gagu++;
                    break;
                }
            }
        }
    }

    public void ChkUpgrade()   //집 확장 정도를 검사하여 상점 탭에 반영
    {
        for(int i =0; i< UpgradeDisable.Length; i++)
        {
            UpgradeDisable[i].SetActive(true);
        }
        int my_house_lv = PlayerPrefs.GetInt("HouseLv");
        if (my_house_lv != 4)
        {
            UpgradeDisable[my_house_lv-1].SetActive(false);
        }
        
        
    }
}
