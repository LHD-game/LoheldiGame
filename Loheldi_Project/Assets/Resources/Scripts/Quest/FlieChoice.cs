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

    public void Tiger()
    {
        chat.Num = "1";
        chat.NPCButton = 2;
        chat.FileAdress = "Scripts/Quest/DialogNPC";
        chat.NewChat();
        chat.Buttons();
    }
}
