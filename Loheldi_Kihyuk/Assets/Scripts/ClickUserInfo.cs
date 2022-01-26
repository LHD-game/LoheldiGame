using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickUserInfo : MonoBehaviour
{
    [Header("Login & Register")]
    public Text ID;
    public Text PW;    

    [Header("User Info")]
    public InputField Nickname;
    public InputField email;

    public int Login;
    public GameObject GameManagerObject;
        
    public void ChangeUserInfo()
    {
        SceneManager.LoadScene("UserInfoScene");
        
    }
    
    public void getUserData()
    {

        Where where = new Where();
        where.Equal("id", ID.text);
        where.Equal("pw", PW.text);
        //where.Equal("email", email.text);
        var bro = Backend.GameData.Get("user", where);
        if (bro.IsSuccess())
        {
            /*JsonData jsonData = bro.GetReturnValuetoJSON();
            string id = jsonData["id"][0].ToString();
            string pw = jsonData["pw"][0].ToString();
            string email = jsonData["email"][0].ToString();*/

            ID.text = "ID:" + ID;
            PW.text = "PW:" + PW; 
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
            if (type == "UserFunc") ID.text = "�ߺ��� ����� ���̵� �Դϴ�.";
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
