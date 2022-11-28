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
    public FlieChoice Chat;
    public GameObject ParentscheckUI;
    public GameObject ThankTreeUI;


    public void SecondButtonClick()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();

        if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�̴ϰ��� �ϱ�"))
            SceneLoader.instance.GotoLobby();
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�̿�� �̿��ϱ�"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.HairShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�ǻ�� �̿��ϱ�"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.clothesShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("������ �̿��ϱ�"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.GaguShop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("���� �̿��ϱ�"))
        {
            Chat.chat.Main_UI.SetActive(true);
            UIB.Market.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("���� �̿��ϱ�"))
        {
            SceneLoader.instance.GotoPlayerCloset();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�Ӹ� �ٽ��ϱ�"))
        {
            SceneLoader.instance.GotoPlayerCustom();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ �Ϲ�"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�غ�ƾ�!") || click.transform.GetChild(0).GetComponent<Text>().text.Equals("�غ�ƾ��!"))
        {
            SceneLoader.instance.GotoQuizGame();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ����"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ��ȣ"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ����"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ���"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ����"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("����Ʈ ����"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�̱��ϱ�"))
        {
            SceneLoader.instance.GotoGacha();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�������!"))
        {
            Chat.Quest();
            CheckQuest();
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("�̼� �����ϱ�"))
        {
            UIB.chat.ChatEnd();
            ParentscheckUI.SetActive(true);
        }
        else if (click.transform.GetChild(0).GetComponent<Text>().text.Equals("���糪�� ���ٱ�"))
        {
            ThankTreeUI.SetActive(true);
            UIB.chat.ChatEnd();
        }
    }
    public void CheckQuest()
    {

        GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");


        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
        
    }
}
