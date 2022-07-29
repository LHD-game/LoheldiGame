using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 외형 선택 카테고리 생성 스크립트
public class CustomCategory : CategoryControl
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

    Param param = new Param();
    List<Dictionary<string, object>> skin_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> eyes_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> mouth_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> hair_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    
    private void Start()
    {
        var allCustomChart = Backend.Chart.GetChartContents(ChartNum.CustomItemChart); //서버의 엑셀파일을 불러온다.
        var myCustom = Backend.GameData.GetMyData("ACC_CUSTOM", new Where(), 100);
        if (!myCustom.IsSuccess())
        {
            Debug.Log("요청 실패");
            SceneLoader.instance.GotoPlayerCustom();
            return;
        }
        else
        {
            JsonData allCustom_rows = allCustomChart.GetReturnValuetoJSON()["rows"];
            JsonData myCustom_rows = myCustom.GetReturnValuetoJSON()["rows"];
            ParsingJSON pj = new ParsingJSON();
            ParsingJSON pj2 = new ParsingJSON();

            int s = 0, e = 0, m = 0, h = 0;
            for (int i = myCustom_rows.Count - 1; i >= 0; i--)
            {
                CustomStoreItem data = pj.ParseBackendData<CustomStoreItem>(allCustom_rows[i]);

                for (int j = 0; j < myCustom_rows.Count; j++)
                {
                    CustomStoreItem mydata = pj2.ParseBackendData<CustomStoreItem>(myCustom_rows[j]);
                    if (data.ICode.Equals(mydata.ICode))
                    {
                        CommonField.SetDataDialog(data);
                        if (data.Category.Equals(CommonField.m_skin))
                        {
                            skin_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(skin_Dialog[s], data);
                            s++;
                        }
                        else if (data.Category.Equals(CommonField.m_eyes))
                        {
                            eyes_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(eyes_Dialog[e], data);
                            e++;
                        }
                        else if (data.Category.Equals(CommonField.m_mouth))
                        {
                            mouth_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(mouth_Dialog[m], data);
                            m++;
                        }
                        else if (data.Category.Equals(CommonField.m_hair))
                        {
                            hair_Dialog.Add(new Dictionary<string, object>());
                            initCustomItem(hair_Dialog[h], data);
                            h++;
                        }
                    }
                }
            }

            MakeCategory(c_skin, skin_Dialog);
            MakeCategory(c_eyes, eyes_Dialog);
            MakeCategory(c_mouth, mouth_Dialog);
            MakeCategory(c_hair, hair_Dialog);

        }
    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
