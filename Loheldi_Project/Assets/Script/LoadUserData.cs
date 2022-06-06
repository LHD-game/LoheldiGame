using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class LoadUserData : MonoBehaviour
{
    public Text usernick;
    public Text userbirth;
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.GameData.GetMyData("ACC_INFO", new Where(), 10);
        if (bro.IsSuccess() == false)
        {
            // ��û ���� ó��
            Debug.Log("load failed");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // ��û�� �����ص� where ���ǿ� �����ϴ� �����Ͱ� ���� �� �ֱ� ������
            // �����Ͱ� �����ϴ��� Ȯ��
            // ���� ���� new Where() ������ ��� ���̺� row�� �ϳ��� ������ Count�� 0 ���� �� �� �ִ�.
            Debug.Log(bro);
            return;
        }
        // �˻��� �������� ��� row�� inDate �� Ȯ��
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            string nick = bro.Rows()[i]["NICKNAME"]["S"].ToString();
            string birth = bro.Rows()[i]["BIRTH"]["S"].ToString();
            Debug.Log(nick);
            Debug.Log(birth);
            usernick.text = nick;
            userbirth.text = birth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
