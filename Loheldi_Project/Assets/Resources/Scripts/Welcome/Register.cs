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



    // ���Խ� üũ ����
    bool nameOK = false;
    bool idOK = false;
    bool pwOK = false;
    bool repwOK = false;
    bool emailOK = false;
    public static bool allOK = false;

    //�ߺ� üũ ����
    public static bool idDupChk = false; //id�ߺ�
    bool emailDup = false;  //email �ߺ�


    public void Signup() //���Խ� ���� üũ �Լ�
    {
        //name check
        if (string.IsNullOrEmpty(InputName.text)){  //null�� ���, �� �� ����
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


        if (nameOK && idOK && pwOK && repwOK && emailOK) //���Խ��� ��� �����ϸ�, ������ ���� ����
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

    private void UserRegister()  //���� ���� id, pw, nickname, email ������ ����
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(InputID.text, InputPW.text);

       

        ShowStatus(BRO);

        if (BRO.IsSuccess())
        {
            print("������ ȸ������ ����");
            Backend.BMember.CreateNickname(InputName.text); //�г���(�̸�) ����
            Backend.BMember.UpdateCustomEmail(InputEmail.text); //��й�ȣ ã�� �� �̸��� ����
            UserInfoDB();
        }


        
    }


    private void UserInfoDB()    //ȸ�� ������ user ���̺� ����
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
            print("���� ��� ������ �Է� ����");
        }

        else Error(bro.GetErrorCode(), "gamedata");
    }

    //id �ߺ� üũ
    public void ShowStatus(BackendReturnObject backendReturn)
    {
        int statusCode = int.Parse(backendReturn.GetStatusCode());

        switch (statusCode)
        {
            case 201:   //ȸ������ ����
                SignupCheck.instance.ExistID(true);
                idDupChk = true;
                break;

            case 409:   // �̹� �����ϴ� id
                SignupCheck.instance.ExistID(false);
                idDupChk = false;
                break;

            case 401:   //������Ʈ ���°� '����'�� ���
                Debug.Log("����");
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
            print("������ �α��� ����");

        }

        else Error(BRO.GetErrorCode(), "UserFunc");

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
