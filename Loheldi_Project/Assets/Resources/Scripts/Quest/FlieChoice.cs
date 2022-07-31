using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    public GameObject EPin;

    private void Awake()
    {
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
        Save_Log.instance.SaveQStartLog();  //퀘스트 시작 시간 로그
        chat.Main_UI.SetActive(false);
        chat.FileAdress = "Scripts/Quest/script";
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
        chat.Num = "0_1";
        
        chat.NewChat();
    }
    public void Quest() 
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            //Debug.Log(chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
            chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest" + chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
        }
        chat.FileAdress = "Scripts/Quest/script";
        chat.Num = chat.DontDestroy.QuestIndex;
        chat.NewChat();
    }

    public void NpcChoice() //npc와 대화 선택하는 함수
    {
        chat.FileAdress = "Scripts/Quest/DialogNPC";

        string[] QuestF = chat.DontDestroy.QuestIndex.Split('_');
            switch (chat.Inter.NameNPC)
        {
            case "Himchan":
                if (chat.DontDestroy.From.Equals("실천해보기")&& chat.DontDestroy.ButtonPlusNpc.Equals("Himchan"))
                    chat.Num = "14";
                else
                    chat.Num = "1";
                chat.NPCButton += 2;
                break;
            case "Markatman":
                chat.Num = "2";
                chat.NPCButton += 2;
                break;
            case "Hami":
                if (QuestF[1].Equals("2"))
                    chat.Num = "11";
                else if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Hami"))
                    chat.Num = "15";
                else
                    chat.Num = "3";
                chat.NPCButton += 1;
                break;
            case "Suho":
                if (QuestF[1].Equals("2"))
                    chat.Num = "12";
                else if (chat.DontDestroy.QuestIndex.Equals("13_1"))
                    chat.Num = "18";
                else if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Suho"))
                    chat.Num = "16";
                else
                    chat.Num = "4";
                chat.NPCButton += 1;
                break;
            case "Nari":
                chat.Num = "5";
                chat.NPCButton += 1;
                break;
            case "Mei":
                if (chat.DontDestroy.From.Equals("실천해보기") && chat.DontDestroy.ButtonPlusNpc.Equals("Mei"))
                    chat.Num = "17";
                else if (chat.DontDestroy.QuestIndex.Equals("8_1"))
                    chat.Num = "19";
                else
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
            case "WallMirror":
                chat.Num = "10";
                chat.NPCButton += 2;
                break;
            case "parents(Clone)":
                chat.Num = "13";
                chat.NPCButton += 2;
                break;
        }
        // CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"+chat.Inter.NameNPC); //이미지 경로
        chat.NewChat();
        chat.Buttons();

    }
    
}
