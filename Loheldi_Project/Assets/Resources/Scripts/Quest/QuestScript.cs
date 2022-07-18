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

    public MailLoad Mail;
    private LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;
    public List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    private int QuestNum;
    FlieChoice file;

    public QuestDontDestroy Load;
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
        Debug.Log("����Ʈ üũ ����");
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        MainQuestLoding();
    }

    public void MainQuestLoding()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
            Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        }
        Debug.Log("����Ʈ ��ȣ" + Load.QuestIndex);
        GiveQuest();
        
    }
    int Q = 0;
    private void GiveQuest()
    {
        
        for (int k = 0; k <= Quest_Mail.Count; k++)
        {
            if (Quest_Mail[k]["QID"].ToString().Equals(Load.QuestIndex))
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
        string title = Quest_Mail[Q]["QName"].ToString();                      
        string detail = Quest_Mail[Q]["Content"].ToString();
        string sent = Quest_Mail[Q]["authorName"].ToString();
        Load.ButtonPlusNpc = sent;
        GameObject temp = Resources.Load<GameObject>("Prefabs/UI/QuestMail") as GameObject;

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //���� ������ ����
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
        Mail.ThisSent.GetComponent<Text>().text = sent;
        Mail.ThisDetail.GetComponent<Text>().text = detail;

        QuestChoice();
    }

    private void CheckMail()
    {
        Load.QuestMail = true;
        Mail.NewMailCheck();
    }
    public void QuestChoice()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        if (Quest_Mail[Q]["QID"].ToString().Equals("4_1"))
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
        if (SceneManager.GetActiveScene().name == "MainField")
            ExclamationMarkCreate();
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
