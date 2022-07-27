using BackEnd;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    //---signup---
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

    //---login---
    [SerializeField]
    private InputField InputLID;
    [SerializeField]
    private InputField InputLPW;
    public Transform ErrorLine;
    public Transform ErrorTxt;    //로그인 실패 문구
    public GameObject AutoBtn;  //자동로그인 버튼
    private bool isAutoChk;

    //---find id/pw---
    [SerializeField]
    private InputField InputFID_Email;
    [SerializeField]
    private InputField InputFPW_Email;
    [SerializeField]
    private InputField InputFPW_ID;

    public Text FindIDErrorTxt;
    public Text InitPWErrorTxt;

    public static bool isIDFind = false;
    public static bool isPWFind = false;

    //level, coin ..etc
    public int money;
    public static int level;
    public static float exp;
    float Maxexp;

    public void Start()
    {
        ErrorLine.gameObject.SetActive(false);
        ErrorTxt.gameObject.SetActive(false);
    }

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
            Save_Basic.PlayerInfoInit();    //계정 기본 정보 저장(레벨, 재화 등)
            Save_Basic.UserGardenInit();    //텃밭 초기 행 저장
            Save_Basic.UserHousingInit();    //하우징 초기 행 저장
            Save_Basic.Save0_0Quest();


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
    public void Login() //로그인시 실행되는 메소드
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(InputLID.text, InputLPW.text);

        if (BRO.IsSuccess())
        {
            ErrorLine.gameObject.SetActive(false);
            ErrorTxt.gameObject.SetActive(false);
            //자동로그인 함수 실행
            AutoLogin();
            Debug.Log("동기방식 로그인 성공");

            // play_info를 서버에서 불러와 로컬에 저장
            Save_Basic.LoadPlayInfo();
            Save_Log.instance.SaveLoginLog();
            Save_Basic.LoadUserGarden();

            if (AccChk())   //계정 정보 만들어져 있으면, 계정 정보를 로컬에 저장하고, 필드로
            {
                Save_Basic.LoadAccInfo();  //계정 정보를 로컬에 저장
                SceneLoader.instance.GotoMainField();
            }
            else    //없으면 계정 정보 생성
            {
                SceneLoader.instance.GotoCreateAcc();
            }
            
        }
        else
        {
            ErrorLine.gameObject.SetActive(true);
            ErrorTxt.gameObject.SetActive(true);
            Error(BRO.GetErrorCode(), "UserFunc");
            Debug.Log(BRO.GetMessage());
        }
    }

    //계정 정보 존재 여부 체크 메소드
    private bool AccChk()
    {
        bool isOK = false;
        BackendReturnObject bro = Backend.GameData.GetMyData("ACC_INFO", new Where(), 100);

        if (bro.IsSuccess())
        {
            var json = bro.GetReturnValuetoJSON();
            
            try
            {
                var playerJson = json["rows"][0];
                ParsingJSON pj = new ParsingJSON();
                AccInfo data = pj.ParseBackendData<AccInfo>(playerJson);
                Debug.Log(data.NICKNAME);
                if (data.NICKNAME.Equals(null))
                {
                    isOK = false;
                }
                else
                {
                    isOK = true;
                }
                
            }
            catch (Exception ex) //조회에는 성공했으나, 해당 값이 없음(NullPointException)
            {
                Debug.Log(ex);
                isOK = false;
            }
        }
        else
        {
            Debug.Log("조회 실패" + bro.GetMessage());
            //todo: 새로고침 팝업 만들기, 403 fobidden. 잦은 요청 트래픽으로 인한 오류. 5분 대기 후 다시 접속하면 해결 된다.

        }
        return isOK;
    }


    public void AutoLogin()    //로컬에 저장해둔다 --> 변경필요! todo
    {
        isAutoChk = AutoBtn.GetComponent<Toggle>().isOn;
        if (isAutoChk)  //자동로그인 체크 시
        {
            PlayerPrefs.SetString("ID", InputID.text);
            PlayerPrefs.SetString("PW", InputPW.text);
        }

    }

    public void FindID()    //아이디 찾기
    {
        string uEmail = InputFID_Email.text;
        var bro = Backend.BMember.FindCustomID(uEmail);
        if (ChkCase(bro, "ID"))
        {
            isIDFind = true;
        }
        else
        {
            isIDFind = false;
        }

    }

    public void InitPW()    //비밀번호 초기화
    {
        string uID = InputFPW_ID.text;
        string uEmail = InputFPW_Email.text;
        var bro = Backend.BMember.ResetPassword(uID, uEmail);
        if (ChkCase(bro, "PW"))
        {
            isPWFind = true;
        }
        else
        {
            isPWFind = false;
        }
    }

    private bool ChkCase(BackendReturnObject bro, string find)  //계정 찾기 성공 여부 확인 
    {
        int statusCode = int.Parse(bro.GetStatusCode());
        bool isSuccess = false;
        switch (statusCode)
        {
            case 204:   //이메일 송신 성공
                Debug.Log(bro.GetMessage());
                if (find.Equals("ID"))
                {
                    FindIDErrorTxt.gameObject.SetActive(false);
                }
                else
                {
                    InitPWErrorTxt.gameObject.SetActive(false);
                }

                isSuccess = true;
                break;

            case 404:   // 등록된 유저가 없는 경우
                Debug.Log(bro.GetMessage());
                if (find.Equals("ID"))
                {
                    FindIDErrorTxt.gameObject.SetActive(true);
                    FindIDErrorTxt.text = bro.GetMessage();
                }
                else
                {
                    InitPWErrorTxt.gameObject.SetActive(true);
                    InitPWErrorTxt.text = bro.GetMessage();
                }
                break;

            case 429:   //24시간 이내에 5회 이상 같은 이메일 정보로 찾기를 시도한 경우
                Debug.Log(bro.GetMessage());
                if (find.Equals("ID"))
                {
                    FindIDErrorTxt.gameObject.SetActive(true);
                    FindIDErrorTxt.text = bro.GetMessage();
                }
                else
                {
                    InitPWErrorTxt.gameObject.SetActive(true);
                    InitPWErrorTxt.text = bro.GetMessage();
                }

                break;

            default:
                Debug.Log(bro.GetMessage());
                if (find.Equals("ID"))
                {
                    FindIDErrorTxt.gameObject.SetActive(true);
                    FindIDErrorTxt.text = "문제가 발생했습니다.\n자세한 사항은 loheldi412@gmail.com으로 문의 바랍니다. (오류코드: " + statusCode + ")";
                }
                else
                {
                    InitPWErrorTxt.gameObject.SetActive(true);
                    InitPWErrorTxt.text = "문제가 발생했습니다.\n자세한 사항은 loheldi412@gmail.com으로 문의 바랍니다. (오류코드:" + statusCode + ")";
                }
                
                break;
        }

        return isSuccess;
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
