using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//1. 아이템 리스트에서 확률percent만큼 아이템코드를 배열 가챠확률[100]에 넣는다.
//2. 랜덤한 수를 뽑아 배열에서 해당 배열값(아이템 코드)를 가져온다. -> 횟수만큼 반복
//3. 이를 서버에 저장한다. 아이템 중복은 코인으로 대체
//4. 결과를 content에 띄운다

public class Gacha : MonoBehaviour
{
    [SerializeField]
    Text MyCoin;
    [SerializeField]
    GameObject[] GachaBuyList = new GameObject[5];  //이름, 내 코인, 가격, 잔액, disable btn
    [SerializeField]
    GameObject ResultPanel;
    [SerializeField]
    GameObject ResultContent;

    int my_coin = 0;
    int gacha_price = 80; //1회 80 코인
    string gacha_type = ""; //뽑기 종류(의상, 가구)
    int try_num = 0;    //뽑기 연속 실행 횟수
    int need_coin = 0;
    string[] clothes_icode_percent = new string[100];  //아이템 코드를 확률 개수 별로 삽입한다.
    string[] gagu_icode_percent = new string[100];

    Coroutine cor;

    List<Dictionary<string, object>> gachaGaguItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaClothesItem = new List<Dictionary<string, object>>();

    List<GameObject> gachaResultList = new List<GameObject>();   //뽑기 결과 오브젝트 객체를 저장하는 변수

    private void Start()
    {
        SceneUpdate();

        //GetChartContents(ChartNum.AllItemChart, gachaGagu);
        GetChartContents(ChartNum.GachaClothesChart, gachaClothesItem);
        GetChartContents(ChartNum.GachaGaguChart, gachaGaguItem);

        //예: Percent값이 5였다면 Icode가 5번 들어간다.
        int k = 0;
        for(int i=0; i<gachaClothesItem.Count; i++)
        {
            int per_num = int.Parse(gachaClothesItem[i]["Percent"].ToString());
            for (int j=0; j<per_num; j++)
            {
                clothes_icode_percent[k] = gachaClothesItem[i]["ICode"].ToString();
                k++;
            }
        }

        int h = 0;
        for (int i = 0; i < gachaGaguItem.Count; i++)
        {
            int per_num = int.Parse(gachaGaguItem[i]["Percent"].ToString());
            for (int j = 0; j < per_num; j++)
            {
                gagu_icode_percent[h] = gachaGaguItem[i]["ICode"].ToString();
                h++;
            }
        }
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
        try_num = num;
        ChangeBuyPanel();
    }

    //레어 가구 뽑기 클릭
    public void PopGGaguBuyPanel(int num)   //매개변수: 돌릴 횟수
    {
        gacha_type = "gagu";
        need_coin = gacha_price * num;
        try_num = num;
        ChangeBuyPanel();
    }

    void ChangeBuyPanel()
    {
        string item_name = "";
        if (gacha_type.Equals("clothes"))
        {
            item_name = try_num + "회 레어 의상 뽑기";
        }
        else
        {
            item_name = try_num + "회 레어 가구 뽑기";
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
            GachaBuyList[4].SetActive(true);
        }
        else
        {
            GachaBuyList[4].SetActive(false);
        }
    }

    public void BuyGachaBtn()
    {
        //기존 결과 삭제
        for(int i=0; i< gachaResultList.Count; i++)
        {
            Destroy(gachaResultList[i]);
        }

        PlayInfoManager.GetCoin(-need_coin);    //코인 차감

        //todo: 화려한 이펙트와 돌아가는 뽑기기계 애니메이션

        ResultPanel.SetActive(true);

        if (gacha_type.Equals("clothes"))
        {
            cor = StartCoroutine(ClothesGachaResult());
        }
        else
        {
            cor = StartCoroutine(GaguGachaResult());
        }
        
        SceneUpdate();
    }

    IEnumerator ClothesGachaResult()
    {
        for(int i=0; i<try_num; i++)
        {
            int r_num = Random.Range(0, 100);
            //Debug.Log(clothes_icode_percent[r_num]);
            string icode = clothes_icode_percent[r_num];
            for(int j=0; j < gachaClothesItem.Count; j++)
            {
                if (gachaClothesItem[j]["ICode"].ToString().Equals(icode))
                {
                    int amount = int.Parse(gachaClothesItem[j]["Amount"].ToString());
                    //서버에 저장(중복은 40코인으로 대체)
                    //결과 화면에 띄우기
                    if (gachaClothesItem[j]["ItemType"].ToString().Equals("Clothes"))   //의상 아이템
                    {
                        SaveItemCloset(icode);
                        ResultCategory(icode, amount, "Clothes");
                    }
                    else    //인벤토리 아이템
                    {
                        SaveItemInven(icode, amount);
                        ResultCategory(icode, amount, "Item");
                    }
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }

    IEnumerator GaguGachaResult()
    {
        for (int i = 0; i < try_num; i++)
        {
            int r_num = Random.Range(0, 100);
            //Debug.Log(clothes_icode_percent[r_num]);
            string icode = gagu_icode_percent[r_num];
            for (int j = 0; j < gachaGaguItem.Count; j++)
            {
                if (gachaGaguItem[j]["ICode"].ToString().Equals(icode))
                {
                    int amount = int.Parse(gachaGaguItem[j]["Amount"].ToString());
                    //서버에 저장(중복은 40코인으로 대체)
                    //결과 화면에 띄우기
                    if (gachaGaguItem[j]["ItemType"].ToString().Equals("Gagu"))   //의상 아이템
                    {
                        SaveItemGagu(icode);
                        ResultCategory(icode, amount, "Gagu");
                    }
                    else    //인벤토리 아이템
                    {
                        SaveItemInven(icode, amount);
                        ResultCategory(icode, amount, "Item");
                    }
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }

    void GetChartContents(string chartNum, List<Dictionary<string, object>> ItemList)  //서버상에 차트를 불러와 저장
    {
        var allItemChart = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.

        JsonData rows = allItemChart.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int g = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            GachaItem data = pj.ParseBackendData<GachaItem>(rows[i]);

            //가챠 아이템을 리스트에 저장.
            ItemList.Add(new Dictionary<string, object>());
            initItem(ItemList[g], data);
            g++;
        }
    }

    void initItem(Dictionary<string, object> item, GachaItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("ItemType", data.ItemType);
        item.Add("Amount", data.Amount);
        item.Add("Percent", data.Percent);
    }

    //아이템을 인벤토리에 저장
    void SaveItemInven(string icode, int amount)
    {
        Where where = new Where();
        where.Equal("ICode", icode);
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
                param.Add("ICode", icode);
                param.Add("Amount", amount);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("아이템 획득 완료: " + icode);
                }
                else
                {
                    Debug.Log("아이템 획득 오류");
                }
            }
            //있을 경우 해당 아이템 indate찾고, 개수 수정
            else
            {
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount += amount;

                Param param = new Param();
                param.Add("ICode", icode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("아이템 " +amount+ "개 획득 완료: " + icode);

                }
                else
                {
                    Debug.Log("아이템 획득 오류");
                }
            }
        }
    }

    //의상 아이템을 옷장 테이블에 저장
    void SaveItemCloset(string icode)
    {
        Where where = new Where();
        where.Equal("ICode", icode);
        var bro = Backend.GameData.GetMyData("ACC_CLOSET", where);

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
                param.Add("ICode", icode);

                var insert_bro = Backend.GameData.Insert("ACC_CLOSET", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("의상 획득 완료: " + icode);
                }
                else
                {
                    Debug.Log("의상 획득 오류");
                }
            }
            //있을 경우 40 코인으로 대체
            else
            {
                PlayInfoManager.GetCoin(40);
            }
        }
    }

    //가구 아이템을 옷장 테이블에 저장
    void SaveItemGagu(string icode)
    {
        Where where = new Where();
        where.Equal("ICode", icode);
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
                param.Add("ICode", icode);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("가구 획득 완료: " + icode);
                }
                else
                {
                    Debug.Log("가구 획득 오류");
                }
            }
            //있을 경우 40 코인으로 대체
            else
            {
                PlayInfoManager.GetCoin(40);
            }
        }
    }

    void ResultCategory(string icode, int amount, string item_type)
    {
        GameObject itemBtn = (GameObject)Resources.Load("Prefabs/UI/GachaItem");
        ParsingJSON pj = new ParsingJSON();

        GameObject child;

        //create caltalog box
        child = Instantiate(itemBtn);    //create itemBtn instance
        child.transform.SetParent(ResultContent.transform);  //move instance: child
        
        //아이템 박스 크기 재설정
        RectTransform rt = child.GetComponent<RectTransform>();
        rt.localScale = new Vector3(1f, 1f, 1f);
        Vector3 position = rt.localPosition;
        position.z = 0;
        rt.localPosition = position;

        gachaResultList.Add(child);


        GameObject ItemBtn = child.transform.Find("ItemBtn").gameObject;


        //change catalog box img
        GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
        Image img = item_img.GetComponent<Image>();
        if (item_type.Equals("Clothes"))
        {
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Customize/" + icode + "_catalog");
        }
        else //Gagu, Item
        {
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + icode + "_catalog");
        }

        //change catalog box price
        GameObject amount_parent = ItemBtn.transform.Find("Amount").gameObject;
        GameObject amount_text = amount_parent.transform.Find("Text").gameObject;
        Text a_txt = amount_text.GetComponent<Text>();
        a_txt.text = amount.ToString();

        /*        //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
                GameObject item_name = ItemBtn.transform.Find("ItemName").gameObject;
                //print(dialog[i]["IName"]);
                Text txt = item_name.GetComponent<Text>();
                txt.text = dialog[i]["IName"].ToString();
        */
    }

/*    public void ItemGacha(int GachaTime)  //가챠 돌리기
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
        
    }*/

    /*public void CreateItemBox(GameObject category, List<Dictionary<string, object>> dialog)  //아이템 이름 프리펩("Text")에서 생성
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
    }*/
}
