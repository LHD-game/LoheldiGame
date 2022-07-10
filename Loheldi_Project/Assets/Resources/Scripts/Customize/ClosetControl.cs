using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetControl : PlayerCloset
{
    void Update()
    {
        PlayerLook();   // 처음에는 SelectCustom() 끝에 붙였는데, 왜인지 그렇게하면 material 인식을 못한다...
    }


    public void SaveCustom()    //현재 커스터마이징을 서버에 저장
    {
        Param param = new Param();
        param.Add("Upper", NowSettings.u_upper_id);
        param.Add("UColor", NowSettings.u_upper_color);
        param.Add("Lower", NowSettings.u_lower_id);
        param.Add("LColor", NowSettings.u_lower_color);
        param.Add("Socks", NowSettings.u_socks_id);
        param.Add("SColor", NowSettings.u_socks_color);
        param.Add("Shoes", NowSettings.u_shoes_id);
        param.Add("ShColor", NowSettings.u_shoes_color);
        param.Add("Hat", NowSettings.u_hat_id);
        param.Add("Glasses", NowSettings.u_glasses_id);
        param.Add("Bag", NowSettings.u_bag_id);

        //유저 현재 착장 저장된 row 검색
        var bro = Backend.GameData.Get("USER_CLOTHES", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //해당 row의 값을 update
        Backend.GameData.UpdateV2("USER_CLOTHES", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        NextScene();
    }

    void NextScene()
    {
        //todo: 변경되었다는 팝업 메시지 띄우기.
        SceneLoader.instance.GotoMainField();
    }

    public void SelectClothes(GameObject go) //의상 아이템 선택 시 실행 메소드
    {
        //해당 커스텀의 itemname 가져오고, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;

        //data_dialog에서 아이템 row 찾기. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName과 동일한 아이템 이름을 가진 아이템 db에서 찾음
            {
                if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_upper)) //upper일 경우,
                {
                    NowSettings.u_upper_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_lower))    //lower일 경우,
                {
                    NowSettings.u_lower_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_socks))    //socks일 경우,
                {
                    NowSettings.u_socks_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_shoes))    //shoes일 경우
                {
                    NowSettings.u_shoes_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_hat))    //hat일 경우
                {
                    NowSettings.u_hat_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_glasses))    //glasses일 경우
                {
                    NowSettings.u_glasses_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_bag))    //bag일 경우
                {
                    NowSettings.u_bag_id = d_dialog[i][CommonField.nCID].ToString();
                }

            }
        }
    }

    public void SelectColor(GameObject go)   //색 이름, 변경할 파츠
    {
        string color = go.transform.Find("ColorTxt").gameObject.GetComponent<Text>().text;
        string part = go.transform.Find("part").gameObject.GetComponent<Text>().text;

        if (part.Equals("upper"))
        {
            NowSettings.u_upper_color = color;
        }
        else if (part.Equals("lower"))
        {
            NowSettings.u_lower_color = color;
        }
        else if (part.Equals("socks"))
        {
            NowSettings.u_socks_color = color;
        }
        else if (part.Equals("shoes"))
        {
            NowSettings.u_shoes_color = color;
        }
    }

    //악세서리 선택 안 함 버튼
    public void selectAccNull(string item_type)
    {
        if (item_type.Equals(CommonField.it_hat))   //item_type = hat
        {
            NowSettings.u_hat_id = "null";
        }
        else if (item_type.Equals(CommonField.it_glasses))  //item_type = glasses
        {
            NowSettings.u_glasses_id = "null";
        }
        else if (item_type.Equals(CommonField.it_bag))  //item_type = bag
        {
            NowSettings.u_bag_id = "null";
        }
    }

}
