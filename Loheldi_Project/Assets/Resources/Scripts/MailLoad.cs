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
    public Text RTitleText;                                           //우측에 표시되는 메일 제목
    public Text RDetailText;                                          //우측에 표시되는 메일 내용
    public GameObject ThisTitle;                                      //메일 제목
    public GameObject ThisSent;                                       //보낸 사람
    public GameObject ThisDetail;                                     //메일 내용
    public Transform MailList;                                        //매일들이 정렬될 ParentObject
    public GameObject TempObject;

    void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText라는 이름에 Text오브젝트를 찾아 RTitleText라고 지정함
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText이라는 이름에 Text오브젝트를 찾아 RDetailText라고 지정함
        MailList = GameObject.Find("MailList").transform;

        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin, 10);  //서버에서 메일 리스트 불러오기
        JsonData json = bro.GetReturnValuetoJSON()["postList"];                   //Json으로 지정
        
        if(bro.IsSuccess())
        {
            for (int i = 0; i < json.Count; i++)
            {
                string title = json[i]["title"].ToString();                      //서버에서 불러온 메일에 속성
                string detail = json[i]["content"].ToString();
                string sent = json[i]["author"].ToString();

                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Mail"), MailList);                      //메일 프리펩 생성
                ThisTitle = TempObject.transform.Find("Title").gameObject;                                              //프리펩에 속성
                ThisSent = TempObject.transform.Find("Sent").gameObject;
                ThisDetail = TempObject.transform.Find("Detail").gameObject;
                TempObject.transform.GetComponent<Button>().onClick.AddListener(delegate { this.MailLoading(); });      //프리펩으로 불러온 버튼의 OnClick()을 MailLoading으로 지정

                ThisTitle.GetComponent<Text>().text = title;                                                            //버튼에 속성을 서버에서 불러온 속성으로 바꿈
                ThisSent.GetComponent<Text>().text = sent;
                ThisDetail.GetComponent<Text>().text = detail;
            }
        }
        

    } 

    public void MailLoading()
    {
        TempObject = EventSystem.current.currentSelectedGameObject;             //선택한 메일을 TempObject에 저장

        ThisTitle = TempObject.transform.Find("Title").gameObject;              //선택한 메일의 제목을 지정
        ThisDetail = TempObject.transform.Find("Detail").gameObject;            //(     ''     )내용을 지정

        RTitleText.text = ThisTitle.GetComponent<Text>().text;                  //우측에 표시되는 제목을 선택한 제목과 같게 함
        RDetailText.text = ThisDetail.GetComponent<Text>().text;                //내용을 Detail1으로 바꿈
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