using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store_CategoryControl : MonoBehaviour
{

    //category
    [SerializeField]
    private GameObject c_skin;
    [SerializeField]
    private GameObject c_eyes;
    [SerializeField]
    private GameObject c_mouth;
    [SerializeField]
    private GameObject c_hair;



    private void Start()
    {
        PopGaguStore();
        /*CommonField.SetDataDialog(CSVReader.Read("Customize/CustomDB"));    //DB parse

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
            else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_hair))
            {
                initHair(d_dialog[i]);
            }
        }*/
    }

    public void PopGaguStore()
    {
        GetChartContents("41919");
    }

    void GetChartContents(string chartNum)  //서버 상의 차트를 불러와 저장
    {
        Param param = new Param();
        Param updateParam = new Param();
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
        
        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();
        for (int i = 0; i < rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(rows[i]);
            print(data.itemCode);
            //todo: 아이템 테마에 따라 다른 리스트에 저장.
        }
        
        

    }

    //---init list---//
    //skin item만 모아보기
    void initSkin(Dictionary<string, object> d) 
    {
        //skin_Dialog.Add(d);
    }
    //Eyes item만 모아보기
    void initEyes(Dictionary<string, object> d)
    {
       // eyes_Dialog.Add(d);
    }
    //Mouth item만 모아보기
    void initMouth(Dictionary<string, object> d)
    {
        //mouth_Dialog.Add(d);
    }
    //Hair item만 모으기
    void initHair(Dictionary<string, object> d)
    {
       // hair_Dialog.Add(d);
    }

    //make category item list on game//
    void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog)   
    {
        /*itemBtn = (GameObject)Resources.Load("Prefebs/Customize/ItemBtn");
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
        }*/
    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
