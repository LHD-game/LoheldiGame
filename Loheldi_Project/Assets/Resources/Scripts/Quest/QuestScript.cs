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

        if (Load.QuestIndex.Equals("0_1"))
        {
            file.Tutorial();
            Debug.Log("Ʃ�丮��");
        }
        else if (Load.LastDay != Load.ToDay)
            QuestCheck();
    }
    private void QuestCheck()
    {
        /*chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        chat.data_Dialog = CSVReader.Read("Scripts/Quest/script");
        for (int k = 0; k <= chat.data_Dialog.Count; k++)
        {
            //Debug.Log(data_Dialog[k]["scriptNumber"].ToString());
            if (chat.data_Dialog[k]["scriptNumber"].ToString().Equals(Load.QuestIndex))
            {
                chat.j = k;
                if (chat.DontDestroy.tutorialLoading)
                {
                    chat.j += 1;
                }
                break;
            }
            else
            {
                continue;
            }
        }*/
        Debug.Log("����Ʈ üũ ����");
        QuestChoice();
        MainQuestLoding();
        if (!String.IsNullOrEmpty(Load.ButtonPlusNpc))
            ExclamationMarkCreate();
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        //Quest = true;
        Debug.Log("����Ʈ ��ȣ" + Load.QuestIndex);
        //QuestNum = Int32.Parse(Quest_Mail[Load.QuestIndex]["QusetNumber"].ToString()); 
        GiveQuest();
        //QuestNumber = QuestNum-1;
        
    }

    private void GiveQuest()
    {
        int Q=0;
        for (int k = 0; k <= Quest_Mail.Count; k++)
        {
            //Debug.Log(data_Dialog[k]["scriptNumber"].ToString());
            if (chat.data_Dialog[k]["scriptNumber"].ToString().Equals(Load.QuestIndex))
            {
                Q= k;
                break;
            }
            else
            {
                continue;
            }
        }
        Load.QuestMail = true;
        string title = Quest_Mail[Q]["title"].ToString();                      
        string detail = Quest_Mail[Q]["content"].ToString();
        string sent = Quest_Mail[Q]["author"].ToString();

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
        //Mail.NewMailCheck();
    }

    private void CheckMail()
    {
        Load.QuestMail = true;
        Mail.NewMailCheck();
    }
    public void QuestChoice()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        chat.data_Dialog = CSVReader.Read("Scripts/Quest/script");
        for (int k = 0; k <= chat.data_Dialog.Count; k++)
        {
            //Debug.Log(data_Dialog[k]["scriptNumber"].ToString());
            if (chat.data_Dialog[k]["scriptNumber"].ToString().Equals(Load.QuestIndex))
            {
                chat.j = k;
                break;
            }
            else
            {
                continue;
            }
        }
        //Load.RiciveQuest = true;
        string QnpcName= Quest_Mail[chat.j]["author"].ToString(); //����Ʈ �����Ϸ� ã�ư��� �� NPC
        if (Quest_Mail[chat.j]["scriptNumber"].ToString().Equals("4_1")|| Quest_Mail[chat.j]["scriptNumber"].ToString().Equals("4_2"))
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
        //NpcQuest = GameObject.Find(QnpcName);
        Load.ButtonPlusNpc = QnpcName;
        ExclamationMarkCreate();
        //Debug.Log("�³�" + chat.Num);
    }

    private void ClearQuest()
    {
        //Quest = false;
    }

    private void ExclamationMarkCreate()
    {
        Instantiate(ExclamationMark[1], GameObject.Find(Load.ButtonPlusNpc).transform.position+new Vector3(0,6,0), GameObject.Find(Load.ButtonPlusNpc).transform.rotation);
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
