using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    private Text Txt;
    public Text name;
    public Text chatTxt;
    public Text QuizeTxt;
    public Text QuizeButton1;
    public Text QuizeButton2;
    public Text QuizeButton3;

    public GameObject Button;
    public GameObject OImage;
    public GameObject XImage;

    public GameObject Arrow;
    public GameObject block; //�ѱ���� �Ǿ� ��
    public GameObject ChatWin;
    public GameObject QuizeWin;

    string LoadTxt;

    List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;//= "Scripts/Quest/Dialog";
    
    int j;//data_Dialog �ٰ���
    string i;//���� ��ư �ν�

    public void NewChat()
    {
        j = 0;
        data_Dialog = CSVReader.Read(FileAdress);
        ChatTime();
    }

    public void Answer1()
    {
        i = "1";
        QuizeAnswer();
    }
    public void Answer2()
    {
        i = "2";
        QuizeAnswer();
    }
    public void Answer3()
    {
        i = "3";
        QuizeAnswer();
    }
    public void QuizeAnswer()
    {
        if (i==data_Dialog[j]["answer"].ToString())
        {
            O();
            Line();
        }

        else
        {
            X();
        }
    }
    public void O()//���� ����� ��
    {
        block.SetActive(true);
        OImage.SetActive(true);
        Button.SetActive(false);
        j++;

    }
    public void X()//Ʋ�� ���� ����� ��
    {
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt= "�ٽ� �����غ���!";
        StartCoroutine(_typing());

    }

    public void Xback()//X�̹��� ��ư
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }

    public void Line()  //�ٳѱ�
    {
        if (j == data_Dialog.Count)
        {
            ChatWin.SetActive(false);
            Arrow.SetActive(false);
            Txt.text = " ";
            name.text = " ";
        }
        else
        {
            if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))
            {
                QuizeTIme();
                Debug.Log("����Ž");
            }
            else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))
            {
                ChatTime();
                Debug.Log("�������");
            }

            LoadTxt = data_Dialog[j]["dialog"].ToString();
            name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());
            Arrow.SetActive(false);
            block.SetActive(true);
            if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))
            {
                j--;
                Debug.Log("������");

                QuizeButton1.text = data_Dialog[j + 1]["select1"].ToString();
                QuizeButton2.text = data_Dialog[j + 1]["select2"].ToString();
                QuizeButton3.text = data_Dialog[j + 1]["select3"].ToString();
                Button.SetActive(true);
            }
        }
        j++;
    }

    public void QuizeTIme()
    {
        Txt = QuizeTxt;
        Txt.text = " ";
        ChatWin.SetActive(false);
        QuizeWin.SetActive(true);
    }

    public void ChatTime()
    {
        Txt = chatTxt;
        Txt.text = " ";
        ChatWin.SetActive(true);
        QuizeWin.SetActive(false);
    }

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        block.SetActive(false);
        //Arrow.SetActive(true);
    }
}

