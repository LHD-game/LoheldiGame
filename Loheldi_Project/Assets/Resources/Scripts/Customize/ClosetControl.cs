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
                if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_upper)) //upper�� ���,
                {
                    NowSettings.u_upper_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_lower))    //lower�� ���,
                {
                    NowSettings.u_lower_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nCategory].ToString().Equals(CommonField.m_socks))    //socks�� ���,
                {
                    NowSettings.u_socks_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals("shoes"))    //shoes�� ���
                {
                    NowSettings.u_shoes_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_hat))    //hat�� ���
                {
                    NowSettings.u_hat_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_glasses))    //glasses�� ���
                {
                    NowSettings.u_glasses_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else if (d_dialog[i][CommonField.nItemType].ToString().Equals(CommonField.it_bag))    //bag�� ���
                {
                    NowSettings.u_bag_id = d_dialog[i][CommonField.nCID].ToString();
                }
                else //�̱� ������: ����
                {
                    if (d_dialog[i][CommonField.nItemType].ToString().Equals("upper"))
                    {
                        NowSettings.u_upper_id = d_dialog[i][CommonField.nCID].ToString();
                    }
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

    //�Ǽ����� ���� �� �� ��ư
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
