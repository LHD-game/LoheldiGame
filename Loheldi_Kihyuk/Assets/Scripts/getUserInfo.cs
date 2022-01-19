using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getUserInfo : MonoBehaviour
{
    [Header("User Info")]
    public Text userInfo;
    public void readData()
    {
        BackendReturnObject BRO = Backend.BMember.GetUserInfo();

        if (BRO.IsSuccess())
        {
            Debug.Log(BRO.GetReturnValue());
            /*JsonData jsonData = BRO.GetReturnValuetoJSON()["rows"][0];
            string ID = jsonData["id"][0].ToString();
            string PW = jsonData["pw"][0].ToString();
            string Email = jsonData["email"][0].ToString();
            string Nickname = jsonData["nickname"][0].ToString();*/

            //dataIndate = jsonData["inDate"][0].ToString();

            /*print($"ID: {ID}  PW: {PW}  Email : {Email}  Nickname: {Nickname}");
            print("���� ��� ���� �б� �Ϸ�");*/

        }
        else Error(BRO.GetErrorCode(), "ReadData");
    }
    void Error(string errorCode, string type)
    {
        if (errorCode == "ReadData") print("�������� �ʴ� ���̺� �Դϴ�.");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
