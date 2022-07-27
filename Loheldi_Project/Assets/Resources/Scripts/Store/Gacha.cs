using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    GameObject itemBox;
    JsonData myInven_rows = new JsonData();

    public GameObject Contents;
    public GameObject ResultTxt;

    public GameObject Machine;
    public GameObject BackGround;

    public int GachaPrice = 100;

    List<Dictionary<string, object>> gachaItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaClothes = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaTable = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gachaResultTable = new List<Dictionary<string, object>>();


    private void Start()
    {
        GetChartContents("55031", gachaItem);
        GetChartContents("54323", gachaClothes);
        gachaTable.AddRange(gachaItem);
        gachaTable.AddRange(gachaClothes);
    }

    public void ItemGacha(int GachaTime)  //가챠 돌리기
    {
        if (PlayerPrefs.GetInt("Wallet") >= GachaPrice * GachaTime)  //돈이 가챠 가격보다 많다면
        {
            gachaResultTable.Clear();

            Transform[] childList = Contents.GetComponentsInChildren<Transform>();  //이미 있는 아이템 이름 초기화
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform) Destroy(childList[i].gameObject);
                }
            }

            int x = 745;
            int y = 570;

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
                    PlayInfoManager.GetCoin(-GachaPrice);
                }
            }
            CreateItemBox(Contents, gachaResultTable);      //아이템 아이콘 생성 스크립트
            this.GetComponent<GachaMachineMovement>().LeverSpin();      //레버 돌리기
            Debug.Log("애니메이션 작동");
        }
        else          //돈이 모자라다면
        {
            Debug.Log("슈퍼맨 : 저리가! 거지야!");
        }
    }

    void GetChartContents(string chartNum, List<Dictionary<string, object>> ItemList)  //서버상에 차트를 불러와 저장
    {
        var allItemChart = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];

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

    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
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
            for (int j = 0; j < dialog.Count; j++)
            {
                MyItem data = pj.ParseBackendData<MyItem>(myInven_rows[j]);
                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //change catalog box price
                    GameObject amount_parent = child.transform.Find("Amount").gameObject;
                    GameObject amount_text = amount_parent.transform.Find("Text").gameObject;
                    Text a_txt = amount_text.GetComponent<Text>();
                    a_txt.text = data.Amount.ToString();
                    break;
                }
            }
        }
    }
}
