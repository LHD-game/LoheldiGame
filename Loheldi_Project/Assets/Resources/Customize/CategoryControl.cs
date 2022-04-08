using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryControl : MonoBehaviour
{
    public GameObject SkinPanel;
    public GameObject EyesPanel;
    public GameObject MouthPanel;
    public GameObject HairPanel;
    GameObject itemBtn;
    private int itemnum;
    
    //category
    [SerializeField]
    private GameObject c_skin;
    [SerializeField]
    private GameObject c_eyes;
    [SerializeField]
    private GameObject c_mouth;
    [SerializeField]
    private GameObject c_hair;

    public int Category;
    public int buttonnum;
    Param param = new Param();
    List<Dictionary<string, object>> skin_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> eyes_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> mouth_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture


    
    Dictionary<string, string> B_custom = new Dictionary<string, string>
    {
        {"Skin1", "skin1" },
        {"Skin2", "skin2" },
        {"Skin3", "skin3" },
        {"Skin4", "skin4" },
        {"Skin5", "skin5" },
        {"Eyes1", "eyes1" },
        {"Eyes2", "eyes2" },
        {"Eyes3", "eyes3" },
        {"MouthI", "mouthi" },
        {"MouthD", "mouthd" },
        {"MouthW", "mouthw" },
    };


    private void Start()
    {
        CommonField.SetDataDialog(CSVReader.Read("Customize/CustomDB"));    //DB parse

        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();

        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))  //if it's skin
            {
                initSkin(d_dialog[i]);
            }
            else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_eyes)) //if it's eyes
            {
                initEyes(d_dialog[i]);
            }
            else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_mouth)) 
            {
                initMouth(d_dialog[i]);
            }
        }
        MakeCategory(c_skin, skin_Dialog);
        MakeCategory(c_eyes, eyes_Dialog);
        MakeCategory(c_mouth, mouth_Dialog);

    }

    public void NowCustom2()
    {
        
        
        if (Category == 1)
        {
            if (buttonnum == 1)
            {
                string str = "skin1";
                print(str);
                param.Add("skin", B_custom[str]);
            }
            else if (buttonnum == 2)
            {
                B_custom.TryGetValue("Skin2", out string skin);
                print(skin);
                param.Add("skin", skin);
            }
            else if (buttonnum == 3)
            {
                B_custom.TryGetValue("Skin3", out string skin);
                print(skin);
                param.Add("skin", skin);
            }
            else if (buttonnum == 4)
            {
                B_custom.TryGetValue("Skin4", out string skin);
                print(skin);
                param.Add("skin", skin);
            }
            else if (buttonnum == 5)
            {
                B_custom.TryGetValue("Skin5", out string skin);
                print(skin);
                param.Add("skin", skin);
            }
            

            
        }
        if(Category == 2)
        {
            if (buttonnum == 1)
            {
                string str = "Eyes1";
                print(str);
                param.Add("eyes", B_custom[str]);
            }
            if (buttonnum == 1)
            {
                string str = "Eyes2";
                print(str);
                param.Add("eyes", B_custom[str]);
            }
            if (buttonnum == 1)
            {
                string str = "Eyes3";
                print(str);
                param.Add("eyes", B_custom[str]);
            }
        }
        if(Category == 3)
        {
            if (buttonnum == 1)
            {
                B_custom.TryGetValue("MouthI", out string mouth);
                print(mouth);
                param.Add("mouth", mouth);
            }
            if (buttonnum == 2)
            {
                B_custom.TryGetValue("MouthD", out string mouth);
                print(mouth);
                param.Add("mouth", mouth);
            }
            if (buttonnum == 3)
            {
                B_custom.TryGetValue("MouthW", out string mouth);
                print(mouth);
                param.Add("mouth", mouth);
            }
        }


        Backend.GameData.Insert("USER_CUSTOM", param);


        /*var bro = Backend.Chart.GetChartContents("45823");
        
        if (Category == 1)
        {
            itemnum = buttonnum + 4;
        }
        if (Category == 2)
        {
            itemnum =  buttonnum + 9;
        }
        if (Category == 3)
        {
            itemnum =  buttonnum + 14; 
        }

        if (bro.IsSuccess())
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            
            param.Add("Skin", rows[itemnum]["Name"][0]);
            param.Add("Eyes", rows[itemnum]["Name"][0]);
            param.Add("Mouth", rows[itemnum]["Name"][0]);
            *//*param.Add("Model", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Model"][0][0][0]);
            param.Add("Meterial", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Meterial"][0][0][0]);
            param.Add("Texture", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Texture"][0][0][0]);*//*
            Backend.GameData.Insert("USER_CUSTOM", param);
        }*/

    }
    
        
    
    public void Cate_Skin()
    {
        Category = 1;
        
    }
    public void Cate_Eyes()
    {
        Category = 2;
        
    }
    public void Cate_Mouth()
    {
        Category = 3;
        
    }
    public void Cate_Hair()
    {
        Category = 4;
        
    }
    public void btn1()
    {
        buttonnum = 1;
    }
    public void btn2()
    {
        buttonnum = 2;
    }
    public void btn3()
    {
        buttonnum = 3;
    }
    public void btn4()
    {
        buttonnum = 4;
    }
    public void btn5()
    {
        buttonnum = 5;
    }
    /*public void NowCustom()
    {
        Param param = new Param();
        param.Add("Skin", skin_Dialog);
        param.Add("Eye", eyes_Dialog);
        param.Add("Mouth", mouth_Dialog);
        //param.Add("Hair", c_hair);

        Backend.GameData.Insert("USER_CUSTOM", param);
    }*/

    //---init list---//
    //skin item만 모아보기
    void initSkin(Dictionary<string, object> d) 
    {
        skin_Dialog.Add(d);
    }
    //Eyes item만 모아보기
    void initEyes(Dictionary<string, object> d)
    {
        eyes_Dialog.Add(d);
    }
    //Mouth item만 모아보기
    void initMouth(Dictionary<string, object> d)
    {
        mouth_Dialog.Add(d);
    }

    //make category item list on game//
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
