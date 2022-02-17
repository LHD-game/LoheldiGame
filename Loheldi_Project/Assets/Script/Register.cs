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
            print("���� ��� ������ �Է� ����");
        }

        else Error(bro.GetErrorCode(), "gamedata");

        if (BRO.IsSuccess())
        {
            print("������ ȸ������ ����");
        }
        else CheckID.text = "�ߺ��� ���̵� �Դϴ�.";

    }
    
    public void CreateEmail()
    {
        BackendReturnObject BRO = Backend.BMember.UpdateCustomEmail(InputEmail.text);
      
        if (BRO.IsSuccess()) print("���� ��� �̸��� ��� �Ϸ�");
               
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