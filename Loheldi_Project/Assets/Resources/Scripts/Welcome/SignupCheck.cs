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
    private Transform[] ErrorLine = new Transform[5]; //������� �̸�, ID, PW, ���Է�PW, �̸���
    [SerializeField]
    private Transform[] ErrorTxt = new Transform[7];    //���� ���� �迭, �ߺ�id, �ߺ� email ����

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
        //Debug.Log(ErrorLine.Length);  //������Ʈ ����O
        //Debug.Log(ErrorLine[1]);  //������Ʈ ����O
    }



    public bool ChkName(string uName = "")   //�̸� 2~6�� �̳� �ѱ�/����, ���� ������
    {
        Regex regex = new Regex(@"[a-zA-Z��-�R]{2,6}$"); //�̸� ���Խ�. ����ҹ���, �ѱ� 2~6�� ����
        bool isCorrect = true; //�ѱ�, ����θ� �̷����+���� �������� �� true

        if ((regex.IsMatch(uName)))    //���Խ� ��ġ ��
        {
            Debug.Log("�̸��� ��İ� ��ġ�մϴ�.");
            ErrorLine[0].gameObject.SetActive(false);
            ErrorTxt[0].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("�̸��� ��İ� ��ġ���� �ʽ��ϴ�.");
            Debug.Log(ErrorLine[1]);  //������Ʈ ����X
            ErrorLine[0].gameObject.SetActive(true);
            ErrorTxt[0].gameObject.SetActive(true);
            isCorrect = false;
        }

        return isCorrect;
    }

    public bool ChkID(string uID = "")     //ID 5~20�� �̳� ����, ����
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{5,20}$"); //ID ���Խ�. ����ҹ���, ���� 5~20�� �̳� ����
        bool isCorrect = true; //���Խ� ���� ��, true

        if ((regex.IsMatch(uID)))    //���Խ� ����ġ ��
        {
            Debug.Log("ID�� ��İ� ��ġ�մϴ�.");
            ErrorLine[1].gameObject.SetActive(false);
            ErrorTxt[1].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("ID�� ��İ� ��ġ���� �ʽ��ϴ�.");
            ErrorLine[1].gameObject.SetActive(true);
            ErrorTxt[1].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;
    }

    public bool ChkPW(string uPW = "")     //��й�ȣ 20�� �̳� ����+����+Ư������ ����
    {
        bool isCorrect = true; //���Խ� ���� ��, true

        //����1�̻�, ����1�̻�, Ư������1�̻�
        Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$", RegexOptions.IgnorePatternWhitespace);

        if (uPW.Length >= 5 && uPW.Length <= 20)
        {
            if (regex.IsMatch(uPW))
            {
                ErrorLine[2].gameObject.SetActive(false);
                ErrorTxt[2].gameObject.SetActive(false);
                isCorrect = true;
                Debug.Log("PW�� ��İ� ��ġ�մϴ�.");
            }
            else
            {
                ErrorLine[2].gameObject.SetActive(true);
                ErrorTxt[2].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PW�� ��İ� ��ġ���� �ʽ��ϴ�.(���Խ� �Ҹ���)");
            }
        }
        else
        {
            ErrorLine[2].gameObject.SetActive(true);
            ErrorTxt[2].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("PW�� ��İ� ��ġ���� �ʽ��ϴ�.(�ڸ��� ����ġ)");
        }

        return isCorrect;
    }



    public bool RePW(string PW = "", string rePW = "")      //��й�ȣ �ߺ� Ȯ��
    {
        bool isCorrect = true;

        if (PW.Equals(rePW))
        {
            ErrorLine[3].gameObject.SetActive(false);
            ErrorTxt[3].gameObject.SetActive(false);
            isCorrect = true;
            Debug.Log("pw�� ��ġ�մϴ�.");
        }
        else
        {
            ErrorLine[3].gameObject.SetActive(true);
            ErrorTxt[3].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("pw�� ��ġ���� �ʽ��ϴ�.");
        }

        return isCorrect;
    }

    public bool ChkEmail(string uEmail = "")  //e-mail ��� Ȯ��
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{1,25}@[a-zA-Z0-9]{1,20}\.[a-zA-Z]{1,5}$"); //�̸��� ���Խ�
        bool isCorrect = true; //���Խ� ���� ��, true

        if ((regex.IsMatch(uEmail)))    //���Խ� ����ġ ��
        {
            Debug.Log("email�� ��İ� ��ġ�մϴ�.");
            ErrorLine[4].gameObject.SetActive(false);
            ErrorTxt[4].gameObject.SetActive(false);
            isCorrect = true;
        }
        
        else
        {
            Debug.Log("email�� ��İ� ��ġ���� �ʽ��ϴ�.");
            ErrorLine[4].gameObject.SetActive(true);
            ErrorTxt[4].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;

    }


//�̹� �����ϴ� ID, Email popup

    public void ExistID(bool isExist)   
    {
        if (isExist)
        {
            Debug.Log("��� ������ id �Դϴ�.");
            ErrorTxt[5].gameObject.SetActive(false);
            ErrorLine[1].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�̹� �����ϴ� id �Դϴ�.");
            ErrorTxt[5].gameObject.SetActive(true);
            ErrorLine[1].gameObject.SetActive(true);
            
        }
        
    }

    public void ExistEmail()
    {
        ErrorTxt[6].gameObject.SetActive(true);
    }

}
