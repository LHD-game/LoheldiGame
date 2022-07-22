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
    private UIButton UIB;
    private FlieChoice Chat;


    public void SecondButtonClick()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();

        if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미니게임 하기"))
            SceneLoader.instance.GotoLobby();
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("미용실 이용하기"))
            SceneLoader.instance.GotoPlayerCustom();
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("의상실 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.clothesShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("가구점 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.GaguShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("마켓 이용하기"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.Market.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("옷장 이용하기"))
        {
            SceneLoader.instance.GotoPlayerCloset();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 하미"))
        {
            Chat.Quest();
            CheckQuest();
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("준비됐어!")||click.transform.GetChild(0).GetComponent<Text>().text.Equals("준비됐어요!"))
        {
            SceneLoader.instance.GotoQuizGame();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 힘찬"))
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
        }else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("퀘스트 요미"))
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
        Chat.EPin.SetActive(false);


        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
    }
}
