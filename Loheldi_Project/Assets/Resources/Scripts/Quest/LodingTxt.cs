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
    private int h;  //�̹��� ���� ��ȣ
    private int n;  //�ߴ� �̹��� ��ȣ(��ũ��Ʈ ��)
    public static int k; //npc
    public int l; //�ߴ� �̹��� ��ȣ(�⺻ ��ȭ)
    string Answer;//���� ��ư �ν�

    public static GameObject[] CCImage; //ĳ���� �̹���
    public static Sprite[] CCImageList;
    static Image spriteR;

    UIButton JumpButtons;
    //Interaction Inter;

    private void Awake()
    {
        //Inter = GameObject.Find("Player").GetComponent<Interaction>(); //�Ⱦ��°�
        ChatWin.SetActive(true);
        QuizeWin.SetActive(true);
        if (SceneManager.GetActiveScene().name == "MainField")
            JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        CCImage = GameObject.FindGameObjectsWithTag("CCImage"); //���� �±� ����
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //�̹��� ���

        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Debug.Log("�̹��� ����Ʈ ����"+CCImageList.Length);
        Debug.Log("�̹��� ��������Ʈ ������Ʈ: "+CCImage.Length);

    }
    public void NewChat()
    {
        //h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ���� �� ����Ʈ ��ȣ
        //n = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ��ȣ(�⺻��ȭ)
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
        /*h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ���� �� ����Ʈ ��ȣ
        n = Int32.Parse(data_Dialog[j]["image"].ToString()); //�̹��� ��ȣ(�⺻��ȭ)*/
        spriteR = CCImage[0].GetComponent<Image>();     //�̹��� ���� �� 0���� h�ֱ�
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        Debug.Log("�̹�����ȣ="+l);
        spriteR.sprite = CCImageList[l];

        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //��ȭ ��
        {
            ChatEnd();
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
            /*if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //���� ������/ �̰� �Ʒ��� ������ �̰� ����
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

    public void ChatEnd() //����
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

    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ������ ��
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

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        
        Txt.text = " ";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        //Arrow.SetActive(true);

        if (data_Dialog[j-1]["scriptType"].ToString().Equals("choice"))  //������
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

