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

    public Text RTitleText;                                           //우측에 표시되는 메일 제목
    public Text RDetailText;                                          //우측에 표시되는 메일 내용
    public GameObject ThisTitle;                                      //메일 제목
    public GameObject ThisSent;                                       //보낸 사람
    public GameObject ThisDetail;                                     //메일 내용
    public GameObject ThisType;                                       //메일 타입(서버인지 퀘스트인지)
    public Transform MailList;                                        //매일들이 정렬될 ParentObject
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
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 4);  //서버에서 메일 리스트 불러오기     (한번에 4개씩 보이므로 4의 배수가 좋을듯)
        JsonData json = bro.GetReturnValuetoJSON()["postList"];                  //Json으로 지정

        BackendReturnObject bro1 = Backend.Notice.NoticeList(4);                 //서버에서 공지사항 불러오기
        JsonData json1 = bro1.FlattenRows();                                     //Json1으로 지정

        ReceiveMailButton.SetActive(false);

        if (MailorAnnou)
        {
            Transform[] childList = MailList.GetComponentsInChildren<Transform>(); //과거에 불러왔던 이력 파괴
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
                string title = json[i]["title"].ToString();                      //서버에서 불러온 메일에 속성
                string sent = json[i]["author"].ToString();
                string detail = json[i]["content"].ToString();

                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), MailList);                      //메일 프리펩 생성
                ThisTitle = TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
                ThisSent = TempObject.transform.Find("Sent").gameObject;
                ThisDetail = TempObject.transform.Find("Detail").gameObject;
                TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

                ThisTitle.GetComponent<Text>().text = title;                                                            //프리펩에 속성을 서버에서 불러온 속성으로 바꿈
                ThisSent.GetComponent<Text>().text = sent;
                ThisDetail.GetComponent<Text>().text = detail;
            }
        }
        else if (!MailorAnnou)
        {
            Transform[] childList = NoticeList.GetComponentsInChildren<Transform>(); //과거에 불러왔던 이력 파괴
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
                string noticetitle = json1[i]["title"].ToString();              //서버에서 불러온 공지사항 속성
                string noticesent = json1[i]["author"].ToString();
                string noticedetail = json1[i]["content"].ToString();

                NoticeTempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), NoticeList);                  //메일 프리펩 생성(위에 메일과 같은 프리펩 공유)
                NoticeTitle = NoticeTempObject.transform.Find("Title").gameObject;                                          //프리펩에 속성
                NoticeSent = NoticeTempObject.transform.Find("Sent").gameObject;
                NoticeDetail = NoticeTempObject.transform.Find("Detail").gameObject;
                NoticeTempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.NoticeLoading(); });  //프리펩으로 불러온 버튼의 OnClick()을 NoticeLoading으로 지정

                NoticeTitle.GetComponent<Text>().text = noticetitle;                                                        //프리펩에 속성을 서버에서 불러온 속성으로 바꿈
                NoticeSent.GetComponent<Text>().text = noticesent;
                NoticeDetail.GetComponent<Text>().text = noticedetail;

                List<MailLoad> noticeList = new List<MailLoad>();
            }
        }
    }

    public void MailLoading()
    {
        TempObject = EventSystem.current.currentSelectedGameObject;             //선택한 메일을 TempObject에 저장

        ThisTitle = TempObject.transform.Find("Title").gameObject;              //선택한 프리팹의 제목을 지정
        ThisDetail = TempObject.transform.Find("Detail").gameObject;            //(     ''     )내용을 지정
        RTitleText.text = ThisTitle.GetComponent<Text>().text;                  //우측에 표시되는 제목을 선택한 제목과 같게 함
        RDetailText.text = ThisDetail.GetComponent<Text>().text;                //내용을 프리펩에 속성인 Detail로 바꿈
        ReceiveMailButton.SetActive(true);
    }

    public void NoticeLoading()
    {
        NoticeTempObject = EventSystem.current.currentSelectedGameObject;       //선택한 프리팹을 NoticeTempObject에 저장

        NoticeTitle = NoticeTempObject.transform.Find("Title").gameObject;      //선택한 프리팹의 제목을 지정
        NoticeDetail = NoticeTempObject.transform.Find("Detail").gameObject;    //(      ''     )내용을 지정
        NoticeTitleText.text = NoticeTitle.GetComponent<Text>().text;           //우측에 표시되는 제목을 선택한 제목과 같게 함
        NoticeDetailText.text = NoticeDetail.GetComponent<Text>().text;         //내용을 프리펩에 속성인 Detail로 바꿈

    }

    public void Type_classification()
    {
        ThisType = TempObject.transform.Find("Type").gameObject;            //타입 지정
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
        var BRO = Backend.Chart.GetChartContents("46292"); //서버의 엑셀파일을 불러온다.
        
        Param param = new Param();
        
        var bro = Backend.UPost.ReceivePostItemAll(PostType.Admin);

        if (bro.IsSuccess())
        {
            
            foreach (LitJson.JsonData postItems in bro.GetReturnValuetoJSON()["postItems"])
            {
                if (postItems.Count <= 0)
                {
                    Debug.Log("아이템이 없는 우편 수령");
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
                Debug.LogError("더이상 수령할 우편이 존재하지 않습니다.");
            }
        }
    }

    public void NewMailCheck()
    {
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //서버에서 메일 리스트 불러오기
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
        var BRO = Backend.Chart.GetChartContents(chartNum); //서버의 엑셀파일을 불러온다.
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