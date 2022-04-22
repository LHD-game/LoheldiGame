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

        chat.FileAdress = "Scripts/Quest/Dialog";
        chat.NewChat();
    }
}
