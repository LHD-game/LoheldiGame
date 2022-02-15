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
    // ������ ȸ������
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
            print("���� ��� ������ �Է� ����");
        }
       
        else Error(bro.GetErrorCode(), "gamedata");
        
        

        if (BRO.IsSuccess())
        {
            print("������ ȸ������ ����");
        }
        else CheckID.text = "�ߺ��� ���̵��Դϴ�.";

        
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
    /*public void checkInfo2()
    {
        var bro = Backend.GameData.Get("user", new Where(), 10);
        if (bro.IsSuccess() == false)
        {
            // ��û ���� ó��
            Debug.Log(bro);
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            print("ID:" + "id");
            print("PW:" + "pw");
            print("Email:" + "email");
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
    
        
    // ������ �α���
    public void Login()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(ID.text, PW.text);

        if (BRO.IsSuccess())
        {
            print("������ �α��� ����");

        }
        
        else Error(BRO.GetErrorCode(), "UserFunc");
        
    }
    

    // ���� ��� �α׾ƿ�
    public void LogOut()
    {
        Backend.BMember.Logout();
        ID.text = PW.text = "";
        print("���� ��� �α׾ƿ� ����");

    }
    // ���� ��� ȸ��Ż��
    public void SignOut()
    {
        // Backend.BMember.SignOut("reason");
        // ������ �ۼ��� �� �ֽ��ϴ�.
        Backend.BMember.SignOut();
        ID.text = PW.text = "";
        print("���� ��� ȸ��Ż�� ����");
    }
    // ���� ��� ���� �г��� ����
    public void CreateUserNickname()
    {
        BackendReturnObject BRO = Backend.BMember.CreateNickname(Nickname.text);

        if (BRO.IsSuccess()) print("���� ��� �г��� ���� �Ϸ�");
        else Error(BRO.GetErrorCode(), "UserNickname");
    }

    // ���� ��� ���� �г��� ������Ʈ
    public void UpdateUserNickname()
    {
        BackendReturnObject BRO = Backend.BMember.CheckNicknameDuplication(Nickname.text);

        if (BRO.IsSuccess())
        {
            CheckNick.text = "��밡���� �г����Դϴ�.";
        }
        else
        {
            CheckNick.text = "�ߺ��� �г����Դϴ�.";
        }
    }
    // ���� ��� ���� ���� ��������
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
    // ���� ��� �̸��� ���
    public void UpdateEmail()
    {
        BackendReturnObject BRO = Backend.BMember.UpdateCustomEmail(email.text);

        if (BRO.IsSuccess()) print("���� ��� �̸��� ��� �Ϸ�");
    }

    public void getUserData()
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


            /*print("ID:" + userID.text);
            print("PW:" + userPW.text);*/
            userID.text = "ID:" + ID.text;
            userPW.text = "PW:" + PW.text;
            //userEmail.text = "email:" + email;
        }
        else Error(bro.GetErrorCode(), "gameData");

    }
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
    // ���� �ڵ� Ȯ��
    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") CheckID.text = "�ߺ��� ����� ���̵� �Դϴ�.";
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

    /*public void OnClickInsertData()
    {

        int Level = Random.Range(0, 99);
        double Coin = Random.Range(0, 99999);

        // Param�� �ڳ� ������ ����� �� �� �Ѱ��ִ� �Ķ���� Ŭ���� �Դϴ�. 
        Param param = new Param();
        param.Add("lv", Level);
        param.Add("coin", Coin);


        // ���� Dictionary �� ����ϱ�
        Dictionary<string, int> equipment = new Dictionary<string, int>
        {
            { "weapon", 123 },
            { "armor", 111 },
            { "helmet", 1345 }
        };

        param.Add("equipItem", equipment);

        BackendReturnObject BRO = Backend.GameInfo.Insert("game", param);

        if (BRO.IsSuccess())
        {
            Debug.Log("indate : " + BRO.GetInDate());
        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "404":
                    Debug.Log("�������� �ʴ� tableName�� ���");
                    break;

                case "412":
                    Debug.Log("��Ȱ��ȭ �� tableName�� ���");
                    break;

                case "413":
                    Debug.Log("�ϳ��� row( column���� ���� )�� 400KB�� �Ѵ� ���");
                    break;

                default:
                    Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
                    break;
            }
        }
    }*/
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


    void Update()
    {
        /*//�α���(�񵿱�)
        if (isSuccess)
        {
            // SaveToken( BackendReturnObject bro ) -> void
            // �񵿱� �޼ҵ�� update()������ SaveToken�� �� �����ؾ� �մϴ�.
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);

            if (saveToken.IsSuccess()) print("�񵿱� �α��� ����");
                //loginState.text = "�α��� ���� : �α���";
            
            else Error(bro.GetErrorCode(), "UserFunc");

            isSuccess = false;
            bro.Clear();*/
        
    }
    }



