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
    public bool Quest = true;
    [SerializeField]
    private int QuestIndex = 0;
    private GameObject NpcQuest;

    public MailLoad Mail;
    private LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;
    public List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();
    public int QuestNumber;
    //1. ���� ����Ʈ ���Լ�
    //2. �Լ��� true�ε� �ð��� 00�� �Ǹ� ����Ʈ �ִ� ��ũ����X
    //3. int�Լ� �ϳ� �ؼ� ��¥++
    //4. 
    // Start is called before the first frame update
    void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();//�̰Ŷ� �ؿ��� ���߿� ���� �׽�Ʈ ������ �а�
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        Quest = true;
        GiveQuest();
        QuestIndex = +1;
        QuestNumber = 0;
        
    }

    private void GiveQuest()
    {
        string title = Quest_Mail[QuestNumber]["title"].ToString();                      
        string detail = Quest_Mail[QuestNumber]["content"].ToString();
        string sent = Quest_Mail[QuestNumber]["author"].ToString();

        GameObject temp = Resources.Load<GameObject>("Prefabs/UI/Mail") as GameObject;

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //���� ������ ����
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
        Mail.ThisSent.GetComponent<Text>().text = sent;
        Mail.ThisDetail.GetComponent<Text>().text = detail;
    }

    public void Quest1()
    {
        NpcQuest = GameObject.Find("Hami");
        LodingTxt Load = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Load.ButtonPlusNpc[0] = "Hami";
        ExclamationMarkCreate();
    }

    private void ClearQuest()
    {
        Quest = false;
    }

    private void ExclamationMarkCreate()
    {
        Instantiate(ExclamationMark[Int32.Parse(Quest_Mail[QuestNumber]["authorNumber"].ToString())], NpcQuest.transform.position+new Vector3(0,5,0),NpcQuest.transform.rotation);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
