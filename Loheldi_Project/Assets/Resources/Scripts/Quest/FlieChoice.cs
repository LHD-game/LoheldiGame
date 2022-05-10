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
        chat.Num = "1";
        chat.l = 1;
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }

    public void test2()
    {
        chat.l = 1;
        chat.Num = "2";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }

    public void tiger()
    {
        chat.l = 1;
        chat.Num = "1";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void cat()
    {
        chat.l = 2;
        chat.Num = "2";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void chick()
    {
        chat.l = 3;
        chat.Num = "3";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void rabbit()
    {
        chat.l = 4;
        chat.Num = "4";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void squirrel()
    {
        chat.l = 5;
        chat.Num = "5";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void goat()
    {
        chat.l = 6;
        chat.Num = "6";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void fox2()
    {
        chat.l = 7;
        chat.Num = "7";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void fox1()
    {
        chat.l = 8;
        chat.Num = "8";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void dog()
    {
        chat.l = 0;
        chat.Num = "9";
        chat.NPCButton = 1;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }
}
