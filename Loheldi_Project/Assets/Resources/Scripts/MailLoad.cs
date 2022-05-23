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
        /*PostType type = PostType.Admin;
        BackendReturnObject bro = Backend.UPost.GetPostList(type);
        JsonData json = bro.GetReturnValuetoJSON()["postList"];

        string recentPostIndate = json[0]["inDate"].ToString();

        Backend.UPost.ReceivePostItem(type, recentPostIndate);*/
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
                /*foreach (LitJson.JsonData items in postItems)
                {
                    string itemInfo = string.Empty;

                    if (postType == PostType.User) // ������ ���������� �ٸ�
                    {
                        foreach (var key in items.Keys)
                        {
                            itemInfo += string.Format("{0} : {1}\n", key, items[key].ToString());
                        }
                    }
                    else
                    {
                        foreach (var key in items["item"].Keys)
                        {
                            itemInfo += string.Format("{0} : {1}\n", key, items["item"][key].ToString());
                        }
                        itemInfo += string.Format("�ƾ��� ���� : {0}\n", items["itemCount"].ToString());
                    }
                    Debug.Log(itemInfo);
                }*/
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
}