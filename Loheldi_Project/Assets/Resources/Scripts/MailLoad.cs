using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailLoad : MonoBehaviour
{
    public Text RTitleText;                                           //우측에 표시되는 메일 제목
    public Text RDetailText;
    public GameObject ThisTitle;                                      //메일 제목
    public GameObject ThisSent;                                       //보낸 사람
    string Detail1 = "A";                                             //메일 내용 (나중에 서버에서 불러오는방식으로 변경)
    string Detail2 = "B";
    string Detail3 = "C";
    string Detail4 = "D";

     void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText라는 이름에 Text오브젝트를 찾아 RTitleText라고 지정함
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText이라는 이름에 Text오브젝트를 찾아 RDetailText라고 지정함

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
        ThisTitle = this.transform.GetChild(1).gameObject;                  //메일에 제목을 지정함 (나중에 DB에서 불러오는 스크립트 필요)
        ThisSent = this.transform.GetChild(2).gameObject;                   //메일 쓴 사람을 지정함(                ''                )

       /* RTitleText.string = ThisTitle.GetComponent<Text>().text;              //우측에 표시되는 제목을 선택한 제목과 같게 함
        RDetailText.text = Detail1;                */                         //내용을 Detail1으로 바꿈
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
                    Debug.Log("아이템이 없는 우편 수령");
                    continue;
                }
                /*foreach (LitJson.JsonData items in postItems)
                {
                    string itemInfo = string.Empty;

                    if (postType == PostType.User) // 유저만 리턴형식이 다름
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
                        itemInfo += string.Format("아아템 갯수 : {0}\n", items["itemCount"].ToString());
                    }
                    Debug.Log(itemInfo);
                }*/
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
}