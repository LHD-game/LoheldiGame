using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public static GameObject button;
    public LodingTxt chat;
    private FlieChoice Chat;

    private void Awake()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
    }

    public void forTutorial()
    {
        chat.Main_UI.SetActive(false);
        chat.Num = "0-5";
        chat.FileAdress = "Scripts/Quest/script_1";
        chat.move = true;
        chat.cuttoonFileAdress = "Sprites/Quest/cuttoon/tutorial";
        chat.NewChat();
    }

    public void Tutorial()
    {
        Debug.Log("Æ©Åä¸®¾ó ½ÇÁP¤·22");

        Image tutoblack = chat.block.GetComponent<Image>();
        
        chat.color.a = 1f;
        chat.block.GetComponent<Image>().color = chat.color;
        if (!chat.tuto)
            chat.ChatWin.SetActive(false);
        chat.j--;

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0-6")||(chat.tutoi<=4&&chat.tutoi>0))
        {
            Debug.Log("Æ©Åäi=" + chat.tutoi);
            Debug.Log("¹öÆ°=" + chat.block.transform.GetChild(chat.tutoi));
            if (chat.tutoi < 4)
            {
                tutoblack.sprite = chat.cuttoonImageList[4+chat.tutoi];
                if (chat.tutoi == 0)
                {
                    chat.Main_UI.SetActive(true);
                    chat.JumpButtons.JumpButtons.SetActive(true);
                }
                else if (chat.tutoi == 1)
                    chat.Main_UI.SetActive(false);
                chat.c += 1;
                //chat.Cuttoon();
                chat.block.transform.GetChild(chat.tutoi).gameObject.SetActive(true);
                if(chat.tutoi>0)
                    chat.block.transform.GetChild(chat.tutoi-1).gameObject.SetActive(false);
                chat.tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    chat.block.transform.GetChild(i).gameObject.SetActive(false);
                chat.tutoi = 6;
                chat.tutoFinish = true;
                chat.ChatEnd();
                forTutorial();
            }
        }

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0-12"))
        {
            if (chat.tutoi < 7)
            {
                if (chat.tutoi == 0)
                    chat.tutoi += 4;
                //Debug.Log("12½ÇÇà Æ©Åäi=" + chat.tutoi);
                tutoblack.sprite = chat.cuttoonImageList[5];
                chat.Main_UI.SetActive(true);
                chat.block.transform.GetChild(chat.tutoi).gameObject.SetActive(true);
                chat.tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                for (int i = 4; i < 7; i++)
                    chat.block.transform.GetChild(i).gameObject.SetActive(false);
                chat.tutoi = 12;
                chat.tutoFinish = true;
                chat.ChatEnd();
                forTutorial();
            }
        }
        

        /*if (chat.fade_in_out.Panel == null)
        {
            chat.Cuttoon();
            chat.fade_in_out.Panel = chat.cuttoon.GetComponent<Image>();
            InvokeRepeating("fade_in_out.Fade", 0.5f, 0.1F);
        }
        else
            CancelInvoke("fade_in_out.Fade");*/
    }

}
