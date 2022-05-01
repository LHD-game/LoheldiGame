using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;

    public void test()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();

        chat.Num = "1";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }

    public void test2()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();

        chat.Num = "2";
        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }
}
