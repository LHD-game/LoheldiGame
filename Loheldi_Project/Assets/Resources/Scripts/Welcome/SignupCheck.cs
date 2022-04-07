using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class SignupCheck : MonoBehaviour
{
    private static SignupCheck _instance;
    public static SignupCheck instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SignupCheck>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private Transform[] ErrorLine = new Transform[5]; //순서대로 이름, ID, PW, 재입력PW, 이메일
    [SerializeField]
    private Transform[] ErrorTxt = new Transform[7];    //오류 문구 배열, 중복id, 중복 email 포함

    public void Start()
    {
        for (int i = 0; i < ErrorLine.Length; i++)
        {
            ErrorLine[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < ErrorTxt.Length; i++)
        {
            ErrorTxt[i].gameObject.SetActive(false);
        }
        Debug.Log(ErrorLine.Length);  //오브젝트 인지O
        Debug.Log(ErrorLine[1]);  //오브젝트 인지O
    }



    public bool ChkName(string uName = "")   //이름 2~6자 이내 한글/영어, 공백 미포함
    {
        Regex regex = new Regex(@"[a-zA-Z가-힣]{2,6}$"); //이름 정규식. 영대소문자, 한글 2~6자 가능
        bool isCorrect = true; //한글, 영어로만 이루어짐+공백 미포함일 시 true

        if ((regex.IsMatch(uName)))    //정규식 일치 시
        {
            Debug.Log("이름이 양식과 일치합니다.");
            ErrorLine[0].gameObject.SetActive(false);
            ErrorTxt[0].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("이름이 양식과 일치하지 않습니다.");
            Debug.Log(ErrorLine[1]);  //오브젝트 인지X
            ErrorLine[0].gameObject.SetActive(true);
            ErrorTxt[0].gameObject.SetActive(true);
            isCorrect = false;
        }

        return isCorrect;
    }

    public bool ChkID(string uID = "")     //ID 5~20자 이내 영어, 숫자
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]$"); //ID 정규식. 영대소문자, 숫자 0~8자 이내 가능
        bool isCorrect = true; //정규식 만족 시, true

        if(uID.Length <= 8)
        {
            if ((regex.IsMatch(uID)))    //정규식 불일치 시
            {
                Debug.Log("ID가 양식과 일치합니다.");
                ErrorLine[1].gameObject.SetActive(false);
                ErrorTxt[1].gameObject.SetActive(false);
                isCorrect = true;
            }
            else
            {
                Debug.Log("ID가 양식과 일치하지 않습니다.");
                ErrorLine[1].gameObject.SetActive(true);
                ErrorTxt[1].gameObject.SetActive(true);
                isCorrect = false;
            }
        }
        else
        {
            Debug.Log("ID가 양식과 일치하지 않습니다.(자리수 불일치)");
            ErrorLine[1].gameObject.SetActive(true);
            ErrorTxt[1].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;
    }

    public bool ChkPW(string uPW = "")     //비밀번호 20자 이내 영어+숫자+특수문자 조합
    {
        bool isCorrect = true; //정규식 만족 시, true
        char[] upw = uPW.ToCharArray();  //입력받은거 배열로 저장
        int Num = 0; //숫자 연속 체크
        int Num2 = 0;  //반복 숫자 체크
        int Num3 = 0;  //연속 숫자 체크
        int[] Numupw = new int[uPW.Length];
        bool numupw = true; //비번 연속, 반복 4이상 아닐시, ture
        
        //숫자1이상, 영문1이상, 특수문자1이상
        Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$", RegexOptions.IgnorePatternWhitespace);

        if (uPW.Length >= 6 && uPW.Length <= 10)
        {
            foreach(char c in upw) //upw안에 애들 한번씩 실행
            {
                if (char.IsNumber(c))
                {
                    Numupw[Num] = (int)c; //숫자 따로 배열로 뺌
                    Num++;
                    if (Num == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (Numupw[i] == Numupw[i + 1]) //반복일때
                                Num2++;
                            else if (Numupw[i] + 1 == Numupw[i + 1]) //연속일때 (+형)
                                Num3++;
                            else if (Numupw[i] - 1 == Numupw[i + 1]) //연속일떼(-형)
                                Num3++;
                            else
                                continue;
                            Debug.Log("연속 숫자:" + Num3);
                            Debug.Log("i:" + i);

                        }
                        if(Num2==4^Num3==4)
                            numupw = false;
                        Debug.Log("반복 숫자:" + Num2);
                        
                        break;
                    }
                }
                else
                    Num = 0;

            }
            
            if (!numupw)
            {
                ErrorLine[2].gameObject.SetActive(true);
                ErrorTxt[2].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PW중 연속/반복되는 숫자가 4개이상 있습니다.");
            }
            else if (regex.IsMatch(uPW))
            {
                ErrorLine[2].gameObject.SetActive(false);
                ErrorTxt[2].gameObject.SetActive(false);
                isCorrect = true;
                Debug.Log("PW가 양식과 일치합니다.");
            }
            else
            {
                ErrorLine[2].gameObject.SetActive(true);
                ErrorTxt[2].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PW가 양식과 일치하지 않습니다.(정규식 불만족)");
                
            }

        }
        else
        {
            ErrorLine[2].gameObject.SetActive(true);
            ErrorTxt[2].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("PW가 양식과 일치하지 않습니다.(자릿수 불일치)");
        }
        return isCorrect;
    }
        
    

    public bool RePW(string PW = "", string rePW = "")      //비밀번호 중복 확인
    {
        bool isCorrect = true;

        if (PW.Equals(rePW))
        {
            ErrorLine[3].gameObject.SetActive(false);
            ErrorTxt[3].gameObject.SetActive(false);
            isCorrect = true;
            Debug.Log("pw가 일치합니다.");
        }
        else
        {
            ErrorLine[3].gameObject.SetActive(true);
            ErrorTxt[3].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("pw가 일치하지 않습니다.");
        }

        return isCorrect;
    }

    public bool ChkEmail(string uEmail = "")  //e-mail 양식 확인
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{1,20}@[a-zA-Z0-9]{1,20}\.[a-zA-Z]{1,5}$"); //이메일 정규식
        bool isCorrect = true; //정규식 만족 시, true

        if ((regex.IsMatch(uEmail)))    //정규식 불일치 시
        {
            Debug.Log("email이 양식과 일치합니다.");
            ErrorLine[4].gameObject.SetActive(false);
            ErrorTxt[4].gameObject.SetActive(false);
            isCorrect = true;
        }
        
        else
        {
            Debug.Log("email이 양식과 일치하지 않습니다.");
            ErrorLine[4].gameObject.SetActive(true);
            ErrorTxt[4].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;

    }


//이미 존재하는 ID, Email popup

    public void ExistID(bool isExist)   
    {
        if (isExist)
        {
            Debug.Log("사용 가능한 id 입니다.");
            ErrorTxt[5].gameObject.SetActive(false);
            ErrorLine[1].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("이미 존재하는 id 입니다.");
            ErrorTxt[5].gameObject.SetActive(true);
            ErrorLine[1].gameObject.SetActive(true);
            
        }
        
    }

    public void ExistEmail()
    {
        ErrorTxt[6].gameObject.SetActive(true);
    }

}
