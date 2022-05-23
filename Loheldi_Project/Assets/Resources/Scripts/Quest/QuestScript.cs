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
    //1. ���� ����Ʈ ���Լ�
    //2. �Լ��� true�ε� �ð��� 00�� �Ǹ� ����Ʈ �ִ� ��ũ����X
    //3. int�Լ� �ϳ� �ؼ� ��¥++
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
        string title = Quest_Mail[QuestNumber]["title"].ToString();                      //�������� �ҷ��� ���Ͽ� �Ӽ�
        string detail = Quest_Mail[QuestNumber]["content"].ToString();
        string sent = Quest_Mail[QuestNumber]["author"].ToString();

        Mail.TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/UI/Mail"), Mail.MailList);                      //���� ������ ����
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
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
