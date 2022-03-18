//CreateAcc Scene에서 계정 정보를 저장하기 위한 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using BackEnd;
using System;

public class NewAccSave : MonoBehaviour
{
    [SerializeField]
    private GameObject NickNameField;   //닉네임 입력 화면
    [SerializeField]
    private GameObject BirthField;   //생년월일 입력 화면
    [SerializeField]
    private GameObject ResultField;   //임시 값 확인용

    [SerializeField]
    private InputField InputNickName;   //계정 닉네임
    [SerializeField]
    private Dropdown[] InputBirth = new Dropdown[3];      //계정주 생년,월,일

    string uNickName;   // 서버에 저장되기 전 값을 담아놓는 변수
    DateTime uBirth;    // 위와 같음

    public Text nick;
    public Text birth;

    void Start()
    {
        NickNameField.SetActive(true);
        BirthField.SetActive(false);
        ResultField.SetActive(false);
    }

    private void Update()
    {
        //입력 확인용 결과 출력
        if (ResultField.activeSelf)
        {
            nick.text = "닉네임: " + uNickName;
            birth.text = "생년월일: " + uBirth.ToString("yyyy년 M월 d일");
        }
    }

    public void SaveNickName()  //닉네임 입력 후 버튼을 눌렀을 경우 실행
    {
        Regex regex = new Regex(@"[a-zA-Z가-힣0-9]{2,8}$"); //닉네임 정규식. 영대소문자, 한글 2~8자 가능

        if ((regex.IsMatch(InputNickName.text))) //정규식 일치시,
        {
            uNickName = InputNickName.text; //uNickName 변수에 입력값을 저장하고,
            
            ShowNHide(BirthField, NickNameField);   //닉네임 입력 비활성화, 생일 입력 활성화
        }
        else    //정규식 불일치시
        {
            //오류 팝업 활성화
            Transform t = NickNameField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
        }
    }

    public void SaveBirth() //생년월일 입력 후 버튼을 눌렀을 경우 실행
    {
        Regex regex = new Regex(@"[0-9]{1,5}$"); //생년월일 정규식
        bool isOK = true;
        for (int i=0; i<InputBirth.Length; i++)
        {
            string birthValue = InputBirth[i].options[InputBirth[i].value].text;
            if (!(regex.IsMatch(birthValue)))  //정규식 불일치 시,
            {
                isOK = false;
                Debug.Log(birthValue);
            }
        }

        if (isOK)   //모두 정규식 일치하면
        {
            string str = InputBirth[0].options[InputBirth[0].value].text + "/";
            str += InputBirth[1].options[InputBirth[1].value].text + "/";
            str += InputBirth[2].options[InputBirth[2].value].text; //yyyy/MM/dd
            Debug.Log(str);
            uBirth = Convert.ToDateTime(str);   //uBirth 변수에 입력값 저장
            ShowNHide(ResultField, BirthField);
        }
        else    //정규식 불일치시
        {
            //오류 팝업 활성화
            Transform t = BirthField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
        }
    }

    private void ShowNHide(GameObject show, GameObject hide)    //활성화 할 것 첫번째, 비활성화 할 것은 두번째 인자로 준다.
    {
        show.SetActive(true);
        hide.SetActive(false);
    }

    public void ClosePop(GameObject go) //오류 팝업 닫기 메소드
    {
        go.SetActive(false);
    }

    //최종적으로 서버에 닉네임과 생년월일을 저장하는 메소드
    public void AccSave()
    {
        Param param = new Param();
        param.Add("BIRTH", uBirth);
        param.Add("NICKNAME", uNickName);


        var bro = Backend.GameData.Insert("ACC_INFO", param);

        if (bro.IsSuccess())
        {
            Debug.Log("계정 정보 설정 완료!");
            //SceneLoader.instance.GotoGameMove();
            SceneLoader.instance.GotoPlayerCustom();    //캐릭터 커스터마이징 씬으로 이동
        }
        else
        {
            Debug.Log("계정 정보 설정 실패!");
            //todo: 오류 문의 안내 문구 띄우기
        }
    }
}
