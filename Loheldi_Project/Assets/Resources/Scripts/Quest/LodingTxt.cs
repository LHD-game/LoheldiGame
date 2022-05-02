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
    public GameObject block; //넘김방지 맨앞 블럭
    public GameObject ChatWin;
    public GameObject QuizeWin;
    public GameObject chatCanvus;

    public int NPCButton = 0;
    public string LoadTxt;

    List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;//= "Scripts/Quest/Dialog";
    public string Num;//스크립트 번호
    int j;//data_Dialog 줄갯수
    string Answer;//누른 버튼 인식

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
        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //대화 끝
        {
            chatCanvus.SetActive(false);
            ChatWin.SetActive(false);
            QuizeWin.SetActive(false);
            Arrow.SetActive(false);
            Name.text = " ";
        }
        else
        {
            if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //퀴즈시작
            {
                QuizeTIme();
            }
            else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //퀴즈끝
            {
                ChatTime();
            }

            Debug.Log("j="+j);
            LoadTxt = data_Dialog[j]["dialog"].ToString();
            Name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());
            Arrow.SetActive(false);
            block.SetActive(true);
            if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //퀴즈 선택지
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

    public void Buttons()      //npc대화 상호작용 버튼 수
    {
        //NPCButtons.SetActive(true);
        for (int i= 0; i < NPCButton;i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            Debug.Log("i는"+i+" j는"+j);
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
        LoadTxt = "안녕!";
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

    IEnumerator _typing()  //타이핑 효과
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

