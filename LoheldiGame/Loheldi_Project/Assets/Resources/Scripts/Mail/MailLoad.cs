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

    //���� �˸�
    [SerializeField]
    GameObject mail_alarm_ui;

    //����
    //public GameObject ThisType;                         //���� Ÿ��(�������� ����Ʈ����)
    [SerializeField]
    GameObject c_mail;                          //��ü ���� ����Ʈ content
    [SerializeField]
    GameObject c_announce;                          //��ü �������� ����Ʈ content
    //public GameObject TempObject;
    [SerializeField]
    GameObject AlreadyRecieveBtn;

    [SerializeField]
    GameObject[] RightDetail = new GameObject[4];

    //��������
    public Transform NoticeContent;
    public GameObject NoticeTitle;
    public GameObject NoticeDetail;
    public GameObject NoticeTempObject;
    public List<GameObject> NoticeObjectList;

    public GameObject MailCountImage;
    public Text MailCount;
    public int TotalCount;


    public QuestDontDestroy DontDestroy;

        List<Dictionary<string, object>> quest = new List<Dictionary<string, object>>();
    static List<GameObject> quest_list = new List<GameObject>();   //���� ������Ʈ ��ü�� �����ϴ� ����

    void Start()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        MailorAnnou = true;

        NewMailCheck();
    }

    public void PopMail()
    {
        NewMailCheck();
        AlreadyRecieveBtn.SetActive(true);
        quest.Clear();
        for (int i = 0; i < quest_list.Count; i++)
        {
            Destroy(quest_list[i]);
        }

        for (int i = 0; i < RightDetail.Length; i++)
        {
            Text txt = RightDetail[i].GetComponent<Text>();
            
            if (i == 2)
            {
                if (DontDestroy.SDA)
                {
                    txt.text = "������� ����յռ��� �ֹε��� ���³��̶� ����Ʈ ������ �� �� �����ϴ�.";
                }
                else if(DontDestroy.LastDay == DontDestroy.ToDay)
                {
                    txt.text = "���� ��Ͽ��� �а� ���� ������ �����ϼ���.\n\n<������ ���� ����Ʈ�� ��� �Ϸ��߽��ϴ�.>";
                }
                else
                {
                    txt.text = "���� ��Ͽ��� �а� ���� ������ �����ϼ���.";
                }
            }
            else
            {
                txt.text = "";
            }
        }
        for (int i = 0; i < MailSelect.reward_list.Count; i++)
        {
            Destroy(MailSelect.reward_list[i]);
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
        var myQuest = Backend.GameData.GetMyData("QUEST_INFO", new Where(), 100);
        if (myQuest.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            JsonData rows = myQuest.GetReturnValuetoJSON()["rows"];
            //���� ��� �ʵ� �� ���� �����ܿ� ���� �˸� ��Ȱ��ȭ
            if (rows.Count <= 0)
            {
                mail_alarm_ui.SetActive(false);
            }
            //���� ��� Ȱ��ȭ
            else
            {
                mail_alarm_ui.SetActive(true);
            }
        }
        /*        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //�������� ���� ����Ʈ �ҷ�����
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
                }*/
    }

    public void RecieveMailBtn()    //��ư Ŭ�� ���� �� MainGameManager�� UpdateField() �����ϵ���
    {
        //���� ����
        List<GameObject> reward = MailSelect.reward_list;

        for(int i=0; i<reward.Count; i++)
        {
            GameObject i_code = reward[i].transform.Find("ICode").gameObject;
            Text i_code_txt = i_code.GetComponent<Text>();
            string item_type = i_code_txt.text;

            GameObject amount = reward[i].transform.Find("Amount").gameObject;
            Text amount_txt = amount.GetComponent<Text>();

            if (item_type.Equals("Exp"))  //����ġ
            {
                float exp = float.Parse(amount_txt.text);
                PlayInfoManager.GetExp(exp);
            }
            else if (item_type.Equals("Coin"))   //����
            {
                int coin = int.Parse(amount_txt.text);
                PlayInfoManager.GetCoin(coin);
            }
            else if (item_type.Contains("B"))    //����
            {
                BadgeManager.GetBadge(amount_txt.text);
            }
            else if (item_type.Contains("C"))    //�ǻ�, coin�� �տ��� �̹� �˻������Ƿ� �ɸ��� ���� ���̴�..!
            {
                
            }
            else    //�κ��丮 ������
            {
                string code = i_code_txt.text;
                int am = int.Parse(amount_txt.text);
                SaveInvenItem(code, am);
            }
        }

        //����Ʈ ���̺� ����
        DeleteQuestInfo();
        //���� ������Ʈ
        PopMail();
    }

    void SaveInvenItem(string i_code, int amount)
    {
        //Inventory ���̺� �ҷ��ͼ�, ���⿡ �ش��ϴ� �����۰� ��ġ�ϴ� �ڵ尡 ���� ��� ������ 1�������Ѽ� ������Ʈ

        Where where = new Where();
        where.Equal("ICode", i_code);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //���� ��� ������ �� �߰�
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", i_code);
                param.Add("Amount", amount);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�");
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

                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount += amount;
                Debug.Log(item_amount);

                Param param = new Param();
                param.Add("ICode", i_code);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�");
                }
                else
                {
                    Debug.Log("������ ���� ����");
                }
            }
        }
        
    }

    void DeleteQuestInfo()
    {
        Where where = new Where();
        where.Equal("QID", MailSelect.this_qid);
        var bro = Backend.GameData.GetMyData("QUEST_INFO", where);
        if (bro.IsSuccess() == false)
        {
            Debug.Log("����Ʈ ���� ����");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();
            var delete_bro = Backend.GameData.DeleteV2("QUEST_INFO", rowIndate, Backend.UserInDate);
            if (delete_bro.IsSuccess())
            {
                Debug.Log("����Ʈ ���� ���� �� ���� ����");
            }
            else
            {
                Debug.Log("����Ʈ ���� ����: " + bro.GetMessage());
            }
        }

    }

    public void NoticeLoad()
    {
        BackendReturnObject bro = Backend.Notice.NoticeList(4);

        itemBtn = (GameObject)Resources.Load("Prefabs/UI/Mail");
        if (bro.IsSuccess())
        {
            string offset = bro.LastEvaluatedKeyString();
            if (!string.IsNullOrEmpty(offset))
            {
                Backend.Notice.NoticeList(4, offset);
            }
            JsonData jsonList = bro.FlattenRows();

            NoticeObjectList.Clear();
            Transform[] childList = NoticeContent.GetComponentsInChildren<Transform>();
            if (childList != null)
            {
                for (int i = 1; i <childList.Length; i++)
                {
                    if (childList[i] != transform)
                        Destroy(childList[i].gameObject);
                }
            }

            for (int i = 0; i < jsonList.Count; i++)
            {
                GameObject child = Instantiate(itemBtn);    //create itemBtn instance
                child.transform.SetParent(NoticeContent.transform);  //move instance: child
                                                                //������ �ڽ� ũ�� �缳��
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.localScale = new Vector3(1f, 1f, 1f);

                NoticeObjectList.Add(child);

                //change catalog box qid - ����ǥ QID
                GameObject mail_qid = child.transform.Find("QID").gameObject;
                Text qid_txt = mail_qid.GetComponent<Text>();
                qid_txt.text = "";

                //change catalog box title
                GameObject mail_title = child.transform.Find("Title").gameObject;
                Text title_txt = mail_title.GetComponent<Text>();
                title_txt.text = jsonList[i]["title"].ToString();

                //change catalog box from
                GameObject mail_from = child.transform.Find("From").gameObject;
                Text from_txt = mail_from.GetComponent<Text>();
                from_txt.text = "";

                //change catalog box content
                GameObject mail_content = child.transform.Find("Content").gameObject;
                Text content_txt = mail_content.GetComponent<Text>();
                content_txt.text = jsonList[i]["content"].ToString();
            }
        }

    }
}