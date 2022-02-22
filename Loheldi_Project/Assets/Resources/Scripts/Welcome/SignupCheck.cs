using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class SignupCheck
{
    public bool ChkName(string uName)   //�̸� 2~6�� �̳� �ѱ�/����, ���� ������
    {
        Regex regex = new Regex(@"[a-zA-Z��-�R]{2,6}$"); //�̸� ���Խ�. ����ҹ���, �ѱ� 2~6�� ����
        bool isCorrect = true; //�ѱ�, ����θ� �̷����+���� �������� �� true

        if ((regex.IsMatch(uName)))    //���Խ� ����ġ ��
        {
            Debug.Log("�̸��� ��İ� ��ġ�մϴ�.");
            isCorrect = true;
        }
        else
        {
            Debug.Log("�̸��� ��İ� ��ġ���� �ʽ��ϴ�.");
            isCorrect = false;
        }

        return isCorrect;
    }

    public bool ChkID(string uID)     //ID 5~20�� �̳� ����, ����
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{5,20}$"); //ID ���Խ�. ����ҹ���, ���� 5~20�� �̳� ����
        bool isCorrect = true; //���Խ� ���� ��, true

        if ((regex.IsMatch(uID)))    //���Խ� ����ġ ��
        {
            Debug.Log("ID�� ��İ� ��ġ�մϴ�.");
            isCorrect = true;
        }
        else
        {
            Debug.Log("ID�� ��İ� ��ġ���� �ʽ��ϴ�.");
            isCorrect = false;
        }
        return isCorrect;
    }

    public bool ChkPW(string uPW)     //��й�ȣ 20�� �̳� ����+����+Ư������ ����
    {
        bool isCorrect = true; //���Խ� ���� ��, true

        //����1�̻�, ����1�̻�, Ư������1�̻�
        Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$", RegexOptions.IgnorePatternWhitespace);

        if (uPW.Length >= 5 && uPW.Length <= 20)
        {
            if (regex.IsMatch(uPW))
            {
                isCorrect = true;
                Debug.Log("PW�� ��İ� ��ġ�մϴ�.");
            }
            else
            {
                isCorrect = false;
                Debug.Log("PW�� ��İ� ��ġ���� �ʽ��ϴ�.2");
            }
        }
        else
        {
            isCorrect = false;
            Debug.Log("PW�� ��İ� ��ġ���� �ʽ��ϴ�.1");
        }

        return isCorrect;
    }

    public bool RePW(string PW, string rePW)      //��й�ȣ �ߺ� Ȯ��
    {
        bool isCorrect = true;
        if (PW.Equals(rePW))
        {
            isCorrect = true;
            Debug.Log("pw�� ��ġ�մϴ�.");
        }
        else
        {
            isCorrect = false;
            Debug.Log("pw�� ��ġ���� �ʽ��ϴ�.");
        }

        return isCorrect;
    }

    public bool ChkEmail(string uEmail)  //e-mail ��� Ȯ��
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{1,20}@[a-zA-Z0-9]{1,20}.[a-zA-Z]{1,5}$"); //�̸��� ���Խ�
        bool isCorrect = true; //���Խ� ���� ��, true

        if ((regex.IsMatch(uEmail)))    //���Խ� ����ġ ��
        {
            Debug.Log("email�� ��İ� ��ġ�մϴ�.");
            isCorrect = true;
        }
        else
        {
            Debug.Log("email�� ��İ� ��ġ���� �ʽ��ϴ�.");
            isCorrect = false;
        }
        return isCorrect;

    }

    /*
//�ߺ� Ȯ�� ID, Email

public bool ExistID()   
{

}

public bool ExistEmail()
{

}*/

}
