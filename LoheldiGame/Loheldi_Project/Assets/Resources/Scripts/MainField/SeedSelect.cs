using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelect : MonoBehaviour //���� ��ư�� �޸����� �մϴ�.
{
    public Text FarmNum;

    public void Selected()  //������ �����Ѵ�. ������ �� �Թ翡 �ɰ������� �Ѵ�.
    {
        for(int i=0; i< GardenControl.empty_ground.Length; i++)
        {
            if (GardenControl.empty_ground[i])  //�� �Թ��̶��
            {
                GameObject item_code = this.transform.Find("ItemCode").gameObject;
                Text item_code_txt = item_code.GetComponent<Text>();
                string i_code = item_code_txt.text;

                if (SeedAmount(i_code)) //���� ���� 0 �̻����� üũ �� ���� ���� 1 ����
                {
                    string ground_num = "G1";
                    switch (i)
                    {
                        case 0:
                            ground_num = "G1";
                            break;
                        case 1:
                            ground_num = "G2";
                            break;
                        case 2:
                            ground_num = "G3";
                            break;
                        case 3:
                            ground_num = "G4";
                            break;
                        default:
                            break;
                    }

                    PlayerPrefs.SetString(ground_num, i_code);  //�ش��ϴ� �Թ翡, �ش��ϴ� ���� �ڵ� ����
                    Debug.Log(ground_num);
                    Debug.Log(i_code);
                    DateTime datetime = DateTime.Now;
                    PlayerPrefs.SetString(ground_num + "Time", datetime.ToString("g"));  //�ش��ϴ� �Թ翡, ���� �ð�(����ð�)�� ����

                    GardenControl.instance.GroundIsUpdated();
                    break;
                }
                else    //���� ������ 0 �����Դϴ�. todo: �˾��� ���ų� �ϱ�
                {
                    Debug.Log("���� ������ 0 �����Դϴ�.");
                }
            }
            else
            {
                //�� �Թ��� �����ϴ�. todo: �˾��� ���ų� �ϱ�
                Debug.Log("�� �Թ��� �����ϴ�.");
            }
        }
        
    }

    public void TreeSelected()  //������ �����Ѵ�. ������ �� �Թ翡 �ɰ������� �Ѵ�.
    {
        GameObject item_code = this.transform.Find("ItemCode").gameObject;
        Text item_code_txt = item_code.GetComponent<Text>();
        string i_code = item_code_txt.text;

        if (SeedAmount(i_code)) //���� ���� 0 �̻����� üũ �� ���� ���� 1 ����
        {
            PlayerPrefs.SetString("Tree", i_code);  //�ش��ϴ� �Թ翡, �ش��ϴ� ���� �ڵ� ����
            Debug.Log(i_code);

            GardenControl.instance.GroundIsUpdated();
        }
        else    //���� ������ 0 �����Դϴ�. todo: �˾��� ���ų� �ϱ�
        {
            Debug.Log("���� ������ 0 �����Դϴ�.");
        }
    }

    bool SeedAmount(string icode)
    {
        //Inventory ���̺� �ҷ��ͼ�, ���⿡ �ش��ϴ� �����۰� ��ġ�ϴ� �ڵ尡 ���� ��� ������ 1���ҽ��Ѽ� ������Ʈ
        //���� ������ ������ 0 ���Ͽ��ٸ�, return false.

        bool result = false;

        Where where = new Where();
        where.Equal("ICode", icode);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            int item_amount = (int)bro.FlattenRows()[0]["Amount"];
            if(item_amount > 0)
            {
                result = true;

                item_amount--;

                Param param = new Param();
                param.Add("ICode", icode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("INVENTORY ���̺� ������Ʈ ����.");
                    GardenCategory.instance.PopGarden();
                }
                else
                {
                    Debug.Log("INVENTORY ���̺� ������Ʈ ����.");
                }
            }
        }
        return result;  //���� ������ ���� 0 ���Ͽ��ٸ� false, 0 �ʰ������ true�� ��ȯ
    }
}
