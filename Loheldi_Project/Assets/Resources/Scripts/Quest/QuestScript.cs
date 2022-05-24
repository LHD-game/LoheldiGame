using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class QuestScript : MonoBehaviour
{
    public bool Quest = true;
    [SerializeField]
    private int QuestIndex = 0;
    private GameObject NpcQuest;

    private MailLoad Mail;
    private LodingTxt chat;


    public GameObject[] ExclamationMark;
    public List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();
    public int QuestNumber;
    //1. 메인 퀘스트 불함수
    //2. 함수가 true인데 시간이 00이 되면 퀘스트 주는 스크립드X
    //3. int함수 하나 해서 날짜++
    //4. 
    // Start is called before the first frame update
    void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Mail = GameObject.Find("chatManager").GetComponent<MailLoad>();//이거랑 밑에꺼 나중에 없앰 테스트 때문에 둔거
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("chatManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        Quest = true;
        GiveQuest();
        QuestIndex = +1;
        
    }

    private void GiveQuest()
    {
        string title = Quest_Mail[QuestNumber]["title"].ToString();                      
        string detail = Quest_Mail[QuestNumber]["content"].ToString();
        string sent = Quest_Mail[QuestNumber]["author"].ToString();

        Mail.TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), Mail.MailList);                      //메일 프리펩 생성
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
