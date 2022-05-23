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

    private MailLoad Mail;
    private LodingTxt chat;

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
        
    }

    public void MainQuestLoding()
    {
        Mail = GameObject.Find("chatManager").GetComponent<MailLoad>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        Quest = true;
        QuestIndex = +1;
        GiveQuest();
    }

    private void GiveQuest()
    {
        string title = Quest_Mail[QuestNumber]["title"].ToString();                      //서버에서 불러온 메일에 속성
        string detail = Quest_Mail[QuestNumber]["content"].ToString();
        string sent = Quest_Mail[QuestNumber]["author"].ToString();

        Mail.TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/UI/Mail"), Mail.MailList);                      //메일 프리펩 생성
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
        Mail.ThisSent.GetComponent<Text>().text = sent;
        Mail.ThisDetail.GetComponent<Text>().text = detail;
    }

    private void ClearQuest()
    {
        Quest = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
