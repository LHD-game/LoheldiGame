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
        PlayerLook();   // ó������ SelectCustom() ���� �ٿ��µ�, ������ �׷����ϸ� material �ν��� ���Ѵ�...
    }


    public void SaveCustom()    //���� Ŀ���͸���¡�� ������ ����
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

        //���� ���� ���� ����� row �˻�
        var bro = Backend.GameData.Get("USER_CUSTOM", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        Backend.GameData.UpdateV2("USER_CUSTOM", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        SceneLoader.instance.GotoMainField();
    }


    public void SelectCustom(GameObject go) //Ŀ���� ������ ���� �� ���� �޼ҵ�
    {
        //�ش� Ŀ������ itemname ��������, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        
        //print(itemName + "�޼ҵ� ���� ����.");

        //data_dialog���� ������ row ã��. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName�� ������ ������ �̸��� ���� ������ db���� ã��
            {
                if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))//�װ� skin�̸�
                {
                    NowSettings.u_skin_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_eyes))
                {
                    NowSettings.u_eyes_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_eyes_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_eyes_color;
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_mouth))    //�װ� ���̸�,
                {
                    NowSettings.u_mouth_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_mouth_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_hair))    //�װ� hair�̸�,
                {
                    NowSettings.u_hair_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_hair_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_hair_color;
                }

            }
        }
    }

    public void SelectColor(GameObject go)   //�� �̸�, ������ ����
    {
        string color = go.transform.Find("ColorTxt").gameObject.GetComponent<Text>().text;
        string part = go.transform.Find("part").gameObject.GetComponent<Text>().text;
        
        print(color);
        print(part);
        

        if (part.Equals("eyes"))
        {
            NowSettings.u_eyes_color = color;
            NowSettings.u_eyes_texture = NowSettings.u_eyes_name + "_texture_" + NowSettings.u_eyes_color;
            //print("���� ��"+NowSettings.u_eyes_texture);
        }
        else if (part.Equals("mouth"))
        {
            //todo
        }
        else if (part.Equals("hair"))
        {
            NowSettings.u_hair_color = color;
            NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;
            print("���� ��"+NowSettings.u_hair_texture);
        }
        //PlayerLook(); <- �ְԵǸ� UnassignedReferenceException ������ �߻��մϴ�;; ���� update() ������ �۵��˴ϴ�.
    }
}
