using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenCategory : MonoBehaviour
{
    List<Dictionary<string, object>> seedItem = new List<Dictionary<string, object>>();

    JsonData myInven_rows = new JsonData();
    List<GameObject> seed_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수

    public void PopGarden(GameObject c_seed)
    {
        seedItem.Clear();

        seedItem = new List<Dictionary<string, object>>();

        GetChartContents(ChartNum.AllItemChart);
        MakeCategory(c_seed, seedItem, seed_list);
    }

    void GetChartContents(string itemChart)  //전체 아이템 목록과 보유 아이템 목록을 가져온다.
    {
        var allItemChart = Backend.Chart.GetChartContents(itemChart); //서버의 엑셀파일을 불러온다.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0;

        for (int i = 0; i < allItem_rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(allItem_rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("seed"))   //씨앗 아이템
            {
                seedItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(seedItem[s], data);
                s++;
            }
        }
    }

    GameObject itemBtn;

    //---init list---//
    //itemTheme 별로 모아서 저장
    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }

    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/InvenItemforFarming");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;

            if (itemObject.Count != dialog.Count)    //만약 처음 인벤토리 여는 것이면 새 객체 생성
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

            GameObject ItemBtn = child.transform.Find("ItemBtn").gameObject;

            //change catalog box img
            GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = ItemBtn.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["IName"].ToString();

            //change catalog box item code
            GameObject item_code = ItemBtn.transform.Find("ItemCode").gameObject;
            Text item_code_txt = item_code.GetComponent<Text>();
            item_code_txt.text = dialog[i]["ICode"].ToString();

            GameObject disable_img = child.transform.Find("Disable").gameObject;
            disable_img.SetActive(true);
            for (int j = 0; j < myInven_rows.Count; j++)
            {
                MyItem data = pj.ParseBackendData<MyItem>(myInven_rows[j]);
                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //비활성 창 오브젝트(Disable)를 비활성화
                    disable_img.SetActive(false);
                    //change catalog box price
                    GameObject amount_parent = ItemBtn.transform.Find("Amount").gameObject;
                    GameObject amount_text = amount_parent.transform.Find("Text").gameObject;
                    Text a_txt = amount_text.GetComponent<Text>();
                    a_txt.text = data.Amount.ToString();
                    break;
                }
            }
        }
    }
}
