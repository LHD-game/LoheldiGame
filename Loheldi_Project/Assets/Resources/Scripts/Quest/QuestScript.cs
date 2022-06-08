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

    private int QuestNum;
    //1. 메인 퀘스트 불함수
    //2. 함수가 true인데 시간이 00이 되면 퀘스트 주는 스크립드X
    //3. int함수 하나 해서 날짜++
    //4. 
    // Start is called before the first frame update
    void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();//이거랑 밑에꺼 나중에 없앰 테스트 때문에 둔거
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        Quest = true;
        Debug.Log(QuestIndex);
        QuestNum = Int32.Parse(Quest_Mail[QuestIndex]["QusetNumber"].ToString()); ;
        GiveQuest();
        QuestIndex++;
        //QuestNumber = QuestNum-1;
        Debug.Log(QuestNum);
        
    }

    private void GiveQuest()
    {
        string title = Quest_Mail[QuestNum-1]["title"].ToString();                      
        string detail = Quest_Mail[QuestNum-1]["content"].ToString();
        string sent = Quest_Mail[QuestNum-1]["author"].ToString();

        GameObject temp = Resources.Load<GameObject>("Prefabs/UI/QuestMail") as GameObject;

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //메일 프리펩 생성
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
        Mail.ThisSent.GetComponent<Text>().text = sent;
        Mail.ThisDetail.GetComponent<Text>().text = detail;
    }

    public void QuestChoice()
    {
        string QnpcName="";
        switch (QuestNum)
        {
            case 1:
                QnpcName = "Hami";
                chat.Num = "1-1";
                break;
            case 2:
                QnpcName = "Himchan";
                chat.Num = "2-1";
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
        NpcQuest = GameObject.Find(QnpcName);
        LodingTxt Load = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Load.ButtonPlusNpc[0] = QnpcName;
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
