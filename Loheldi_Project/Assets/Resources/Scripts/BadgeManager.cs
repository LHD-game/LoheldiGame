using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    //���� ���� �޼ҵ�
    public static void GetBadge(string b_code)
    {
        SaveAccBadge(b_code);
    }

    //���� �� ACC_BADGE�� �����ϴ� �޼ҵ�
    static void SaveAccBadge(string b_code)
    {
        Where where = new Where();
        where.Equal("BCode", b_code);
        var bro = Backend.GameData.GetMyData("ACC_BADGE", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //���� ��� ������ �� �߰�
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("BCode", b_code);

                var insert_bro = Backend.GameData.Insert("ACC_BADGE", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("���� ���� �Ϸ�");
                }
                else
                {
                    Debug.Log("���� ���� ����");
                }
            }
            else
            {
                //�̹� �ش� ������ ������ ����
                return;
            }


        }
    }
}
