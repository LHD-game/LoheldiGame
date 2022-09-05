using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//인벤토리 아이템의 카테고리를 위한 클래스입니다. 여기에 다른 곳 기능 덧붙이지 말아주세요^^; 한 클래스에는 한 가지 기능만!//
public class InvenCategory : MonoBehaviour
{
    GridLayoutGroup csf;
    //category
    [SerializeField]
    private GameObject c_super;
    [SerializeField]
    private GameObject c_gagu;
    [SerializeField]
    private GameObject c_crops;

    List<Dictionary<string, object>> superItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> gaguItem = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> cropsItem = new List<Dictionary<string, object>>();

    JsonData myInven_rows = new JsonData();
    List<GameObject> super_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    List<GameObject> gagu_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수
    List<GameObject> crop_list = new List<GameObject>();   //인벤토리 아이템을 저장하는 변수

    GameObject child;

    public void PopInven()
    {
        superItem.Clear();
        gaguItem.Clear();
        cropsItem.Clear();

        superItem = new List<Dictionary<string, object>>();
        gaguItem = new List<Dictionary<string, object>>();
        cropsItem = new List<Dictionary<string, object>>();

        GetChartContents(ChartNum.AllItemChart);
        MakeCategory(c_super, superItem, super_list);
        MakeCategory(c_gagu, gaguItem, gagu_list);
        MakeCategory(c_crops, cropsItem, crop_list);
        Inven_CategorySelect.instance.initInven();
    }

    protected void GetChartContents(string itemChart)  //전체 아이템 목록과 보유 아이템 목록을 가져온다.
    {
        var allItemChart = Backend.Chart.GetChartContents(itemChart); //서버의 엑셀파일을 불러온다.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0, g = 0, c = 0, m;

        for (int i = 0; i < allItem_rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(allItem_rows[i]);

            //아이템 테마에 따라 다른 리스트에 저장.

            if (data.Category.Equals("seed") || data.Category.Equals("tree") || data.Category.Equals("interior"))   //슈퍼 아이템
            {
                superItem.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                initItem(superItem[s], data);
                s++;
            }
            //목공방 아이템
            else if (data.Category.Equals("wood") || data.Category.Equals("modern") || data.Category.Equals("kitsch") || data.Category.Equals("classic") || data.Category.Equals("wallpaper"))
            {
                gaguItem.Add(new Dictionary<string, object>());
                initItem(gaguItem[g], data);
                g++;
            }
            else if (data.Category.Equals("crops"))
            {
                cropsItem.Add(new Dictionary<string, object>());

                initItem(cropsItem[c], data);
                c++;
            }
            //기타 아이템: 일단 작물 탭에 넣어둔다
            else
            {
                cropsItem.Add(new Dictionary<string, object>());

                initItem(cropsItem[c], data);
                c++;
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
        item.Add("Price", data.Price);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }

    //make category item list on game//
    //전체 아이템을 띄우되, 보유 아이템일 경우와 미보유 아이템일 경우 다른 처리를 합니다.
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/InvenItem");
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

            for (int j=0; j< myInven_rows.Count; j++)
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
            csf = category.GetComponent<GridLayoutGroup>();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
        }
    }
}
