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

            GameObject mail_reward = child.transform.Find("Reward").gameObject;
            Text reward_txt = mail_reward.GetComponent<Text>();
            reward_txt.text = dialog[i]["Reward"].ToString();
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

    public void RecieveMailBtn()
    {
        //���� ����
        List<GameObject> reward = MailSelect.reward_list;


        //����Ʈ ���̺� ����
        DeleteQuestInfo();
    }

    void DeleteQuestInfo()
    {

    }
}