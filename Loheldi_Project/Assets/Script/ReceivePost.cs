using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public class UPostItem
    {
        public string title;
        public string author;
        public string content;
    }
    public void ClickMail()
    {
        BackendReturnObject bro = Backend.UPost.GetPostList(PostType.Admin);
        JsonData json = bro.GetReturnValuetoJSON()["postList"];


        List<UPostItem> postItemList = new List<UPostItem>();
        for(int i = 0; i < json.Count; i++)
        {

            /*UPostItem postItem = new UPostItem();
            postItem.content = json[i]["content"].ToString();
            postItem.title = json[i]["title"].ToString();

            postItemList.Add(postItem);
            Debug.Log(postItemList.Count);*/
            string title = json[0]["inDate"].ToString();
            Debug.Log(title);
        }
        


    }
    public void ReceiveMail()
    {
        PostType type = PostType.Admin;
        BackendReturnObject bro = Backend.UPost.GetPostList(type);
        JsonData json = bro.GetReturnValuetoJSON()["postList"];

        string recentPostIndate = json[0]["inDate"].ToString();

        Backend.UPost.ReceivePostItem(type, recentPostIndate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
