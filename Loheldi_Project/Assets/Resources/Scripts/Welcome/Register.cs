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
    public Text CheckID;
    public Text CheckEmail;
    //public Text CheckNick;

    [Header("User Info")]

    public Text userNick;
    public Text userID;
    public Text userPW;
    public Text userEmail;

    // ���Խ� üũ ����
    bool nameOK = false;
    bool idOK = false;
    bool pwOK = false;
    bool repwOK = false;
    bool emailOK = false;
    public static bool allOK = false;

    public void Signup()
    {
        //���Խ� ���� üũ
        SignupCheck sc = new SignupCheck();
        //name check
        if (string.IsNullOrEmpty(InputName.text)){  //null�� ���, �� �� ����
            nameOK = sc.ChkName();
        }
        else
        {
            nameOK = sc.ChkName(InputName.text);
        }

        //id check
        if (string.IsNullOrEmpty(InputID.text))
        {
            idOK = sc.ChkID();
        }
        else
        {
            idOK = sc.ChkID(InputID.text);
        }

        //password check
        if (string.IsNullOrEmpty(InputPW.text))
        {
            pwOK = sc.ChkPW();
        }
        else
        {
            pwOK = sc.ChkPW(InputPW.text);
        }

        //re password check
        if (string.IsNullOrEmpty(InputPW2.text))
        {
            repwOK = sc.RePW();
        }
        else
        {
            repwOK = sc.RePW(InputPW.text, InputPW2.text);
        }

        //email check
        if (string.IsNullOrEmpty(InputEmail.text))
        {
            emailOK = sc.ChkEmail();
        }
        else
        {
            emailOK = sc.ChkEmail(InputEmail.text);
        }


        if(nameOK && idOK && pwOK && repwOK && emailOK) //���Խ��� ��� �����ϸ�, ������ ���� ����
        {
            allOK = true;
            UserRegister();
        }
        else
        {
            allOK = false;
          //nameok = false��
        }
    }

    private void UserRegister()  //���� ���� id, pw ������ ����
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(InputID.text, InputPW.text);
       

        if (BRO.IsSuccess())
        {
            print("������ ȸ������ ����");
        }
        else CheckID.text = "�ߺ��� ���̵� �Դϴ�.";

        UserInfoDB();
    }

    private void UserInfoDB()    //ȸ�� ������ user ���̺� ����(pw, email ���� ����)
    {
        Param param = new Param();
        param.Add("id", InputID.text);
        param.Add("pw", InputPW.text);
        param.Add("name", InputName.text);
        param.Add("email", InputEmail.text);
        //param.Add("age", InputAge.text);

        var bro = Backend.GameData.Insert("user", param);

        if (bro.IsSuccess())
        {
            print("���� ��� ������ �Է� ����");
        }

        else Error(bro.GetErrorCode(), "gamedata");
    }

    
    public void Login()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(InputID.text, InputPW.text);

        if (BRO.IsSuccess())
        {
            print("������ �α��� ����");

        }

        else Error(BRO.GetErrorCode(), "UserFunc");

    }

    public void CreateEmail()
    {
        BackendReturnObject BRO = Backend.BMember.UpdateCustomEmail(InputEmail.text);

        if (BRO.IsSuccess()) print("���� ��� �̸��� ��� �Ϸ�");
        else CheckEmail.text = "�ߺ��� �̸��� �ּ��Դϴ�.";
               
    }
/*    public void Save()    //���ÿ� ����
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
        }
    }
}
