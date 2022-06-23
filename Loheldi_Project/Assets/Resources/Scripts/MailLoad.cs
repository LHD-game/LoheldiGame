using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MailLoad : MonoBehaviour
{
    public static bool MailorAnnou;

    public Text RTitleText;                                           //������ ǥ�õǴ� ���� ����
    public Text RDetailText;                                          //������ ǥ�õǴ� ���� ����
    public GameObject ThisTitle;                                      //���� ����
    public GameObject ThisSent;                                       //���� ���
    public GameObject ThisDetail;                                     //���� ����
    public GameObject ThisType;                                       //���� Ÿ��(�������� ����Ʈ����)
    public Transform MailList;                                        //���ϵ��� ���ĵ� ParentObject
    public GameObject TempObject;
    public GameObject ReceiveMailButton;

    public Text NoticeTitleText;
    public Text NoticeDetailText;
    public Transform NoticeList;
    public GameObject NoticeTitle;
    public GameObject NoticeSent;
    public GameObject NoticeDetail;
    public GameObject NoticeTempObject;

    public GameObject MailCountImage;
    public Text MailCount;
    public int TotalCount;

    QuestScript Quest;

    Dictionary<string, string> icode = new Dictionary<string, string>();
    Dictionary<string, string> iname = new Dictionary<string, string>();
    Dictionary<string, string> price = new Dictionary<string, string>();

    void Start()
    {
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        MailorAnnou = true;
        NewMailCheck();
        UpdateList();
    }

    public void UpdateList()
    {
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 4);  //�������� ���� ����Ʈ �ҷ�����     (�ѹ��� 4���� ���̹Ƿ� 4�� ����� ������)
        JsonData json = bro.GetReturnValuetoJSON()["postList"];                  //Json���� ����

        BackendReturnObject bro1 = Backend.Notice.NoticeList(4);                 //�������� �������� �ҷ�����
        JsonData json1 = bro1.FlattenRows();                                     //Json1���� ����

        ReceiveMailButton.SetActive(false);

        if (MailorAnnou)
        {
            Transform[] childList = MailList.GetComponentsInChildren<Transform>(); //���ſ� �ҷ��Դ� �̷� �ı�
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            }

            for (int i = 0; i < json.Count; i++)
            {
                string title = json[i]["title"].ToString();                      //�������� �ҷ��� ���Ͽ� �Ӽ�
                string sent = json[i]["author"].ToString();
                string detail = json[i]["content"].ToString();

                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), MailList);                      //���� ������ ����
                ThisTitle = TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
                ThisSent = TempObject.transform.Find("Sent").gameObject;
                ThisDetail = TempObject.transform.Find("Detail").gameObject;
                TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

                ThisTitle.GetComponent<Text>().text = title;                                                            //�����鿡 �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
                ThisSent.GetComponent<Text>().text = sent;
                ThisDetail.GetComponent<Text>().text = detail;
            }
        }
        else if (!MailorAnnou)
        {
            Transform[] childList = NoticeList.GetComponentsInChildren<Transform>(); //���ſ� �ҷ��Դ� �̷� �ı�
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; i++)
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            } 

            for (int i = 0; i < json1.Count; i++)
            {
                string noticetitle = json1[i]["title"].ToString();              //�������� �ҷ��� �������� �Ӽ�
                string noticesent = json1[i]["author"].ToString();
                string noticedetail = json1[i]["content"].ToString();

                NoticeTempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), NoticeList);                  //���� ������ ����(���� ���ϰ� ���� ������ ����)
                NoticeTitle = NoticeTempObject.transform.Find("Title").gameObject;                                          //�����鿡 �Ӽ�
                NoticeSent = NoticeTempObject.transform.Find("Sent").gameObject;
                NoticeDetail = NoticeTempObject.transform.Find("Detail").gameObject;
                NoticeTempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.NoticeLoading(); });  //���������� �ҷ��� ��ư�� OnClick()�� NoticeLoading���� ����

                NoticeTitle.GetComponent<Text>().text = noticetitle;                                                        //�����鿡 �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
                NoticeSent.GetComponent<Text>().text = noticesent;
                NoticeDetail.GetComponent<Text>().text = noticedetail;

                List<MailLoad> noticeList = new List<MailLoad>();
            }
        }
    }

    public void MailLoading()
    {
        TempObject = EventSystem.current.currentSelectedGameObject;             //������ ������ TempObject�� ����

        ThisTitle = TempObject.transform.Find("Title").gameObject;              //������ �������� ������ ����
        ThisDetail = TempObject.transform.Find("Detail").gameObject;            //(     ''     )������ ����
        RTitleText.text = ThisTitle.GetComponent<Text>().text;                  //������ ǥ�õǴ� ������ ������ ����� ���� ��
        RDetailText.text = ThisDetail.GetComponent<Text>().text;                //������ �����鿡 �Ӽ��� Detail�� �ٲ�
        ReceiveMailButton.SetActive(true);
    }

    public void NoticeLoading()
    {
        NoticeTempObject = EventSystem.current.currentSelectedGameObject;       //������ �������� NoticeTempObject�� ����

        NoticeTitle = NoticeTempObject.transform.Find("Title").gameObject;      //������ �������� ������ ����
        NoticeDetail = NoticeTempObject.transform.Find("Detail").gameObject;    //(      ''     )������ ����
        NoticeTitleText.text = NoticeTitle.GetComponent<Text>().text;           //������ ǥ�õǴ� ������ ������ ����� ���� ��
        NoticeDetailText.text = NoticeDetail.GetComponent<Text>().text;         //������ �����鿡 �Ӽ��� Detail�� �ٲ�

    }

    public void Type_classification()
    {
        ThisType = TempObject.transform.Find("Type").gameObject;            //Ÿ�� ����
        Transform type = ThisType.transform.GetChild(0);
        Debug.Log(type.name);
        if (type.name.Equals("Quest"))
        {
            Quest.QuestChoice();
            Quest.Quest = false;
        }
        else
            ReceiveMail();
        UpdateList();
    }
    public void ReceiveMail()
    {
        var BRO = Backend.Chart.GetChartContents("46292"); //������ ���������� �ҷ��´�.
        
        Param param = new Param();
        
        var bro = Backend.UPost.ReceivePostItemAll(PostType.Admin);

        if (bro.IsSuccess())
        {
            
            foreach (LitJson.JsonData postItems in bro.GetReturnValuetoJSON()["postItems"])
            {
                if (postItems.Count <= 0)
                {
                    Debug.Log("�������� ���� ���� ����");
                    continue;
                }
                else
                {
                    if (BRO.IsSuccess())
                    {
                        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
                        if (rows.Count >= 1)
                        {
                            string icode = BRO.FlattenRows()[0]["itemCode"].ToString();
                            string name = BRO.FlattenRows()[0]["name"].ToString();
                            string price = BRO.FlattenRows()[0]["price"].ToString();

                            param.Add("icode", icode);
                            param.Add("name", name);
                            param.Add("price", price);

                            Backend.GameData.Insert("MAILITEM", param);

                        }
                    }
                    Debug.Log(icode);
                    Debug.Log(name);
                    Debug.Log(price);

                }

            }
        }
        else
        {
            if (bro.GetErrorCode() == "NotFoundException")
            {
                Debug.LogError("���̻� ������ ������ �������� �ʽ��ϴ�.");
            }
        }
    }

    public void NewMailCheck()
    {
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //�������� ���� ����Ʈ �ҷ�����
        JsonData json = bro.GetReturnValuetoJSON()["postList"];

        if (Quest.Quest)
            TotalCount = json.Count + 1;
        else if (!Quest.Quest)
            TotalCount = json.Count;

        if (TotalCount != 0)
        {
            MailCountImage.SetActive(true);
            MailCount.text = TotalCount.ToString();
            if (TotalCount >= 10)
            {
                MailCount.text = "9+";
            }
        }
        else if (TotalCount == 0)
        {
            MailCountImage.SetActive(false);
        }
    }

    public void MailReset()
    {
        TotalCount = 0;
        NewMailCheck();
    }


    /*oid GetChartContents(string chartNum)
    {
        var BRO = Backend.Chart.GetChartContents(chartNum); //������ ���������� �ҷ��´�.
        JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
        Param param = new Param();
        if (rows.Count <= 0)
        {
            string icode = BRO.FlattenRows()[0]["itemCode"].ToString();
            string name = BRO.FlattenRows()[0]["name"].ToString();
            string price = BRO.FlattenRows()[0]["price"].ToString();

            param.Add("icode", icode);
            param.Add("name", name);
            param.Add("price", price);

            Backend.GameData.Insert("MAILITEM", param);

        }
    }*/
}