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
        var bro = Backend.GameData.GetMyData("ACC_CUSTOM", new Where(), 100);
        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log("요청 성공했지만 테이블에 row가 하나도 없음");
            return;
        }

        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        int s = 0, e = 0, m = 0, h = 0;
        for (int i = rows.Count-1 ; i >= 0 ; i--)
        {
            CustomItem data = new CustomItem();
            data.ICode = bro.Rows()[i]["ICode"]["S"].ToString();
            data.IName = bro.Rows()[i]["IName"]["S"].ToString();
            data.Model = bro.Rows()[i]["Model"]["S"].ToString();
            data.Material = bro.Rows()[i]["Material"]["S"].ToString();
            data.Texture = bro.Rows()[i]["Texture"]["S"].ToString();

            CommonField.SetDataDialog(data);

            if (data.Model.Equals("Skin"))
            {
                skin_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(skin_Dialog[s], data);
                s++;
            }
            else if (data.Model.Equals("Eyes"))
            {
                eyes_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(eyes_Dialog[e], data);
                e++;
            }
            else if (data.Model.Equals("Mouth"))
            {
                mouth_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(mouth_Dialog[m], data);
                m++;
            }
            else if (data.Model.Equals("Hair"))
            {
                hair_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(hair_Dialog[h], data);
                h++;
            }
        }

        MakeCategory(c_skin, skin_Dialog);
        MakeCategory(c_eyes, eyes_Dialog);
        MakeCategory(c_mouth, mouth_Dialog);
        MakeCategory(c_hair, hair_Dialog);

    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
