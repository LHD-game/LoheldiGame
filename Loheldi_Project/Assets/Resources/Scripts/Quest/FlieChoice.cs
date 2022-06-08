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
        chat.Num = "0-1";
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
        chat.Num = "0-1";
        chat.FileAdress = "Scripts/Quest/script_1";
        chat.NewChat();
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
    }
    public void Quest1()
    {
        chat.FileAdress = "Scripts/Quest/script_1";
        switch (chat.QuestSubNum)
        { 
            case 1:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest1");
                break;
            case 2:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest2");
                break;
            case 3:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest3");
                break;
            case 4:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest4");
                break;
            case 5:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest5");
                break;
            case 6:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest6");
                break;
            case 7:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest7");
                break;
            case 8:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest8");
                break;
            case 9:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest9");
                break;
            case 10:
                chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest10");
                break;
        }
    

        chat.NewChat();
    }

    public void NpcChoice()
    {
        chat.FileAdress = "Scripts/Quest/DialogNPC";

        switch (chat.Inter.NameNPC)
        {
            case "Himchan":
                chat.Num = "1";
                chat.NPCButton = 2;
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
