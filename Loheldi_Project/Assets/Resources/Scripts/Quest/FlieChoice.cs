using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    // l=�ߴ� �̹��� ��ȣ(��ũ��Ʈ ��ȭ)  n�̶�l�̶� ��ĥ �� �ֳ���(�� �𸣰ھ��)
    //n=�ߴ� �̹��� ��ȣ(�⺻��ȭ)
    //h=�̹��� ���� �� ��ȣ

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
    public void QuestChoice()
    {
        //chat.Num = "0_1";
        chat.FileAdress = "Scripts/Quest/script_1";
        switch (chat.Num) 
        {
            case "1":
                break;
        }
        
        
        chat.NewChat();
    }

    public void Quest1()
    {
        chat.Num = "0_1";
        chat.FileAdress = "Scripts/Quest/script_1";
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
