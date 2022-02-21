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
    public InputField uEmail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SignUp()
    {
        BackendReturnObject bro = Backend.BMember.CustomSignUp(uID.text, uPW.text); //id, pw 저장
        Backend.BMember.CreateNickname(uName.text); //닉네임(이름) 저장
        Backend.BMember.UpdateCustomEmail(uEmail.text); //이메일 저장
        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다");
        }


    }

    public bool IDChk()
    {
        //id가 중복되지 않으면
        return true;

        //id가 중복되면
        return false;
    }


}
