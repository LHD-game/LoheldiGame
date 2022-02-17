using BackEnd;
using LitJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField InputName;
    public InputField InputID;
    public InputField InputPW;
    //public InputField Nickname;
    public InputField InputEmail;
    public Text CheckID;
    //public Text CheckNick;

    [Header("User Info")]

    public Text userNick;
    public Text userID;
    public Text userPW;
    public Text userEmail;
    public void UserRegister()

    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(InputID.text, InputPW.text);
        Param param = new Param();
        param.Add("id", InputID.text);
        param.Add("pw", InputPW.text);
        param.Add("name", InputName.text);
        param.Add("email", InputEmail.text);

        var bro = Backend.GameData.Insert("user", param);

        if (bro.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }

        else Error(bro.GetErrorCode(), "gamedata");

        if (BRO.IsSuccess())
        {
            print("동기방식 회원가입 성공");
        }
        else CheckID.text = "중복된 아이디 입니다.";

    }
    public void Login()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(InputID.text, InputPW.text);

        if (BRO.IsSuccess())
        {
            print("동기방식 로그인 성공");

        }

        else Error(BRO.GetErrorCode(), "UserFunc");

    }

    public void CreateEmail()
    {
        BackendReturnObject BRO = Backend.BMember.UpdateCustomEmail(InputEmail.text);
      
        if (BRO.IsSuccess()) print("동기 방식 이메일 등록 완료");
               
    }
    public void Save()
    {
        PlayerPrefs.SetString("ID", InputID.text);
        PlayerPrefs.SetString("PW", InputPW.text);
        PlayerPrefs.SetString("Email", InputEmail.text);
    }
    public void Road()
    {
        if (PlayerPrefs.HasKey("ID"))
        {
            userID.text = PlayerPrefs.GetString("ID");
            userPW.text = PlayerPrefs.GetString("PW").ToString();
            userEmail.text = PlayerPrefs.GetString("Email").ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") print("중복된 사용자 아이디 입니다.");
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
