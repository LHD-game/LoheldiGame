using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailLoad : MonoBehaviour
{
    public Text RTitleText;                                           //������ ǥ�õǴ� ���� ����
    public Text RDetailText;
    public GameObject ThisTitle;                                      //���� ����
    public GameObject ThisSent;                                       //���� ���
    string Detail1 = "A";                                             //���� ���� (���߿� �������� �ҷ����¹������ ����)
    string Detail2 = "B";
    string Detail3 = "C";
    string Detail4 = "D";

     void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText��� �̸��� Text������Ʈ�� ã�� RTitleText��� ������
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText�̶�� �̸��� Text������Ʈ�� ã�� RDetailText��� ������

        ;
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);
        JsonData json = bro.GetReturnValuetoJSON()["postList"];
        
        if(bro.IsSuccess())
        {
            for (int i = 0; i < json.Count; i++)
            {
                string title = json[i]["title"].ToString();
                string content = json[i]["content"].ToString();

                Debug.Log(title);
                Debug.Log(content);
            }
        }
        

    } 

    public void MailLoading()
    {
        ThisTitle = this.transform.GetChild(1).gameObject;                  //���Ͽ� ������ ������ (���߿� DB���� �ҷ����� ��ũ��Ʈ �ʿ�)
        ThisSent = this.transform.GetChild(2).gameObject;                   //���� �� ����� ������(                ''                )

       /* RTitleText.string = ThisTitle.GetComponent<Text>().text;              //������ ǥ�õǴ� ������ ������ ����� ���� ��
        RDetailText.text = Detail1;                */                         //������ Detail1���� �ٲ�
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