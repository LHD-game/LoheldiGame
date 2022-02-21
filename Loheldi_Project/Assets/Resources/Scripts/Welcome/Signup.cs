using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;

public class Signup : MonoBehaviour
{
    public InputField uName;
    public InputField uID;
    public InputField uPW;
    public InputField uPW2;
    public InputField uEmail;

    bool nameOK = false;
    bool idOK = false;
    bool pwOK = false;
    bool repwOK = false;
    bool emailOK = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SignUp()
    {
        SignupCheck sc = new SignupCheck();
        nameOK = sc.ChkName(uName.text);
        idOK = sc.ChkID(uID.text);
        pwOK = sc.ChkPW(uPW.text);
        repwOK = sc.RePW(uPW.text, uPW2.text);
        emailOK = sc.ChkEmail(uEmail.text);
/*        BackendReturnObject bro = Backend.BMember.CustomSignUp(uID.text, uPW.text); //id, pw ����
        Backend.BMember.CreateNickname(uName.text); //�г���(�̸�) ����
        Backend.BMember.UpdateCustomEmail(uEmail.text); //�̸��� ����
        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }*/


    }

    public bool IDChk()
    {
        //id�� �ߺ����� ������
        return true;

        //id�� �ߺ��Ǹ�
        return false;
    }


}
