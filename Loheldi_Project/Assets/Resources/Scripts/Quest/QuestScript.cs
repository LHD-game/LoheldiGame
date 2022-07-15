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
    public bool Quest = false;               //현재 받은 퀘스트 메일이 있는지 확인하는 함수 (MailLoad 211참조)
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
    //1. 메인 퀘스트 불함수
    //2. 함수가 true인데 시간이 00이 되면 퀘스트 주는 스크립드X
    //3. int함수 하나 해서 날짜++
    //4. 
    // Start is called before the first frame update
    void Start()
    {
        file = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        Load = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        Debug.Log("퀘스트스크립트스타트실행" + String.IsNullOrEmpty(Load.ButtonPlusNpc) + Load.QuestMail);

        if (Load.QuestIndex.Equals("0_1"))
        {
            file.Tutorial();
            Debug.Log("튜토리얼");
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
        Debug.Log("퀘스트 체크 실행");
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
        Debug.Log("퀘스트 번호" + Load.QuestIndex);
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

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //메일 프리펩 생성
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
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
        string QnpcName= Quest_Mail[chat.j]["author"].ToString(); //퀘스트 실행하러 찾아가야 할 NPC
        if (Quest_Mail[chat.j]["scriptNumber"].ToString().Equals("4_1")|| Quest_Mail[chat.j]["scriptNumber"].ToString().Equals("4_2"))
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
        //NpcQuest = GameObject.Find(QnpcName);
        Load.ButtonPlusNpc = QnpcName;
        ExclamationMarkCreate();
        //Debug.Log("쳇넘" + chat.Num);
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
