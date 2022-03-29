using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryControl : MonoBehaviour
{
    GameObject itemBtn;

    //category
    [SerializeField]
    private GameObject c_skin;
    private GameObject c_eyes;
    private GameObject c_mouth;
    private GameObject c_hair;

    
    List<Dictionary<string, object>> skin_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<object> eyes_Dialog;

    enum ColumnName
    {
        CID,
        Name,
        Model,
        Meterial,
        Texture
    }




    void Start()
    {
        CommonField.SetDataDialog(CSVReader.Read("Customize/CustomDB"));    //DB parse

        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();

        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))
            {
                initSkin(d_dialog[i]);
            }
        }
        MakeCategory(c_skin, skin_Dialog);

    }

    //skin item만 모아보기
    void initSkin(Dictionary<string, object> d) 
    {
        skin_Dialog.Add(d);
    }

    //make category item list on game
    void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog)   
    {
        itemBtn = (GameObject)Resources.Load("Prefebs/Customize/ItemBtn");
        print(dialog.Count);
        for(int i=0; i < dialog.Count; i++)
        {
            //create caltalog box
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
            
            //change catalog box img
            GameObject item_img= child.transform.Find("ItemImage").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Customize/Catalog_Images/"+ dialog[i][CommonField.nName] + "_catalog");
            print(dialog[i][CommonField.nName]);

            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name = child.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i][CommonField.nName].ToString();
        }
    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
