using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;

public class BackendInfo : MonoBehaviour
{
    Dictionary<string, string> charset = new Dictionary<string, string>
        {
            { "Level" , "1" },
            { "Coin", "2000"},
            { "Exp", "150" }
        }; 
    
        // ���� Dictionary �� ����ϱ�
    /*Dictionary<string, int> equipment = new Dictionary<string, int>
        {
            { "weapon", 123 },
            { "armor", 111 },
            { "helmet", 1345 }
        };*/
    // Insert �� '����' �۾��� �ַ� ���ȴ�. 
    /*public void OnClickInsertData()
    {

        *//*int charLevel = Random.Range(0, 99);
        int charExp = Random.Range(0, 9999);*//*
        //int charScore = Random.Range(0, 99999);

        // Param�� �ڳ� ������ ����� �� �� �Ѱ��ִ� �Ķ���� Ŭ���� �Դϴ�. 
        Param param = new Param();
        param.Add("lv", 100);
        param.Add("exp", 30);
        param.Add("equipItem", equipment);

        BackendReturnObject BRO = Backend.GameInfo.Insert("character", param);


        if (BRO.IsSuccess())
        {
            print("���� ��� ������ �Է� ����");
        }
        else Error(BRO.GetErrorCode(), "gameData");

    }*/
    //�񵿱� ���� ����
    /*public void OnClickInsertData()
    {
        Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");

        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "dic1", 1 },
            { "dic4", 2 },
            { "dic2", 4 }
        };

        Dictionary<string, string> dic2 = new Dictionary<string, string>
        {       
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        string[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };

        Param param = new Param();
        param.Add("�̸�", "cheolsu");
        param.Add("score", 99);
        param.Add("lunch", lunch);
        param.Add("dic_num", dic);
        param.Add("dic_string", dic2);
        param.Add("list_string", list);
        param.Add("list_num", list2);

        BackendAsyncClass.BackendAsync(Backend.GameInfo.Insert, "character", param, (callback) =>
        {
            if (callback.IsSuccess())
            {
                print("�񵿱� ��� ������ ���� ����");
            }
            else Error(callback.GetErrorCode(), "gameData");
        });
    }*/
    public void AddUserData()
    {
        /*Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");*/

        

        /*Dictionary<string, string> dic2 = new Dictionary<string, string>
        {
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        string[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };*/

        Param param = new Param();
        param.Add("�̸�", "cheolsu");
        param.Add("charset", charset);
        
        BackendReturnObject BRO = Backend.GameData.Insert("character", param);

        if (BRO.IsSuccess())
        {
            print("���� ��� ������ �Է� ����");
        }
        else Error(BRO.GetErrorCode(), "gameData");
    }
    // Public ���̺� ����� ������ �ҷ�����
    public void OnClickPublicContents()
    {
        var bro = Backend.GameData.GetMyData("character", new Where(), 10);
        // default limit �� 10���̴�. �Ʒ� �ڵ�� 10���� �����͸� �����´�.
        // BackendReturnObject BRO = Backend.GameInfo.GetPublicContents("custom");

        if (bro.IsSuccess())
        {
            bro.GetReturnValuetoJSON();
            bool l = charset.TryGetValue("Level", out string testValue);
            {
                print("Level:" + testValue);
            };
            bool c = charset.TryGetValue("Coin", out string testValue2);
            {
                print("Coin:" + testValue2);
            };
            bool e = charset.TryGetValue("Exp", out string testValue3);
            {
                print("Exp:" + testValue3);
            };
            /* print($"level: {charset.TryGetValue("Level", out string testValue)}"){
                 Debug.Log("level: " + testValue);
             };*/
           /* print($"coin: {charset}"[1]);
            print($"exp: {charset}"[2]);*/
        }

        else
        {
            // ���� üũ
            CheckError(bro);
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
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
    }
    



    /*public void readData()
    {
        var bro = Backend.GameData.GetMyData("character", new Where(), 10);

        if (bro.IsSuccess() == false)
        {
            // ��û ���� ó��
            Debug.Log(bro);
            return;

            //print($"level: {level} coin: {coin} exp: {exp}");            
        }
        else
        {
            bro.GetReturnValuetoJSON();
            print($"level: {charset}");
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
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
        
    }*/

    /*public void readDataAsync()
    {
        BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "character", (callback) =>
        {
            if (callback.IsSuccess())
            {
                JsonData jsonData = callback.GetReturnValuetoJSON();
                string level = jsonData["level"][0].ToString();
                string coin = jsonData["coin"][0].ToString();
                string exp = jsonData["exp"][0].ToString();

                //dataIndate = jsonData["inDate"][0].ToString();

                print($"Level : {level} Coin : {coin} Exp : {exp}");
                print("�񵿱� ��� ���� �б� �Ϸ�.");
            }
            else Error(callback.GetErrorCode(), "ReadData");
        });
        *//*BackendReturnObject BRO = Backend.GameInfo.GetPrivateContents("character");

        if (BRO.IsSuccess())
        {
            JsonData jsonData = BRO.GetReturnValuetoJSON()["rows"][0];
            string level = jsonData["level"][0].ToString();
            string coin = jsonData["coin"][0].ToString();
            string exp = jsonData["exp"][0].ToString();
            string dataIndate = jsonData["inDate"][0].ToString();
            print($"Level : {level} Coin : {coin} Exp : {exp}");
            print("���� ��� ���� �б� �Ϸ�");
        }
        else Error(BRO.GetErrorCode(), "ReadData");*/
    /*string[] select = { "owner_inDate"};

    //�ڽ��� owner_inDate
    string owner_inDate = Backend.UserInDate;
    // Ÿ���� ower_inDate
    //string owner_inDate = "2018-07-06T05:22:41.000Z";

    // ���̺� �� �ش� rowIndate�� ���� row�� ��ȸ
    var bro = Backend.GameData.Get("character", owner_inDate, select);
    Debug.Log(bro);

    // ���̺� �� �ش� rowIndate�� ���� row�� ��ȸ
    // select�� �����ϴ� �÷��� ����
    bro = Backend.GameData.Get("character", "rowIndate", select);
    Debug.Log(bro);*//*
}*/

    // ���� �ڵ� Ȯ��
    void CheckError(BackendReturnObject BRO)
    {
        switch (BRO.GetStatusCode())
        {
            case "200":
                Debug.Log("�ش� ������ �����Ͱ� ���̺� �����ϴ�.");
                break;

            case "404":
                if (BRO.GetMessage().Contains("gamer not found"))
                {
                    Debug.Log("gamerIndate�� �������� gamer�� indate�� ���");
                }
                else if (BRO.GetMessage().Contains("table not found"))
                {
                    Debug.Log("�������� �ʴ� ���̺�");
                }
                break;

            case "400":
                if (BRO.GetMessage().Contains("bad limit"))
                {
                    Debug.Log("limit ���� 100�̻��� ���");
                }

                else if (BRO.GetMessage().Contains("bad table"))
                {
                    // public Table ������ ��� �ڵ�� private Table �� �������� �� �Ǵ�
                    // private Table ������ ��� �ڵ�� public Table �� �������� �� 
                    Debug.Log("��û�� �ڵ�� ���̺��� �������ΰ� ���� �ʽ��ϴ�.");
                }
                break;

            case "412":
                Debug.Log("��Ȱ��ȭ�� ���̺��Դϴ�.");
                break;

            default:
                Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
                break;

        }
    }





    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") print("�ߺ��� ����� ���̵� �Դϴ�.");
            else if (type == "UserNickname") print("�ߺ��� �г��� �Դϴ�.");
            else if (type == "Friend") print("�̹� ��û�Ǿ��ų� ģ���Դϴ�.");
        }
        else if (errorCode == "BadUnauthorizedException")
        {
            if (type == "UserFunc") print("�߸��� ����� ���̵� Ȥ�� ��й�ȣ �Դϴ�.");
            else if (type == "Message") print("�߸��� �г����Դϴ�.");
        }
        else if (errorCode == "UndefinedParameterException")
        {
            if (type == "UserNickname") print("�г����� �ٽ� �Է����ּ���");
        }
        else if (errorCode == "BadParameterException")
        {
            if (type == "UserNickname") print("�г��� ��/�� ������ �ְų� 20�� �̻��Դϴ�.");
            else if (type == "UserPW") print("�߸��� �̸����Դϴ�.");
            else if (type == "gameData") print("�߸��� ������ ���̺� �Դϴ�.");
            else if (type == "Message") print("������ ���, �޴� ����� �����ϴ�.");
        }
        else if (errorCode == "NotFoundException")
        {
            if (type == "UserPW") print("��ϵ� �̸����� �����ϴ�.");
            else if (type == "Coupon") print("�ߺ� ����̰ų� �Ⱓ�� ����� �����Դϴ�.");
            else if (type == "gameData") print("�ش� ���̺��� ã�� �� �����ϴ�.");
        }
        else if (errorCode == "Too Many Request")
        {
            if (type == "UserPW") print("��û Ƚ���� �ʰ��Ͽ����ϴ�. (1�� 5ȸ)");
        }
        else if (errorCode == "PreconditionFailed")
        {
            if (type == "gameData") print("�ش� ���̺��� ��Ȱ��ȭ �� ���̺� �Դϴ�.");
            else if (type == "Friend") print("�޴� ��� Ȥ�� ������ ����� ��û������ �� �� �����Դϴ�.");
            else if (type == "Message") print("������ ���ڼ��� �ʰ��Ͽ����ϴ�.");
        }
        else if (errorCode == "ServerErrorException")
        {
            if (type == "gameData") print("�ϳ��� row�� 400KB�� �ѽ��ϴ�");
        }
        else if (errorCode == "ForbiddenError")
        {
            if (type == "gameData") print("Ÿ���� ������ ������ �Ұ����մϴ�.");
            else if (type == "Message") print("�ֿܼ��� ���� �ִ뺸������ �������ּ���");
        }
        else if (errorCode == "MethodNotAllowedParameterException")
        {
            if (type == "Message") print("������ ������ ���� ã���ϴ�.");
        }
    }
}

    
