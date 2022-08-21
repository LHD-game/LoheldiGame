using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField]
    Text MyCoin;
    [SerializeField]
    GameObject[] GachaBuyList = new GameObject[5];  //이름, 내 코인, 가격, 잔액, disable btn
    [SerializeField]
    GameObject ResultContent;

    GameObject itemBox;
    JsonData myInven_rows = new JsonData();

    int my_coin = 0;
    int gacha_price = 80; //1회 80 코인
    string gacha_type = ""; //뽑기 종류(의상, 가구)
    int need_coin = 0;
    string[] rand_item_list = new string[100];  //아이템 코드를 확률 개수 별로 삽입한다.
    
    

    List<Dictionary<string, object>> gachaGagu = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaClothes = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaTable = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaResultTable = new List<Dictionary<string, object>>();


    private void Start()
    {
        SceneUpdate();

        GetChartContents(ChartNum.AllItemChart, gachaGagu);
        GetChartContents(ChartNum.ClothesItemChart, gachaClothes);
        gachaTable.AddRange(gachaGagu);
        gachaTable.AddRange(gachaClothes);
    }

    void SceneUpdate()
    {
        my_coin = PlayerPrefs.GetInt("Wallet");
        MyCoin.text = my_coin.ToString();
    }

    //레어 의상 뽑기 클릭
    public void PopGClothesBuyPanel(int num)   //매개변수: 돌릴 횟수
    {
        gacha_type = "clothes";
        need_coin = gacha_price * num;
        ChangeBuyPanel(num);
    }

    //레어 가구 뽑기 클릭
    public void PopGGaguBuyPanel(int num)   //매개변수: 돌릴 횟수
    {
        gacha_type = "gagu";
        need_coin = gacha_price * num;
        ChangeBuyPanel(num);
    }

    void ChangeBuyPanel(int num)
    {
        string item_name = "";
        if (gacha_type.Equals("clothes"))
        {
            item_name = num + "회 레어 의상 뽑기";
        }
        else
        {
            item_name = num + "회 레어 가구 뽑기";
        }

        //아이템 명
        Text ItemNameTxt = GachaBuyList[0].GetComponent<Text>();
        ItemNameTxt.text = item_name;

        //내 소지 코인
        GameObject MyCoin = GachaBuyList[1].transform.Find("Text").gameObject;
        Text MyCoinTxt = MyCoin.GetComponent<Text>();
        MyCoinTxt.text = my_coin.ToString();

        //아이템 가격
        GameObject ItemCost = GachaBuyList[2].transform.Find("Text").gameObject;
        Text ItemCostTxt = ItemCost.GetComponent<Text>();
        ItemCostTxt.text = need_coin.ToString();

        //잔액
        int result_coin = my_coin - need_coin;

        GameObject ChangeCost = GachaBuyList[3].transform.Find("Text").gameObject;
        Text ChangeCostTxt = ChangeCost.GetComponent<Text>();
        ChangeCostTxt.text = result_coin.ToString();

        //Disable Btn
        if (result_coin < 0)
        {
            GachaBuyList[4].SetActive(false);
        }
        else
        {
            GachaBuyList[4].SetActive(true);
        }
    }

    public void BuyGachaBtn()
    {
        PlayInfoManager.GetCoin(-need_coin);    //코인 차감

    }

    void GetChartContents(string chartNum, List<Dictionary<string, object>> ItemList)  //서버상에 차트를 불러와 저장
    {
        var allItemChart = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.

        JsonData rows = allItemChart.GetReturnValuetoJSON()["rows"];
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

    void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Price", data.Price);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }

    void initItem(Dictionary<string, object> item, CustomStoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Price", data.Price);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
        item.Add("Texture", data.Texture);
    }

    public void ItemGacha(int GachaTime)  //가챠 돌리기
    {
        gachaResultTable.Clear();

        Transform[] childList = ResultContent.GetComponentsInChildren<Transform>();  //이미 있는 아이템 이름 초기화
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform) Destroy(childList[i].gameObject);
            }
        }

        for (int i = 0; i < GachaTime; i++)  //아이템 이름들의 위치 지정
        {
            int k = 0;
            k = Random.Range(0, gachaTable.Count); //아이템 가챠 부분
            gachaResultTable.Add(gachaTable[k]);  //뽑힌 아이템을 결과 리스트 삽입

            //Inventory 테이블 불러와서, 여기에 해당하는 아이템과 일치하는 코드가 있을 경우 개수를 1증가시켜서 업데이트
            Where where = new Where();
            where.Equal("ICode", gachaTable[k]["ICode"].ToString());
            var bro = Backend.GameData.GetMyData("INVENTORY", where);

            if (bro.IsSuccess() == false)
            {
                Debug.Log("요청 실패");
            }
            else
            {
                JsonData rows = bro.GetReturnValuetoJSON()["rows"];
                //없을 경우 아이템 행 추가
                if (rows.Count <= 0)
                {
                    Param param = new Param();
                    param.Add("ICode", gachaTable[k]["ICode"].ToString());
                    param.Add("Amount", 1);

                    var insert_bro = Backend.GameData.Insert("INVENTORY", param);
                }
                //있을 경우 해당 아이템 indate찾고, 개수 수정
                else
                {
                    string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                    int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                    item_amount++;

                    Param param = new Param();
                    param.Add("ICode", gachaTable[k]["ICode"].ToString());
                    param.Add("Amount", item_amount);

                    var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                }
            }
        }
        CreateItemBox(ResultContent, gachaResultTable);      //아이템 아이콘 생성 스크립트
        this.GetComponent<GachaMachineMovement>().LeverSpin();      //레버 돌리기
        Debug.Log("애니메이션 작동");
        
    }

    

    

    public void CreateItemBox(GameObject category, List<Dictionary<string, object>> dialog)  //아이템 이름 프리펩("Text")에서 생성
    {
        itemBox = (GameObject)Resources.Load("Prefabs/UI/ItemBtnforGacha");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;
            //create caltalog box
            child = Instantiate(itemBox);    //create itemBtn instance
            child.transform.SetParent(category.transform, false);  //move instance: child
                                                                   //아이템 박스 크기 재설정
            RectTransform rt = child.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1f, 1f, 1f);

            //change catalog box img
            GameObject item_img = child.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["IName"].ToString();

            //change catalog box item code
            GameObject item_code = child.transform.Find("ItemCode").gameObject;
            Text item_code_txt = item_code.GetComponent<Text>();
            item_code_txt.text = dialog[i]["ICode"].ToString();

            Debug.Log(dialog.Count);
        }
    }
}
