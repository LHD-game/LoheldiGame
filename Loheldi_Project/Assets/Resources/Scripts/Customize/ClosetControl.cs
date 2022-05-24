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
        PlayerLook();   // ó������ SelectCustom() ���� �ٿ��µ�, ������ �׷����ϸ� material �ν��� ���Ѵ�...
    }


    public void SaveCustom()    //���� Ŀ���͸���¡�� ������ ����
    {
        Param param = new Param();
        param.Add("Upper", NowSettings.u_upper_name);
        param.Add("UColor", NowSettings.u_upper_color);
        param.Add("Lower", NowSettings.u_lower_name);
        param.Add("LColor", NowSettings.u_lower_color);
        param.Add("Socks", NowSettings.u_socks_name);
        param.Add("SColor", NowSettings.u_socks_color);
        param.Add("Shoes", NowSettings.u_shoes_name);
        param.Add("ShColor", NowSettings.u_shoes_color);

        //���� ���� ���� ����� row �˻�
        var bro = Backend.GameData.Get("USER_CLOTHES", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        Backend.GameData.UpdateV2("USER_CLOTHES", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        NextScene();
    }

    void NextScene()
    {
        //todo: ����Ǿ��ٴ� �˾� �޽��� ����.
        SceneLoader.instance.GotoMainField();
    }

    public void SelectClothes(GameObject go) //�ǻ� ������ ���� �� ���� �޼ҵ�
    {
        //�ش� Ŀ������ itemname ��������, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;

        //data_dialog���� ������ row ã��. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName�� ������ ������ �̸��� ���� ������ db���� ã��
            {
                if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_upper)) //upper�� ���,
                {
                    NowSettings.u_upper_name = d_dialog[i][CommonField.nName].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_lower))    //lower�� ���,
                {
                    NowSettings.u_lower_name = d_dialog[i][CommonField.nName].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_socks))    //socks�� ���,
                {
                    NowSettings.u_socks_name = d_dialog[i][CommonField.nName].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_shoes))    //shoes�� ���
                {
                    NowSettings.u_shoes_name = d_dialog[i][CommonField.nName].ToString();
                }

            }
        }
    }

    public void SelectColor(GameObject go)   //�� �̸�, ������ ����
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
}
