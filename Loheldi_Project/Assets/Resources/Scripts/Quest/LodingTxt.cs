using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    private Text Txt;
    private Text Name;
    public Text chatName;
    public Text QuizName;
    public Text chatTxt;
    public Text QuizTxt;
    public Text QuizButton1;
    public Text QuizButton2;
    public Text QuizButton3;

    public GameObject Button;
    public GameObject OImage;
    public GameObject XImage;

    public GameObject Arrow;
    public GameObject block; //넘김방지 맨앞 블럭
    public GameObject ChatWin;
    public GameObject QuizeWin;
    public GameObject chatCanvus;

    string LoadTxt;

    List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;//= "Scripts/Quest/Dialog";
    public string Num;//스크립트 번호
    int j;//data_Dialog 줄갯수
    string i;//누른 버튼 인식

    public void NewChat()
    {
        Debug.Log(Num);
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
        /*if(data_Dialog[j]["scriptNumber"].ToString().Equals(Num))
        {
            j = 0;
            data_Dialog = CSVReader.Read(FileAdress);
            chatCanvus.SetActive(true);
            ChatTime();
        }
        else
        {
            
        }
        j = 0;
        data_Dialog = CSVReader.Read(FileAdress);
        chatCanvus.SetActive(true);
        ChatTime();*/
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
    public void O()//정답 골랐을 때
    {
        block.SetActive(true);
        OImage.SetActive(true);
        Button.SetActive(false);
        j++;

    }
    public void X()//틀린 정답 골랐을 때
    {
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt= "다시 생각해보자!";
        StartCoroutine(_typing());

    }

    public void Xback()//X이미지 버튼
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }

    public void Line()  //줄넘김
    {
        if (data_Dialog[j]["scriptType"].ToString().Equals("end"))
        {
            chatCanvus.SetActive(false);
            ChatWin.SetActive(false);
            QuizeWin.SetActive(false);
            Arrow.SetActive(false);
            Txt.text = " ";
            Name.text = " ";
        }
        else
        {
            if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))
            {
                QuizeTIme();
            }
            else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))
            {
                ChatTime();
            }

            Debug.Log(j);
            LoadTxt = data_Dialog[j]["dialog"].ToString();
            Name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());
            Arrow.SetActive(false);
            block.SetActive(true);
            if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))
            {
                j--;

                QuizButton1.text = data_Dialog[j + 1]["select1"].ToString();
                QuizButton2.text = data_Dialog[j + 1]["select2"].ToString();
                QuizButton3.text = data_Dialog[j + 1]["select3"].ToString();
                Button.SetActive(true);
            }
        }
        j++;
    }

    public void QuizeTIme()
    {
        Txt = QuizTxt;
        Name=QuizName;
        Txt.text = " ";
        ChatWin.SetActive(false);
        QuizeWin.SetActive(true);
    }

    public void ChatTime()
    {
        Txt = chatTxt;
        Name=chatName;
        Txt.text = " ";
        ChatWin.SetActive(true);
        QuizeWin.SetActive(false);
    }

    IEnumerator _typing()  //타이핑 효과
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

