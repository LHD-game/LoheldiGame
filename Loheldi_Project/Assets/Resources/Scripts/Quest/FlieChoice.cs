using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    // l=뜨는 이미지 번호(스크립트 대화)  n이랑l이랑 합칠 수 있나요(전 모르겠어요)
    //n=뜨는 이미지 번호(기본대화)
    //h=이미지 넣을 곳 번호

    private void Awake()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        if (SceneManager.GetActiveScene().name == "Quiz")
        {
            Quest(); 
        }
    }
    public void test()
    {
        chat.Num = "1_15";
        chat.FileAdress = "Scripts/Quest/test";
        chat.NewChat();
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest1");
    }

    public void test2()
    {
        chat.Num = "2";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }
    public void Tutorial()
    {
        chat.Main_UI.SetActive(false);
        chat.FileAdress = "Scripts/Quest/script";
        chat.move = true;
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
        if (chat.DontDestroy.tutorialLoading)
        {
            chat.DontDestroy.QuestIndex = "0_4";
        }
        else
            chat.Num = "0_1";
        
        chat.NewChat();
    }
    public void Quest()  //컷툰 등장하는 퀘스트일 때, 컷툰 이미지 불러오기
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            Debug.Log(chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
            chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest" + chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
        }
        chat.FileAdress = "Scripts/Quest/script";
        chat.Num = chat.DontDestroy.QuestIndex;
        chat.NewChat();
    }

    public void NpcChoice() //npc와 대화 선택하는 함수
    {
        chat.FileAdress = "Scripts/Quest/DialogNPC";

        switch (chat.Inter.NameNPC)
        {
            case "Himchan":
                chat.Num = "1";
                chat.NPCButton += 2;
                break;
            case "Markatman":
                chat.Num = "2";
                chat.NPCButton += 2;
                break;
            case "Hami":
                if (chat.DontDestroy.QuestIndex.Substring(chat.DontDestroy.QuestIndex.IndexOf("_")+1).Equals("2"))
                    chat.Num = "10";
                else
                    chat.Num = "3";
                chat.NPCButton += 1;
                break;
            case "Suho":
                chat.Num = "4";
                chat.NPCButton += 1;
                break;
            case "Nari":
                chat.Num = "5";
                chat.NPCButton += 1;
                break;
            case "Mei":
                chat.Num = "6";
                chat.NPCButton += 1;
                break;
            case "Yomi":
                chat.Num = "7";
                chat.NPCButton += 2;
                break;
            case "Yeomi":
                chat.Num = "8";
                chat.NPCButton += 2;
                break;
            case "Mu":
                chat.Num = "9";
                chat.NPCButton += 2;
                break;
        }
        
        chat.NewChat();
        chat.Buttons();

    }
    
}
