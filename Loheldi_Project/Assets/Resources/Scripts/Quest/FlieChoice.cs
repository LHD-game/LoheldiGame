using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    // l=뜨는 이미지 번호(스크립트 대화)  n이랑l이랑 합칠 수 있나요(전 모르겠어요)
    //n=뜨는 이미지 번호(기본대화)
    //h=이미지 넣을 곳 번호

    private void Awake()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
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
        if(chat.DontDestroy.tutorialLoading )
            chat.Num = "0_4";
        else
            chat.Num = "0_1";
        chat.Main_UI.SetActive(false);
        chat.FileAdress = "Scripts/Quest/script";
        chat.move = true;
        chat.cuttoonFileAdress = "Sprites/Quest/cuttoon/tutorial";
        chat.NewChat();
    }
    public void Quest()  //컷툰 등장하는 퀘스트일 때, 컷툰 이미지 불러오기
    {
        Debug.Log("subNum" + chat.DontDestroy.QuestSubNum);
        chat.FileAdress = "Scripts/Quest/script";
        switch (chat.DontDestroy.QuestIndex)
        { 
            case 1:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest1");
                chat.Num = "1_1";
                break;
            /*case 2:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest2");
                chat.Num = "2_1";
                break;*/
            case 3:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest3");
                chat.Num = "3_1";
                break;
            case 4:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest4");
                chat.Num = "4-1";
                break;
            case 5:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest5");
                chat.Num = "5-1";
                break;
            case 6:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest6");
                chat.Num = "6-1";
                break;
            case 7:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest7");
                chat.Num = "7-1";
                break;
            case 8:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest8");
                chat.Num = "8-1";
                break;
            case 9:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest9");
                chat.Num = "9-1";
                break;
            case 10:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest10");
                chat.Num = "10-1";
                break;
        }
    

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
                chat.NPCButton += 1;
                break;
            case "Hami":
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
                chat.NPCButton += 1;
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
