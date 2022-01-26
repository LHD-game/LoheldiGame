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
            if (type == "UserFunc") ID.text = "중복된 사용자 아이디 입니다.";
            else if (type == "UserNickname") print("중복된 닉네임 입니다.");
            else if (type == "Friend") print("이미 요청되었거나 친구입니다.");
        }
        else if (errorCode == "BadUnauthorizedException")
        {
            if (type == "UserFunc") print("잘못된 사용자 아이디 혹은 비밀번호 입니다.");
            else if (type == "Message") print("잘못된 닉네임입니다.");
        }
        else if (errorCode == "UndefinedParameterException")
        {
            if (type == "UserNickname") print("닉네임을 다시 입력해주세요");
        }
        else if (errorCode == "BadParameterException")
        {
            if (type == "UserNickname") print("닉네임 앞/뒤 공백이 있거나 20자 이상입니다.");
        }
    }
}
