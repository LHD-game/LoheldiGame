using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;

    private void Awake()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
    }
    public void test()
    {
        chat.Num = "1";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }

    public void test2()
    {
        chat.Num = "2";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }

    public void tiger()
    {
        chat.Num = "1";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void cat()
    {
        chat.Num = "2";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void chick()
    {
        chat.Num = "3";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void rabbit()
    {
        chat.Num = "4";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void squirrel()
    {
        chat.Num = "5";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void goat()
    {
        chat.Num = "6";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void fox2()
    {
        chat.Num = "7";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void fox1()
    {
        chat.Num = "8";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }

    public void dog()
    {
        chat.Num = "9";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }
}
