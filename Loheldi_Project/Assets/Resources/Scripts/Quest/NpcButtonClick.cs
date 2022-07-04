using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcButtonClick : MonoBehaviour
{
    [SerializeField]
    Text SecondButtonTxt;
    //private Text TButtonTxt;
    private UIButton UIB;
    private FlieChoice Chat;

    // Start is called before the first frame update

    public void SecondButtonClick()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();

        if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미니게임 하기"))
            SceneLoader.instance.GotoLobby();
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미용실 이용하기"))
            SceneLoader.instance.GotoPlayerCustom();
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("가구점 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.shop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 하미"))
        {
            Chat.Quest();
            CheckQuest();
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 힘찬"))
        {
            Chat.Quest();
            CheckQuest();
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 수호"))
        {
            Chat.Quest();
            CheckQuest();
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 여미"))
        {
            Chat.Quest();
            CheckQuest();
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 메이"))
        {
            Chat.Quest();
            CheckQuest();
        }
    }
    void CheckQuest()
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");

        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
    }
}
