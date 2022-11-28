using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�κ��丮 �������� ī�װ��� ���� Ŭ�����Դϴ�. ���⿡ �ٸ� �� ��� �������� �����ּ���^^; �� Ŭ�������� �� ���� ��ɸ�!//
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
    List<GameObject> super_list = new List<GameObject>();   //�κ��丮 �������� �����ϴ� ����
    List<GameObject> gagu_list = new List<GameObject>();   //�κ��丮 �������� �����ϴ� ����
    List<GameObject> crop_list = new List<GameObject>();   //�κ��丮 �������� �����ϴ� ����

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

    protected void GetChartContents(string itemChart)  //��ü ������ ��ϰ� ���� ������ ����� �����´�.
    {
        var allItemChart = Backend.Chart.GetChartContents(itemChart); //������ ���������� �ҷ��´�.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0, g = 0, c = 0, m;

        for (int i = 0; i < allItem_rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(allItem_rows[i]);

            //������ �׸��� ���� �ٸ� ����Ʈ�� ����.

            if (data.Category.Equals("seed") || data.Category.Equals("tree") || data.Category.Equals("interior"))   //���� ������
            {
                superItem.Add(new Dictionary<string, object>()); // list�� ������ ������ݴϴ�.
                initItem(superItem[s], data);
                s++;
            }
            //����� ������
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
            //��Ÿ ������: �ϴ� �۹� �ǿ� �־�д�
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
    //itemTheme ���� ��Ƽ� ����
    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Price", data.Price);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }

    //make category item list on game//
    //��ü �������� ����, ���� �������� ���� �̺��� �������� ��� �ٸ� ó���� �մϴ�.
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/InvenItem");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;
        
            if (itemObject.Count != dialog.Count)    //���� ó�� �κ��丮 ���� ���̸� �� ��ü ����
            {
                //create caltalog box
                child = Instantiate(itemBtn);    //create itemBtn instance
                child.transform.SetParent(category.transform);  //move instance: child
                                                                //������ �ڽ� ũ�� �缳��
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.localScale = new Vector3(1f, 1f, 1f);

                itemObject.Add(child);
            }
            else    //�ƴ϶�� ���� ��ü ��Ȱ��
            {
                child = itemObject[i];
            }

            GameObject ItemBtn = child.transform.Find("ItemBtn").gameObject;

            //change catalog box img
            GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (���ý� �ش� �������� ã�� ���� ����ǥ �뵵)
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
                    //��Ȱ�� â ������Ʈ(Disable)�� ��Ȱ��ȭ
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
