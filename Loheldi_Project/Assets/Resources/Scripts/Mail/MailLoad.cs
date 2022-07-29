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

    //메일 알림
    [SerializeField]
    GameObject mail_alarm_ui;

    //우편
    //public GameObject ThisType;                         //메일 타입(서버인지 퀘스트인지)
    [SerializeField]
    GameObject c_mail;                          //전체 메일 리스트 content
    [SerializeField]
    GameObject c_announce;                          //전체 공지사항 리스트 content
    //public GameObject TempObject;
    [SerializeField]
    GameObject AlreadyRecieveBtn;

    //공지사항
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

    List<Dictionary<string, object>> quest = new List<Dictionary<string, object>>();
    static List<GameObject> quest_list = new List<GameObject>();   //우편 오브젝트 객체를 저장하는 변수

    void Start()
    {
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        MailorAnnou = true;
        
        NewMailCheck();
        //UpdateList();
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
        for (int i=0; i< MailSelect.right_detail_text.Count; i++)
        {
            Text txt = MailSelect.right_detail_text[i].GetComponent<Text>();
            txt.text = "";
        }
        for (int i=0;i<MailSelect.reward_list.Count; i++)
        {
            Destroy(MailSelect.reward_list[i]);
        }
        GetQuestMail();
        MakeCategory(c_mail, quest, quest_list);
    }

    //퀘스트 우편을 Quest_INFO 테이블에서 가져온다.
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
                                                            //아이템 박스 크기 재설정
            RectTransform rt = child.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1f, 1f, 1f);

            itemObject.Add(child);

            //change catalog box qid - 꼬리표 QID
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
            Debug.Log("요청 실패");
        }
        else
        {
            JsonData rows = myQuest.GetReturnValuetoJSON()["rows"];
            //없을 경우 필드 상 우편 아이콘에 빨간 알림 비활성화
            if (rows.Count <= 0)
            {
                mail_alarm_ui.SetActive(false);
            }
            //있을 경우 활성화
            else
            {
                mail_alarm_ui.SetActive(true);
            }
        }
        /*        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //서버에서 메일 리스트 불러오기
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

    public void RecieveMailBtn()    //버튼 클릭 수행 후 MainGameManager의 UpdateField() 실행하도록
    {
        //보상 수령
        List<GameObject> reward = MailSelect.reward_list;

        for(int i=0; i<reward.Count; i++)
        {
            GameObject i_code = reward[i].transform.Find("ICode").gameObject;
            Text i_code_txt = i_code.GetComponent<Text>();
            string item_type = i_code_txt.text;

            GameObject amount = reward[i].transform.Find("Amount").gameObject;
            Text amount_txt = amount.GetComponent<Text>();

            if (item_type.Equals("Exp"))  //경험치
            {
                float exp = float.Parse(amount_txt.text);
                PlayInfoManager.GetExp(exp);
            }
            else if (item_type.Equals("Coin"))   //코인
            {
                int coin = int.Parse(amount_txt.text);
                PlayInfoManager.GetCoin(coin);
            }
            else if (item_type.Contains("B"))    //뱃지
            {
                BadgeManager.GetBadge(amount_txt.text);
            }
            else if (item_type.Contains("C"))    //의상, coin은 앞에서 이미 검사했으므로 걸리지 않을 것이다..!
            {
                
            }
            else    //인벤토리 아이템
            {
                string code = i_code_txt.text;
                int am = int.Parse(amount_txt.text);
                SaveInvenItem(code, am);
            }
        }

        //퀘스트 테이블 삭제
        DeleteQuestInfo();
        //메일 업데이트
        PopMail();
    }

    void SaveInvenItem(string i_code, int amount)
    {
        //Inventory 테이블 불러와서, 여기에 해당하는 아이템과 일치하는 코드가 있을 경우 개수를 1증가시켜서 업데이트

        Where where = new Where();
        where.Equal("ICode", i_code);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //없을 경우 아이템 행 추가
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", i_code);
                param.Add("Amount", amount);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("아이템 수령 완료");
                }
                else
                {
                    Debug.Log("아이템 수령 오류");
                }
            }
            //있을 경우 해당 아이템 indate찾고, 개수 수정
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
                    Debug.Log("아이템 수령 완료");
                }
                else
                {
                    Debug.Log("아이템 수령 오류");
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
            Debug.Log("퀘스트 삭제 실패");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();
            var delete_bro = Backend.GameData.DeleteV2("QUEST_INFO", rowIndate, Backend.UserInDate);
            if (delete_bro.IsSuccess())
            {
                Debug.Log("퀘스트 보상 수령 후 삭제 성공");
            }
            else
            {
                Debug.Log("퀘스트 삭제 실패: " + bro.GetMessage());
            }
        }

    }
}