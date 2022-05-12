using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LodingTxt : MonoBehaviour
{
    private Text Txt;
    public Text Name;
    public Text chatName;
    public Text QuizName;
    public Text chatTxt;
    public Text QuizTxt;
    public Text[] QuizButton=new Text[3];

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
    private int h;  //이미지 넣을 번호
    private int n;  //뜨는 이미지 번호(스크립트 상)
    public static int k; //npc
    public int l; //뜨는 이미지 번호(기본 대화)
    string Answer;//누른 버튼 인식

    public static GameObject[] CCImage; //캐릭터 이미지
    public static Sprite[] CCImageList;
    static Image spriteR;

    UIButton JumpButtons;
    //Interaction Inter;

    private void Awake()
    {
        //Inter = GameObject.Find("Player").GetComponent<Interaction>(); //안쓰는거
        ChatWin.SetActive(true);
        QuizeWin.SetActive(true);
        if (SceneManager.GetActiveScene().name == "MainField")
            JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        CCImage = GameObject.FindGameObjectsWithTag("CCImage"); //뱃지 태그 저장
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //이미지 경로

        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Debug.Log("이미지 리스트 갯수"+CCImageList.Length);
        Debug.Log("이미지 스프라이트 오브젝트: "+CCImage.Length);

    }
    public void NewChat()
    {
        //h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //이미지 넣을 곳 리스트 번호
        //n = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //이미지 번호(기본대화)
        if (SceneManager.GetActiveScene().name == "MainField")
            JumpButtons.JumpButtons.SetActive(false);
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
        /*h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //이미지 넣을 곳 리스트 번호
        n = Int32.Parse(data_Dialog[j]["image"].ToString()); //이미지 번호(기본대화)*/
        spriteR = CCImage[0].GetComponent<Image>();     //이미지 넣을 곳 0에다 h넣기
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        Debug.Log("이미지번호="+l);
        spriteR.sprite = CCImageList[l];

        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //대화 끝
        {
            ChatEnd();
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
            /*if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //퀴즈 선택지/ 이거 아래로 내려감 이건 흔적
            {
                j--;
                for (int i = 0; i < NPCButton; i++)
                {
                    QuizButton[i].text = data_Dialog[j + 1]["select"+(i+1)].ToString();
                    string selecNumber = "select" + (i + 1).ToString();
                }
                Button.SetActive(true);
            }*/
        }
        j++;
    }

    public void ChatEnd() //리셋
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            JumpButtons.JumpButtons.SetActive(true);
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Arrow.SetActive(false);
        NPCButtons.SetActive(false);
        Name.text = " ";
        for (int i = 0; i < NPCButton; i++)
        {
            string selecNumber = "select" + (i + 1).ToString();
            SelecButton[i].SetActive(false);
            SelecButtonTxt[i].text = data_Dialog[j - 1][selecNumber].ToString();
        }
    }

    public void Buttons()      //npc대화 상호작용 선택지 수
    {
        NPCButtons.SetActive(true);
        for (int i= 0; i < NPCButton;i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(true);
            SelecButtonTxt[i].text = data_Dialog[j-1][selecNumber].ToString();
        }
    }

    public void QuizeTIme()
    {
        k = 1;
        Txt = QuizTxt;
        Name=QuizName;
        ChatWin.SetActive(false);
        QuizeWin.SetActive(true);
    }

    public void ChatTime()
    {
        k = 0;
        Txt = chatTxt;
        Name=chatName;
        ChatWin.SetActive(true);
        QuizeWin.SetActive(false);
    }

    IEnumerator _typing()  //타이핑 효과
    {
        
        Txt.text = " ";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        //Arrow.SetActive(true);

        if (data_Dialog[j-1]["scriptType"].ToString().Equals("choice"))  //선택지
        {
            j--;
            for (int i = 0;i<QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
            Button.SetActive(true);
        }

        block.SetActive(false);
    }
}

