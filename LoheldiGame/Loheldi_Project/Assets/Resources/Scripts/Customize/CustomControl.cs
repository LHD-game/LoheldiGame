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

        Param param = new Param();
        param.Add("Skin", NowSettings.u_skin_id);
        param.Add("Eyes", NowSettings.u_eyes_id);
        param.Add("EColor", NowSettings.u_eyes_color);
        param.Add("Mouth", NowSettings.u_mouth_id);
        param.Add("Hair", NowSettings.u_hair_id);
        param.Add("HColor", NowSettings.u_hair_color);

        //���� ���� ���� ����� row �˻�
        var bro = Backend.GameData.Get("USER_CUSTOM", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        Backend.GameData.UpdateV2("USER_CUSTOM", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        NextScene();
    }

    void NextScene()
    {
        if (newAcc)
        {
            SceneLoader.instance.GotoPlayerCloset();
        }
        else
        {
            SceneLoader.instance.GotoMainField();
        }
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
                if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_skin))//�װ� skin�̸�
                {
                    NowSettings.u_skin_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_eyes))
                {
                    NowSettings.u_eyes_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_mouth))    //�װ� ���̸�,
                {
                    NowSettings.u_mouth_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_hair))    //�װ� hair�̸�,
                {
                    NowSettings.u_hair_id = d_dialog[i][CommonField.nCID].ToString();
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
        }
        else if (part.Equals("hair"))
        {
            NowSettings.u_hair_color = color;
        }
        //PlayerLook(); <- �ְԵǸ� UnassignedReferenceException ������ �߻��մϴ�;; ���� update() ������ �۵��˴ϴ�.
    }
}
