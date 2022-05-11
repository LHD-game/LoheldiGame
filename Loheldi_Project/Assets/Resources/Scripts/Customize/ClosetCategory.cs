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

    Param param = new Param();
    List<Dictionary<string, object>> upper_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> lower_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> socks_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    List<Dictionary<string, object>> shoes_Dialog = new List<Dictionary<string, object>>();   // cid, name, model, meterial, texture
    
    private void Start()
    {
        var bro = Backend.GameData.GetMyData("ACC_CLOSET", new Where(), 100);
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

        int u = 0, l = 0, so = 0, sh = 0;
        for (int i = rows.Count-1 ; i >= 0 ; i--)
        {
            CustomItem data = new CustomItem();
            data.ICode = bro.Rows()[i]["ICode"]["S"].ToString();
            data.IName = bro.Rows()[i]["IName"]["S"].ToString();
            data.Model = bro.Rows()[i]["Model"]["S"].ToString();
            data.Material = bro.Rows()[i]["Material"]["S"].ToString();
            data.Texture = bro.Rows()[i]["Texture"]["S"].ToString();

            CommonField.SetDataDialog(data);

            if (data.Model.Equals(CommonField.m_upper))
            {
                upper_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(upper_Dialog[u], data);
                u++;
            }
            else if (data.Model.Equals(CommonField.m_lower))
            {
                lower_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(lower_Dialog[l], data);
                l++;
            }
            else if (data.Model.Equals(CommonField.m_socks))
            {
                socks_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(socks_Dialog[so], data);
                so++;
            }
            else if (data.Model.Equals(CommonField.m_shoes))
            {
                shoes_Dialog.Add(new Dictionary<string, object>());
                initCustomItem(shoes_Dialog[sh], data);
                sh++;
            }
        }

        MakeCategory(c_upper, upper_Dialog);
        MakeCategory(c_lower, lower_Dialog);
        MakeCategory(c_socks, socks_Dialog);
        MakeCategory(c_shoes, shoes_Dialog);

    }

    //todo: 선택된 커스텀(nowsettings)에는 선택된 표시를 해줄 것 --> setActive이용
}
