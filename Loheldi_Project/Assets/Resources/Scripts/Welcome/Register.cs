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
    public InputField InputPW2;
    public InputField InputEmail;
    //public InputField InputAge;



    // 정규식 체크 변수
    bool nameOK = false;
    bool idOK = false;
    bool pwOK = false;
    bool repwOK = false;
    bool emailOK = false;
    public static bool allOK = false;

    //중복 체크 변수
    public static bool idDupChk = false; //id중복
    bool emailDup = false;  //email 중복


    public void Signup() //정규식 만족 체크 함수
    {
        //name check
        if (string.IsNullOrEmpty(InputName.text)){  //null일 경우, 빈 값 넣음
            nameOK = SignupCheck.instance.ChkName();
        }
        else
        {
            nameOK = SignupCheck.instance.ChkName(InputName.text);
        }

        //id check
        if (string.IsNullOrEmpty(InputID.text))
        {
            idOK = SignupCheck.instance.ChkID();
        }
        else
        {
            idOK = SignupCheck.instance.ChkID(InputID.text);
        }

        //password check
        if (string.IsNullOrEmpty(InputPW.text))
        {
            pwOK = SignupCheck.instance.ChkPW();
        }
        else
        {
            pwOK = SignupCheck.instance.ChkPW(InputPW.text);
        }

        //re password check
        if (string.IsNullOrEmpty(InputPW2.text))
        {
            repwOK = SignupCheck.instance.RePW();
        }
        else
        {
            repwOK = SignupCheck.instance.RePW(InputPW.text, InputPW2.text);
        }

        //email check
        if (string.IsNullOrEmpty(InputEmail.text))
        {
            emailOK = SignupCheck.instance.ChkEmail();
        }
        else
        {
            emailOK = SignupCheck.instance.ChkEmail(InputEmail.text);
        }


        if (nameOK && idOK && pwOK && repwOK && emailOK) //정규식을 모두 만족하면, 서버에 정보 저장
        {
            allOK = true;
            UserRegister();
        }
        else
        {
            allOK = false;
          //nameok = false면
        }
    }

    private void UserRegister()  //유저 정보 id, pw, nickname, email 서버에 저장
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(InputID.text, InputPW.text);

       

        ShowStatus(BRO);

        if (BRO.IsSuccess())
        {
            print("동기방식 회원가입 성공");
            Backend.BMember.CreateNickname(InputName.text); //닉네임(이름) 저장
            Backend.BMember.UpdateCustomEmail(InputEmail.text); //비밀번호 찾기 용 이메일 저장
            UserInfoDB();
        }


        
    }


    private void UserInfoDB()    //회원 정보를 user 테이블에 저장
    {
        Param param = new Param();
        param.Add("id", InputID.text);
        //param.Add("pw", InputPW.text);
        param.Add("name", InputName.text);
        //param.Add("email", InputEmail.text);
        //param.Add("age", InputAge.text);

        var bro = Backend.GameData.Insert("user", param);


        if (bro.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }

        else Error(bro.GetErrorCode(), "gamedata");
    }

    //id 중복 체크
    public void ShowStatus(BackendReturnObject backendReturn)
    {
        int statusCode = int.Parse(backendReturn.GetStatusCode());

        switch (statusCode)
        {
            case 201:   //회원가입 성공
                SignupCheck.instance.ExistID(true);
                idDupChk = true;
                break;

            case 409:   // 이미 존재하는 id
                SignupCheck.instance.ExistID(false);
                idDupChk = false;
                break;

            case 401:   //프로젝트 상태가 '점검'일 경우
                Debug.Log("점검");
                break;
            default:
                break;
        }
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


/*    public void Save()    //로컬에 저장
    {
        PlayerPrefs.SetString("Name", InputName.text);
        PlayerPrefs.SetString("ID", InputID.text);
        PlayerPrefs.SetString("PW", InputPW.text);
        PlayerPrefs.SetString("Email", InputEmail.text);
        //PlayerPrefs.SetString("Age", InputAge.text);
    }*/
    /*public void Load()
    {
        if (PlayerPrefs.HasKey("ID"))
        {
            userID.text = PlayerPrefs.GetString("ID");
            userPW.text = PlayerPrefs.GetString("PW").ToString();
            userEmail.text = PlayerPrefs.GetString("Email").ToString();
        }
    }*/

     
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
