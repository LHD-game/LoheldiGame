//CreateAcc Scene���� ���� ������ �����ϱ� ���� ��ũ��Ʈ

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
    private GameObject NariField;   //������ ����
    [SerializeField]
    private GameObject NickNameField;   //�г��� �Է� ȭ��
    [SerializeField]
    private GameObject BirthField;   //������� �Է� ȭ��
    [SerializeField]
    private GameObject ResultField;   //�ӽ� �� Ȯ�ο�

    [SerializeField]
    private InputField InputNickName;   //���� �г���
    [SerializeField]
    private Dropdown[] InputBirth = new Dropdown[3];      //������ ����,��,��

    public static string uNickName;   // ������ ����Ǳ� �� ���� ��Ƴ��� ����
    public static DateTime uBirth;    // ���� ����

    public Text nick;
    public Text birth;

    public static bool nari_can_talk = true;

    void Start()
    {
        NariField.SetActive(true);
        NickNameField.SetActive(true);
        BirthField.SetActive(false);
        ResultField.SetActive(false);
    }

    private void Update()
    {
        //�Է� Ȯ�ο� ��� ���
        if (ResultField.activeSelf)
        {
            nick.text = "�г���: " + uNickName;
            birth.text = "�������: " + uBirth.ToString("yyyy�� M�� d��");
        }
    }

    public void SaveNickName()  //�г��� �Է� �� ��ư�� ������ ��� ����
    {
        Regex regex = new Regex(@"[a-zA-Z��-�R0-9]{2,8}$"); //�г��� ���Խ�. ����ҹ���, �ѱ� 2~8�� ����

        if ((regex.IsMatch(InputNickName.text))) //���Խ� ��ġ��,
        {
            uNickName = InputNickName.text; //uNickName ������ �Է°��� �����ϰ�,
            
            ShowNHide(BirthField, NickNameField);   //�г��� �Է� ��Ȱ��ȭ, ���� �Է� Ȱ��ȭ
            nari_can_talk = true;
        }
        else    //���Խ� ����ġ��
        {
            //���� �˾� Ȱ��ȭ
            Transform t = NickNameField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    public void SaveBirth() //������� �Է� �� ��ư�� ������ ��� ����
    {
        Regex regex = new Regex(@"[0-9]{1,5}$"); //������� ���Խ�
        bool isOK = true;
        for (int i=0; i<InputBirth.Length; i++)
        {
            string birthValue = InputBirth[i].options[InputBirth[i].value].text;
            if (!(regex.IsMatch(birthValue)))  //���Խ� ����ġ ��,
            {
                isOK = false;
                Debug.Log(birthValue);
                nari_can_talk = false;
            }
        }

        if (isOK)   //��� ���Խ� ��ġ�ϸ�
        {
            string str = InputBirth[0].options[InputBirth[0].value].text + "/";
            str += InputBirth[1].options[InputBirth[1].value].text + "/";
            str += InputBirth[2].options[InputBirth[2].value].text; //yyyy/MM/dd
            Debug.Log(str);
            uBirth = Convert.ToDateTime(str);   //uBirth ������ �Է°� ����
            ShowNHide(ResultField, BirthField);
            nari_can_talk = true;
        }
        else    //���Խ� ����ġ��
        {
            //���� �˾� Ȱ��ȭ
            Transform t = BirthField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    private void ShowNHide(GameObject show, GameObject hide)    //Ȱ��ȭ �� �� ù��°, ��Ȱ��ȭ �� ���� �ι�° ���ڷ� �ش�.
    {
        show.SetActive(true);
        hide.SetActive(false);
    }

    public void ClosePop(GameObject go) //���� �˾� �ݱ� �޼ҵ�
    {
        go.SetActive(false);
    }

    //���������� ������ �г��Ӱ� ��������� �����ϴ� �޼ҵ�
    public void AccSave()
    {
        Param param = new Param();
        param.Add("BIRTH", uBirth);
        param.Add("NICKNAME", uNickName);


        var bro = Backend.GameData.Insert("ACC_INFO", param);

        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ���� �Ϸ�!");
            
            Save_BasicCustom.SaveBasicClothes(); //�⺻ �ǻ� ������ ����
            SceneLoader.instance.GotoPlayerCustom();    //ĳ���� Ŀ���͸���¡ ������ �̵�
        }
        else
        {
            Debug.Log("���� ���� ���� ����!");
            //todo: ���� ���� �ȳ� ���� ����
        }
    }
}
