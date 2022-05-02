using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    private Text Txt;
    public Text Name;
    public Text chatName;
    public Text QuizName;
    public Text chatTxt;
    public Text QuizTxt;
    public Text QuizButton1;
    public Text QuizButton2;
    public Text QuizButton3;

    public GameObject[] SelecButton=new GameObject[5];
    public Text[] SelecButtonTxt = new Text[5];

    public GameObject Button;
    public GameObject NPCButtons;
    public GameObject OImage;
    public GameObject XImage;

    public GameObject Arrow;
    public GameObject block; //�ѱ���� �Ǿ� ��
    public GameObject ChatWin;
    public GameObject QuizeWin;
    public GameObject chatCanvus;

    public int NPCButton = 0;
    public string LoadTxt;

    List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;//= "Scripts/Quest/Dialog";
    public string Num;//��ũ��Ʈ ��ȣ
    int j;//data_Dialog �ٰ���
    string Answer;//���� ��ư �ν�

    public void NewChat()
    {
        data_Dialog = CSVReader.Read(FileAdress);
        for (int k=0;k<= data_Dialog.Count;k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k;
                chatCanvus.SetActive(true);
                ChatTime();
                Line();
                break;
            }
            else
            {
                continue;
            }
        }
    }

    public void Answer1()
    {
        Answer = "1";
        QuizeAnswer();
    }
    public void Answer2()
    {
        Answer = "2";
        QuizeAnswer();
    }
    public void Answer3()
    {
        Answer = "3";
        QuizeAnswer();
    }
    public void QuizeAnswer()
    {
        if (data_Dialog[j]["answer"].ToString().Equals(Answer))
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
        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //��ȭ ��
        {
            chatCanvus.SetActive(false);
            ChatWin.SetActive(false);
            QuizeWin.SetActive(false);
            Arrow.SetActive(false);
            Name.text = " ";
        }
        else
        {
            if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
            {
                QuizeTIme();
            }
            else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //���
            {
                ChatTime();
            }

            Debug.Log("j="+j);
            LoadTxt = data_Dialog[j]["dialog"].ToString();
            Name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());
            Arrow.SetActive(false);
            block.SetActive(true);
            if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //���� ������
            {
                j--;

                QuizButton1.text = data_Dialog[j + 1]["select"+1].ToString();
                QuizButton2.text = data_Dialog[j + 1]["select"+2].ToString();
                QuizButton3.text = data_Dialog[j + 1]["select3"].ToString();
                Button.SetActive(true);
            }
        }
        j++;
    }
    /*public void LineNPC()
    {
        Debug.Log("LineNPC()");
        chatCanvus.SetActive(true);
        ChatTime();
        block.SetActive(true);
        StartCoroutine(_typing());
    }*/

    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ��ư ��
    {
        //NPCButtons.SetActive(true);
        for (int i= 0; i < NPCButton;i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            Debug.Log("i��"+i+" j��"+j);
            SelecButton[i].SetActive(true);
            SelecButtonTxt[i].text = data_Dialog[j-1][selecNumber].ToString();
        }
    }

    public void QuizeTIme()
    {
        Txt = QuizTxt;
        Name=QuizName;
        ChatWin.SetActive(false);
        QuizeWin.SetActive(true);
    }

    public void ChatTime()
    {
        Txt = chatTxt;
        Name=chatName;
        ChatWin.SetActive(true);
        QuizeWin.SetActive(false);
    }

    /*public void bye()
    {
        block.SetActive(true);
        LoadTxt = "�ȳ�!";
        StartCoroutine(_typing());

    }
    public void finish()
    {
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Arrow.SetActive(false);
        Name.text = " ";
    }*/

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        Txt.text = " ";
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

