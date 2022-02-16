using BackEnd;
using LitJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField ID;    
    public InputField PW;
    public InputField Nickname;
    public InputField email;
    public Text CheckID;
    public Text CheckNick;

    [Header("User Info")]
    
    public Text userNick;
    public Text userID;
    public Text userPW;
    public Text userEmail;

    /*BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;
    */private string errorCode;

    Dictionary<string, string> user = new Dictionary<string, string>
    {
        {"id", ""},
        {"pw", "" },
        {"email", "" }
    };
    /*private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(gameObject);
        
    }*/

    void Start()
    {
        
        
    }
    // 동기방식 회원가입
    public void Register()
    {
        
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(ID.text, PW.text, email.text);
        Param param = new Param();
        param.Add("id", ID.text);
        param.Add("pw", PW.text);
        param.Add("email", email.text);
        
                
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
        else CheckID.text = "중복된 아이디입니다.";

        
    }
    public void checkInfo()
    {
        var bro = Backend.GameData.GetMyData("user", new Where(), 10);
        if (bro.IsSuccess())
        {
            bro.GetReturnValuetoJSON();
            bool I = user.TryGetValue("id", out string id);
            {
                print("ID:" + id);
            };
            var P = user.TryGetValue("pw", out var pw);
            {
                print("PW:" + pw);
            };
            var E = user.TryGetValue("email", out var email);
            {
                print("Email:" + email);
            };
            /* print($"level: {charset.TryGetValue("Level", out string testValue)}"){
                 Debug.Log("level: " + testValue);
             };*/
            /* print($"coin: {charset}"[1]);
             print($"exp: {charset}"[2]);*/
            print(I);
            print(P);
            print(E);
        }

        else
        {
            // 에러 체크
            CheckError(bro);
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log(bro);
            return;
        }

        // 검색한 데이터의 모든 row의 inDate 값 확인
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
    }
    /*public void checkInfo2()
    {
        var bro = Backend.GameData.Get("user", new Where(), 10);
        if (bro.IsSuccess() == false)
        {
            // 요청 실패 처리
            Debug.Log(bro);
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            print("ID:" + "id");
            print("PW:" + "pw");
            print("Email:" + "email");
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log(bro);
            return;
        }

        // 검색한 데이터의 모든 row의 inDate 값 확인
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
    }*/
    
        
    // 동기방식 로그인
    public void Login()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(ID.text, PW.text);

        if (BRO.IsSuccess())
        {
            print("동기방식 로그인 성공");

        }
        
        else Error(BRO.GetErrorCode(), "UserFunc");
        
    }
    

    // 동기 방식 로그아웃
    public void LogOut()
    {
        Backend.BMember.Logout();
        ID.text = PW.text = "";
        print("동기 방식 로그아웃 성공");

    }
    // 동기 방식 회원탈퇴
    public void SignOut()
    {
        // Backend.BMember.SignOut("reason");
        // 이유도 작성할 수 있습니다.
        Backend.BMember.SignOut();
        ID.text = PW.text = "";
        print("동기 방식 회원탈퇴 성공");
    }
    // 동기 방식 유저 닉네임 생성
    public void CreateUserNickname()
    {
        BackendReturnObject BRO = Backend.BMember.CreateNickname(Nickname.text);

        if (BRO.IsSuccess()) print("동기 방식 닉네임 생성 완료");
        else Error(BRO.GetErrorCode(), "UserNickname");
    }

    // 동기 방식 유저 닉네임 업데이트
    public void UpdateUserNickname()
    {
        BackendReturnObject BRO = Backend.BMember.CheckNicknameDuplication(Nickname.text);

        if (BRO.IsSuccess())
        {
            CheckNick.text = "사용가능한 닉네임입니다.";
        }
        else
        {
            CheckNick.text = "중복된 닉네임입니다.";
        }
    }
    // 동기 방식 유저 정보 가져오기
    public void getUserInfo()
    {
        BackendReturnObject BRO = Backend.BMember.GetUserInfo();

        JsonData returnJson = BRO.GetReturnValuetoJSON()["row"];

        userNick.text = "nickName : " + returnJson["nickname"] +
                        "\ninDate : " + returnJson["inDate"].ToString() +
                        "\nsubscriptionType : " + returnJson["subscriptionType"].ToString() +
                        "\nemailForFindPassword : " + returnJson["emailForFindPassword"];
        /*userEmail.text = "email : " + returnJson["email"] +
                        "\ninDate : " + returnJson["inDate"].ToString() +
                        "\nsubscriptionType : " + returnJson["subscriptionType"].ToString() +
                        "\nemailForFindPassword : " + returnJson["emailForFindPassword"];*/

        /*userID.text = "ID : " + returnJson["id"].ToString();*/

    }
    // 동기 방식 이메일 등록
    public void UpdateEmail()
    {
        BackendReturnObject BRO = Backend.BMember.UpdateCustomEmail(email.text);

        if (BRO.IsSuccess()) print("동기 방식 이메일 등록 완료");
    }

    /*public void getUserData()
    {

        Where where = new Where();
        where.Equal("id", ID.text);
        where.Equal("pw", PW.text);
        //where.Equal("email", email);

        var bro = Backend.GameData.Get("user", where);
        //where.Equal("email", userEmail.text);
        
        
        if (bro.IsSuccess())
        {
            JsonData jsonData = bro.GetReturnValuetoJSON();
            
            //string id = jsonData["id"][0].ToString();
            //string pw = jsonData["pw"][0].ToString();
            //string email = jsonData["email"][0].ToString();


            *//*print("ID:" + userID.text);
            print("PW:" + userPW.text);*//*
            userID.text = "ID:" + ID.text;
            userPW.text = "PW:" + PW.text;
            //userEmail.text = "email:" + email;
        }
        else Error(bro.GetErrorCode(), "gameData");

    }*/
    public void Save()
    {
        PlayerPrefs.SetString("ID", ID.text);
        PlayerPrefs.SetString("PW", PW.text);
        PlayerPrefs.SetString("email", email.text);
    }
    public void Road()
    {
        if(PlayerPrefs.HasKey("ID"))
        {
            userID.text = PlayerPrefs.GetString("ID");
            userPW.text = PlayerPrefs.GetString("PW").ToString();
            userEmail.text = PlayerPrefs.GetString("email").ToString();
        }
    }
    // 에러 코드 확인
    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") CheckID.text = "중복된 사용자 아이디 입니다.";
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

    
    void CheckError(BackendReturnObject BRO)
    {
        switch (BRO.GetStatusCode())
        {
            case "200":
                Debug.Log("해당 유저의 데이터가 테이블에 없습니다.");
                break;

            case "404":
                if (BRO.GetMessage().Contains("gamer not found"))
                {
                    Debug.Log("gamerIndate가 존재하지 gamer의 indate인 경우");
                }
                else if (BRO.GetMessage().Contains("table not found"))
                {
                    Debug.Log("존재하지 않는 테이블");
                }
                break;

            case "400":
                if (BRO.GetMessage().Contains("bad limit"))
                {
                    Debug.Log("limit 값이 100이상인 경우");
                }

                else if (BRO.GetMessage().Contains("bad table"))
                {
                    // public Table 정보를 얻는 코드로 private Table 에 접근했을 때 또는
                    // private Table 정보를 얻는 코드로 public Table 에 접근했을 때 
                    Debug.Log("요청한 코드와 테이블의 공개여부가 맞지 않습니다.");
                }
                break;

            case "412":
                Debug.Log("비활성화된 테이블입니다.");
                break;

            default:
                Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
                break;

        }
    }


    void Update()
    {
        /*//로그인(비동기)
        if (isSuccess)
        {
            // SaveToken( BackendReturnObject bro ) -> void
            // 비동기 메소드는 update()문에서 SaveToken을 꼭 적용해야 합니다.
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);

            if (saveToken.IsSuccess()) print("비동기 로그인 성공");
                //loginState.text = "로그인 상태 : 로그인";
            
            else Error(bro.GetErrorCode(), "UserFunc");

            isSuccess = false;
            bro.Clear();*/
        
    }
    }



