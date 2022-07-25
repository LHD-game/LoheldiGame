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

    //����
    //public GameObject ThisType;                         //���� Ÿ��(�������� ����Ʈ����)
    [SerializeField]
    GameObject c_mail;                          //��ü ���� ����Ʈ content
    [SerializeField]
    GameObject c_announce;                          //��ü �������� ����Ʈ content
    //public GameObject TempObject;
    //public GameObject ReceiveMailButton;

    //��������
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

    public QuestScript Quest;

/*    Dictionary<string, string> icode = new Dictionary<string, string>();
    Dictionary<string, string> iname = new Dictionary<string, string>();
    Dictionary<string, string> price = new Dictionary<string, string>();

    static string iCode = "";
*/
    List<Dictionary<string, object>> quest = new List<Dictionary<string, object>>();
    static List<GameObject> quest_list = new List<GameObject>();   //���� ������Ʈ ��ü�� �����ϴ� ����

    void Start()
    {
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        MailorAnnou = true;
        
        NewMailCheck();
        //UpdateList();
    }

    public void PopMail()
    {
        quest.Clear();
        for (int i = 0; i < quest_list.Count; i++)
        {
            Destroy(quest_list[i]);
        }

        GetQuestMail();
        MakeCategory(c_mail, quest, quest_list);
    }

    //����Ʈ ������ Quest_INFO ���̺��� �����´�.
    void GetQuestMail()
    {
        var myQuest = Backend.GameData.GetMyData("QUEST_INFO", new Where(), 100);
        JsonData myQuest_rows = myQuest.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();
        for (int i=0; i< myQuest_rows.Count; i++)
        {
            QuestInfo data = pj.ParseBackendData<QuestInfo>(myQuest_rows[i]);
            quest.Add(new Dictionary<string, object>());
            initQuest(quest[i], data);
        }
    }

    void initQuest(Dictionary<string, object> item, QuestInfo data)
    {
        item.Add("QID", data.QID);
        item.Add("QName", data.QName);
        item.Add("From", data.From);
        item.Add("Content", data.Content);
        item.Add("Reward", data.Reward);
        item.Add("authorName", data.authorName);
    }

    GameObject itemBtn;
    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/Mail");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
                                                            //������ �ڽ� ũ�� �缳��
            RectTransform rt = child.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1f, 1f, 1f);

            itemObject.Add(child);

            //change catalog box qid - ����ǥ QID
            GameObject mail_qid = child.transform.Find("QID").gameObject;
            Text qid_txt = mail_qid.GetComponent<Text>();
            qid_txt.text = dialog[i]["QID"].ToString();

            //change catalog box title
            GameObject mail_title = child.transform.Find("Title").gameObject;
            Text title_txt = mail_title.GetComponent<Text>();
            title_txt.text = dialog[i]["QName"].ToString();

            //change catalog box from
            GameObject mail_from = child.transform.Find("From").gameObject;
            Text from_txt = mail_from.GetComponent<Text>();
            from_txt.text = dialog[i]["From"].ToString();

            string content_edit = dialog[i]["Content"].ToString().Replace("<n>","\n");

            //change catalog box content
            GameObject mail_content = child.transform.Find("Content").gameObject;
            Text content_txt = mail_content.GetComponent<Text>();
            content_txt.text = content_edit;
        }
    }

/*
    public void UpdateList()
    {
*//*        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 4);  //�������� ���� ����Ʈ �ҷ�����     (�ѹ��� 4���� ���̹Ƿ� 4�� ����� ������)
        JsonData json = bro.GetReturnValuetoJSON()["postList"];                  //Json���� ����*//*

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
        *//*if (Quest.Load.QuestMail)
            Quest.MainQuestLoding();*//*
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
        if (type.name.Equals("Quest")) //����Ʈ ���� �ޱ� �����κ�
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
                        Debug.Log("Mailgood");

                    }


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

        //Inventory ���̺� �ҷ��ͼ�, ���⿡ �ش��ϴ� �����۰� ��ġ�ϴ� �ڵ尡 ���� ��� ������ 1�������Ѽ� ������Ʈ

        Where where = new Where();
        where.Equal("ICode", iCode);
        var bro2 = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro2.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            JsonData rows = bro2.GetReturnValuetoJSON()["rows"];
            //���� ��� ������ �� �߰�
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", 1);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�: " + iCode);
                }
                else
                {
                    Debug.Log("������ ���� ����");
                }
            }
            //���� ��� �ش� ������ indateã��, ���� ����
            else
            {
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();
                Debug.Log(rowIndate);
                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount++;
                Debug.Log(item_amount);

                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�: " + iCode);
                }
                else
                {
                    Debug.Log("������ ���� ����");
                }
            }
        }
        //todo: ���� ������ �� ������ ����, �˾� ����

    }*/
    

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

    /*public void MailReset()
    {
        RTitleText.text = " ";
        RDetailText.text = " ";
        NoticeTitleText.text = " ";
        NoticeDetailText.text = " ";

        TotalCount = 0;
        NewMailCheck();

        for (int i = 0; i < MailList.transform.childCount; i++)
        {
            Destroy(MailList.transform.GetChild(i).gameObject);
        }
    }*/
}