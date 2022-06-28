using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryControl : MonoBehaviour
{
    GameObject itemBtn;

    //---init list---//
    protected void initCustomItem(Dictionary<string, object> item, CustomStoreItem data)
    {
        print("initCustomItem");
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
        item.Add("Texture", data.Texture);
    }

    //make category item list on game//
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, string scene = "Custom")
    {
        if (scene.Equals("Custom")) //Ŀ���� ���� ���
        {
            itemBtn = (GameObject)Resources.Load("Prefabs/Customize/ItemBtn");
        }
        else if(scene.Equals("Closet")) //�ǻ� ���� ���� ���
        {
            itemBtn = (GameObject)Resources.Load("Prefabs/Customize/ClosetItemBtn");
        }
        
        Debug.Log(dialog.Count);
        for (int i = dialog.Count-1; i >= 0 ; i--)
        {
            //create caltalog box
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child

            //change catalog box img
            GameObject item_img = child.transform.Find("ItemImage").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Customize/Catalog_Images/" + dialog[i][CommonField.nName] + "_catalog");

            //change catalog box item name (���ý� �ش� �������� ã�� ���� ����ǥ �뵵)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i][CommonField.nName].ToString();
        }
    }
}
