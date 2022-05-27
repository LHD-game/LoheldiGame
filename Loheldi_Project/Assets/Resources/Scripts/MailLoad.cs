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
    public Text RTitleText;                                           //������ ǥ�õǴ� ���� ����
    public Text RDetailText;                                          //������ ǥ�õǴ� ���� ����
    public GameObject ThisTitle;                                      //���� ����
    public GameObject ThisSent;                                       //���� ���
    public GameObject ThisDetail;                                     //���� ����
    public Transform MailList;                                        //���ϵ��� ���ĵ� ParentObject
    public GameObject TempObject;

    public Text NoticeTitleText;
    public Text NoticeSentText;
    public Transform NoticeList;
    public GameObject NoticeTitle;
    public GameObject NoticeSent;
    public GameObject NoticeObject;


    Dictionary<string, string> icode = new Dictionary<string, string>();
    Dictionary<string, string> iname = new Dictionary<string, string>();
    Dictionary<string, string> price = new Dictionary<string, string>();


    void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText��� �̸��� Text������Ʈ�� ã�� RTitleText��� ������
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText�̶�� �̸��� Text������Ʈ�� ã�� RDetailText��� ������
        MailList = GameObject.Find("MailList").transform;

        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //�������� ���� ����Ʈ �ҷ�����
        JsonData json = bro.GetReturnValuetoJSON()["postList"];                   //Json���� ����
        
        if(bro.IsSuccess())
        {
            for (int i = 0; i < json.Count; i++)
            {
                string title = json[i]["title"].ToString();                      //�������� �ҷ��� ���Ͽ� �Ӽ�
                string detail = json[i]["content"].ToString();
                string sent = json[i]["author"].ToString();

                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), MailList);                      //���� ������ ����
                ThisTitle = TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
                ThisSent = TempObject.transform.Find("Sent").gameObject;
                ThisDetail = TempObject.transform.Find("Detail").gameObject;
                TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.MailLoading(); });      //���������� �ҷ��� ��ư�� OnClick()�� MailLoading���� ����

                ThisTitle.GetComponent<Text>().text = title;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
                ThisSent.GetComponent<Text>().text = sent;
                ThisDetail.GetComponent<Text>().text = detail;

                /*NoticeObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), MailList);
                NoticeTitle = NoticeObject.transform.Find("NTitle").gameObject;
                NoticeSent = NoticeSent.transform.Find("NSent").gameObject;
                NoticeObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.MailLoading(); });*/


            }
        }
        

    } 

    public void MailLoading()
    {
        TempObject = EventSystem.current.currentSelectedGameObject;             //������ ������ TempObject�� ����

        ThisTitle = TempObject.transform.Find("Title").gameObject;              //������ ������ ������ ����
        ThisDetail = TempObject.transform.Find("Detail").gameObject;            //(     ''     )������ ����

        RTitleText.text = ThisTitle.GetComponent<Text>().text;                  //������ ǥ�õǴ� ������ ������ ����� ���� ��
        RDetailText.text = ThisDetail.GetComponent<Text>().text;                //������ Detail1���� �ٲ�
    }

    public void ReceiveMail()
    {

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
                    
                    string itemCode = (string)bro.FlattenRows()[0];

                    Param param = new Param();
                    icode.Add ("itemCode", itemCode);
                    

                    param.Add("MailItem", icode);
                    bro = Backend.GameData.Insert("MAILITEM", param);
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


    public void GetNotice()
    {
        /*NoticeTitleText = GameObject.Find("NoticeTitleText").GetComponent<Text>();
        NoticeSentText = GameObject.Find("NoticeSentText").GetComponent<Text>();
        NoticeList = GameObject.Find("NoticeList").transform;*/

        List<MailLoad> noticeList = new List<MailLoad>();

        BackendReturnObject bro = Backend.Notice.NoticeList(10);
        JsonData json = bro.FlattenRows();

        if (bro.IsSuccess())
        {
            Debug.Log("���� ��:" + bro);

            for (int i = 0; i < json.Count; i++)
            {

                string noticetitle = json[i]["title"].ToString();
                string noticesent = json[i]["author"].ToString();
                /*TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/UI/Mail"), NoticeList);
                NoticeTitle = TempObject.transform.Find("Title").gameObject;                                              //�����鿡 �Ӽ�
                NoticeSent = TempObject.transform.Find("Sent").gameObject;

                NoticeTitle.GetComponent<Text>().text = noticetitle;                                                            //��ư�� �Ӽ��� �������� �ҷ��� �Ӽ����� �ٲ�
                NoticeSent.GetComponent<Text>().text = noticesent;*/

                Debug.Log(noticetitle);
                Debug.Log(noticesent);
            }

        }
    }
    public void NoticeLoading()
    {
        NoticeObject = EventSystem.current.currentSelectedGameObject;

        NoticeTitle = TempObject.transform.Find("NTitle").gameObject;                                              //�����鿡 �Ӽ�
        NoticeSent = TempObject.transform.Find("NSent").gameObject;

        NoticeTitleText.text = NoticeTitle.GetComponent<Text>().text;                  //������ ǥ�õǴ� ������ ������ ����� ���� ��
        NoticeSentText.text = NoticeSent.GetComponent<Text>().text;                //������ Detail1���� �ٲ�

    }


    

    
    
}