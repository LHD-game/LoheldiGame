using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomControl : PlayerCustom
{
    void Update()
    {
        PlayerLook();   // 처음에는 SelectCustom() 끝에 붙였는데, 왜인지 그렇게하면 material 인식을 못한다...
    }


    public void SaveCustom()    //현재 커스터마이징을 서버에 저장
    {
        PreviousSettings.u_skin_name = NowSettings.u_skin_name;
        PreviousSettings.u_eyes_name = NowSettings.u_eyes_name;
        PreviousSettings.u_eyes_color = NowSettings.u_eyes_color;
        PreviousSettings.u_mouth_name = NowSettings.u_mouth_name;
        PreviousSettings.u_hair_name = NowSettings.u_hair_name;
        PreviousSettings.u_hair_color = NowSettings.u_hair_color;

        Param param = new Param();
        param.Add("Skin", NowSettings.u_skin_name);
        param.Add("Eyes", NowSettings.u_eyes_name);
        param.Add("EColor", NowSettings.u_eyes_color);
        param.Add("Mouth", NowSettings.u_mouth_name);
        param.Add("Hair", NowSettings.u_hair_name);
        param.Add("HColor", NowSettings.u_hair_color);

        //유저 현재 착장 저장된 row 검색
        var bro = Backend.GameData.Get("USER_CUSTOM", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //해당 row의 값을 update
        Backend.GameData.UpdateV2("USER_CUSTOM", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        SceneLoader.instance.GotoMainField();
    }


    public void SelectCustom(GameObject go) //커스텀 아이템 선택 시 실행 메소드
    {
        //해당 커스텀의 itemname 가져오고, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        
        //print(itemName + "메소드 실행 성공.");

        //data_dialog에서 아이템 row 찾기. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName과 동일한 아이템 이름을 가진 아이템 db에서 찾음
            {
                if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))//그게 skin이면
                {
                    NowSettings.u_skin_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_eyes))
                {
                    NowSettings.u_eyes_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_eyes_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_eyes_color;
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_mouth))    //그게 입이면,
                {
                    NowSettings.u_mouth_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_mouth_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_hair))    //그게 hair이면,
                {
                    NowSettings.u_hair_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_hair_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_hair_color;
                }

            }
        }
    }

    public void SelectColor(GameObject go)   //색 이름, 변경할 파츠
    {
        string color = go.transform.Find("ColorTxt").gameObject.GetComponent<Text>().text;
        string part = go.transform.Find("part").gameObject.GetComponent<Text>().text;
        
        print(color);
        print(part);
        

        if (part.Equals("eyes"))
        {
            NowSettings.u_eyes_color = color;
            NowSettings.u_eyes_texture = NowSettings.u_eyes_name + "_texture_" + NowSettings.u_eyes_color;
            //print("지금 눈"+NowSettings.u_eyes_texture);
        }
        else if (part.Equals("mouth"))
        {
            //todo
        }
        else if (part.Equals("hair"))
        {
            NowSettings.u_hair_color = color;
            NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;
            print("지금 눈"+NowSettings.u_hair_texture);
        }
        //PlayerLook(); <- 넣게되면 UnassignedReferenceException 오류가 발생합니다;; 오직 update() 에서만 작동됩니다.
    }
}
