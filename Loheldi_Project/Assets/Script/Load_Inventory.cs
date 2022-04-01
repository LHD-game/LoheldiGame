using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Inventory : MonoBehaviour
{
    private int itemnum;
    public void GetMyItem()
    {
       /* Where where = new Where();
        where.Equal("item", "����");  �÷��� �ҷ��ͺ����� �õ��� ����*/
        /*where.BeginsWith("item", "����");
        where.BeginsWith("item", "�����");*/

        var bro = Backend.GameData.GetMyData("INVENTORY", new Where(), 10);
        
        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // ��û�� �����ص� where ���ǿ� �����ϴ� �����Ͱ� ���� �� �ֱ� ������
            // �����Ͱ� �����ϴ��� Ȯ��
            // ���� ���� new Where() ������ ��� ���̺� row�� �ϳ��� ������ Count�� 0 ���� �� �� �ִ�.
            Debug.Log("��û ���������� ���̺� row�� �ϳ��� ����");
            return;
        }
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            var inDate = bro.Rows()[i]["itemCode"]["S"].ToString();
            Debug.Log(inDate);
        } /*�̰Ŵ� ���� ���� ��� ������ ��ȸ�ϴ� itemCode �ڵ� int ������ �� �ҷ������µ� string���� �ҷ����� ������ json �ѹ� �� ����ҵ�.*/
        
        /*string item = bro.Rows()[0]["item"]["S"].ToString();
        Debug.Log(item);*/
        




    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
