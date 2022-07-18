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
        Debug.Log("퀘스트 체크 실행");
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
        Debug.Log("퀘스트 번호" + Load.QuestIndex);
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

        Mail.TempObject = Instantiate(temp, Mail.MailList);                      //메일 프리펩 생성
        Mail.ThisTitle = Mail.TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
        Mail.ThisSent = Mail.TempObject.transform.Find("Sent").gameObject;
        Mail.ThisDetail = Mail.TempObject.transform.Find("Detail").gameObject;
        Mail.TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.Mail.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

        Mail.ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
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
