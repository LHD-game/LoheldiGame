using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickUserInfo : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField ID;
    public InputField PW;
    public Text userID;
    public Text userPW;

    [Header("User Info")]
    public InputField Nickname;
    public InputField email;
        
    public void ChangeUserInfo()
    {
        SceneManager.LoadScene("UserInfoScene");
        
    }
    public void Login()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(ID.text, PW.text);

        if (BRO.IsSuccess())
        {
            print("������ �α��� ����");

        }

        else Error(BRO.GetErrorCode(), "UserFunc");

    }

    public void getUserData()
    {
        Where where = new Where();
        
        var bro = Backend.GameData.Get("user", where);
        where.Equal("id", userID);
        where.Equal("pw", userPW);

        //where.Equal("email", email.text);

        if (bro.IsSuccess())
        {
            /*JsonData jsonData = bro.GetReturnValuetoJSON();
            string id = jsonData["id"][0].ToString();
            string pw = jsonData["pw"][0].ToString();
            string email = jsonData["email"][0].ToString();*/

            userID.text = "ID:" + userID.text;
            userPW.text = "PW:" + userPW.text; 
            //print("Email:" + email.text);
        }
        else Error(bro.GetErrorCode(), "gameData");
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") userID.text = "�ߺ��� ����� ���̵� �Դϴ�.";
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
        }
    }
}
