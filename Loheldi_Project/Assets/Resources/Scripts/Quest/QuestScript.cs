using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class QuestScript : MonoBehaviour
{
    public Transform QMailList;
    public bool Quest = false;               //���� ���� ����Ʈ ������ �ִ��� Ȯ���ϴ� �Լ� (MailLoad 211����)
    [SerializeField]
    //private GameObject NpcQuest;

    public MailLoad Mail;
    private LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;
    public List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    //public int QuestNumber;


    private int QuestNum;
    FlieChoice file;

    public QuestDontDestroy Load;
    //1. ���� ����Ʈ ���Լ�
    //2. �Լ��� true�ε� �ð��� 00�� �Ǹ� ����Ʈ �ִ� ��ũ����X
    //3. int�Լ� �ϳ� �ؼ� ��¥++
    //4. 
    // Start is called before the first frame update
    void Start()
    {
        file = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        Load = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        Debug.Log("����Ʈ��ũ��Ʈ��ŸƮ����" + String.IsNullOrEmpty(Load.ButtonPlusNpc) + Load.QuestMail);
        if (Load.LastDay != Load.ToDay)
            QuestCheck();
    }
    private void QuestCheck()
    {
        Debug.Log("����Ʈ üũ ����");
        if (Load.QuestIndex < 11)
        {
            if (Load.QuestIndex == 0)
            {
                Load.Quest = false;
                file.Tutorial();
            }
            else
            {
                MainQuestLoding();
                if (!String.IsNullOrEmpty(Load.ButtonPlusNpc))
                    ExclamationMarkCreate();
            }
        }
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        //Quest = true;
        Debug.Log("����Ʈ ��ȣ" + Load.QuestIndex);
        QuestNum = Int32.Parse(Quest_Mail[Load.QuestIndex]["QusetNumber"].ToString()); 
        GiveQuest();
        //QuestNumber = QuestNum-1;
        
    }

    private void GiveQuest()
    {
        Load.QuestMail = true;
        string title = Quest_Mail[QuestNum - 1]["title"].ToString();                      
        string detail = Quest_Mail[QuestNum - 1]["content"].ToString();
        string sent = Quest_Mail[QuestNum - 1]["author"].ToString();

        GameObject temp = Resources.Load<GameObject>("Prefabs/UI/QuestMail") as GameObject;

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //���� ������ ����
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
        Mail.ThisSent.GetComponent<Text>().text = sent;
        Mail.ThisDetail.GetComponent<Text>().text = detail;

        //if (!Load.QuestMail)
        Mail.NewMailCheck();
    }

    private void CheckMail()
    {
        Load.QuestMail = true;
        Mail.NewMailCheck();
    }
    public void QuestChoice()
    {
        //Load.RiciveQuest = true;
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        string QnpcName="";
        switch (QuestNum)
        {
            case 1:
                QnpcName = "Hami";
                chat.Num = "1_1";
                break;
            case 2:
                QnpcName = "Himchan";
                chat.Num = "2_1";
                break;
            case 3:
                QnpcName = "Suho";
                chat.Num = "3-1";
                break;
            case 4:
                QnpcName = "Himchan";
                chat.Num = "4-1";
                break;
            case 5 :
                QnpcName = "Yeomi";
                chat.Num = "5-1";
                break;
            case 6:
                QnpcName = "Hami";
                chat.Num = "6-1";
                break;
            case 7:
                QnpcName = "Himchan";
                chat.Num = "7-1";
                break;
            case 8:
                QnpcName = "Mei";
                chat.Num = "8-1";
                break;
            case 9:
                QnpcName = "Himchan";
                chat.Num = "9-1";
                break;
            case 10:
                QnpcName = "Suho";
                chat.Num = "10-1";
                break;
        }
        //NpcQuest = GameObject.Find(QnpcName);
        Load.ButtonPlusNpc = QnpcName;
        ExclamationMarkCreate();
        Debug.Log("�³�" + chat.Num);
    }

    private void ClearQuest()
    {
        //Quest = false;
    }

    private void ExclamationMarkCreate()
    {
        Instantiate(ExclamationMark[1], GameObject.Find(Load.ButtonPlusNpc).transform.position+new Vector3(0,5,0), GameObject.Find(Load.ButtonPlusNpc).transform.rotation);
    }

    public void ChangeDrawCamera()
    {
        if (DrawCamera.enabled == false)
        {
            Draw=true;
            MainCamera.enabled = false;
            DrawCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            DrawCamera.enabled = false;
        }
    }
}
