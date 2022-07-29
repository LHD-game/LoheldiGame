using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 옷 선택 카테고리 생성 스크립트
public class ClosetCategory : CategoryControl
{
    private int itemnum;
    
    //category
    [SerializeField]
    private GameObject c_upper;
    [SerializeField]
    private GameObject c_lower;
    [SerializeField]
    private GameObject c_socks;
    [SerializeField]
    private GameObject c_shoes;
    [SerializeField]
    private GameObject c_hat;
    [SerializeField]
    private GameObject c_glasses;
    [SerializeField]
    private GameObject c_bag;

    Param param = new Param();
    List<Dictionary<string, object>> upper_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> lower_Dialog = new List<Dictionary<string, object>>();   
    List<Dictionary<string, object>> socks_Dialog = new List<Dictionary<string, object>>();   
    List<Dictionary<string, object>> shoes_Dialog = new List<Dictionary<string, object>>();  
    List<Dictionary<string, object>> hat_Dialog = new List<Dictionary<string, object>>();   
    List<Dictionary<string, object>> glasses_Dialog = new List<Dictionary<string, object>>();   
    List<Dictionary<string, object>> bag_Dialog = new List<Dictionary<string, object>>();   
    
    private void Start()
    {
        var allClothesChart = Backend.Chart.GetChartContents(ChartNum.ClothesItemChart); //서버의 엑셀파일을 불러온다.
        var myClothes = Backend.GameData.GetMyData("ACC_CLOSET", new Where(), 100);
        if (myClothes.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            SceneLoader.instance.GotoPlayerCloset();
            return;
        }
        else
        {
            JsonData allClothes_rows = allClothesChart.GetReturnValuetoJSON()["rows"];
            JsonData myClothes_rows = myClothes.GetReturnValuetoJSON()["rows"];
            ParsingJSON pj = new ParsingJSON();
            ParsingJSON pj2 = new ParsingJSON();

            int u = 0, l = 0, so = 0, sh = 0, h = 0, g = 0, b = 0;
            for (int i = 0; i < allClothes_rows.Count; i++)
            {
                CustomStoreItem data = pj.ParseBackendData<CustomStoreItem>(allClothes_rows[i]);
                for (int j = 0; j < myClothes_rows.Count; j++)
                {
                    CustomStoreItem mydata = pj2.ParseBackendData<CustomStoreItem>(myClothes_rows[j]);
                    if (data.ICode.Equals(mydata.ICode))
                    {
                        CommonField.SetDataDialog(data);
                        if (data.Category.Equals(CommonField.m_upper))
                        {
                            upper_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(upper_Dialog[u], data);
                            u++;
                        }
                        else if (data.Category.Equals(CommonField.m_lower))
                        {
                            lower_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(lower_Dialog[l], data);
                            l++;
                        }
                        else if (data.Category.Equals(CommonField.m_socks))
                        {
                            socks_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(socks_Dialog[so], data);
                            so++;
                        }
                        else if (data.Category.Equals(CommonField.m_shoes))
                        {
                            shoes_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(shoes_Dialog[sh], data);
                            sh++;
                        }
                        else if (data.ItemType.Equals(CommonField.it_hat))
                        {
                            hat_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(hat_Dialog[h], data);
                            h++;
                        }
                        else if (data.ItemType.Equals(CommonField.it_glasses))
                        {
                            glasses_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(glasses_Dialog[g], data);
                            g++;
                        }
                        else if (data.ItemType.Equals(CommonField.it_bag))
                        {
                            bag_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(bag_Dialog[b], data);
                            b++;
                        }
                    }

                }
            }

            string scene = "Closet";

            MakeCategory(c_upper, upper_Dialog, scene);
            MakeCategory(c_lower, lower_Dialog, scene);
            MakeCategory(c_socks, socks_Dialog, scene);
            MakeCategory(c_shoes, shoes_Dialog, scene);
            MakeCategory(c_hat, hat_Dialog, scene);
            MakeCategory(c_glasses, glasses_Dialog, scene);
            MakeCategory(c_bag, bag_Dialog, scene);

        }

    }


    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
