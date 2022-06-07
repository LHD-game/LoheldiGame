using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCategoryControl : MonoBehaviour
{
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
    protected void initItem(Dictionary<string, object> item, CustomStoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Price", data.Price);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
        item.Add("Texture", data.Texture);
    }

    //make category item list on game//
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/ItemBtn2");

        for (int i = 0; i < dialog.Count; i++)
        {
            //create caltalog box
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child

            //change catalog box img
            GameObject item_img = child.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Store/Catalog_Images/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            print(dialog[i]["IName"]);
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["IName"].ToString();

            //change catalog box item code
            GameObject item_code = child.transform.Find("ItemCode").gameObject;
            Text item_code_txt = item_code.GetComponent<Text>();
            item_code_txt.text = dialog[i]["ICode"].ToString();

            //change catalog box price
            GameObject price_parent = child.transform.Find("CostImg").gameObject;
            GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
            Text price_txt = item_price.GetComponent<Text>();
            price_txt.text = dialog[i]["Price"].ToString();
        }
    }
}
