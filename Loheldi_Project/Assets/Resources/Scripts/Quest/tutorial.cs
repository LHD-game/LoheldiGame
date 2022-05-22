using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        chat.Num = "0_12";
        chat.FileAdress = "Scripts/Quest/script_1";
        chat.NewChat();
    }

    public void Tutorial()
    {
        Debug.Log("Ʃ�丮�� ���P��22");

        Image tutoblack = chat.block.GetComponent<Image>();
        
        chat.color.a = 0.8f;
        chat.block.GetComponent<Image>().color = chat.color;
        if (!chat.tuto)
            chat.chatCanvus.SetActive(false);
        chat.j--;

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_6"))
        {
            //Debug.Log("Ʃ��i=" + chat.tutoi);
            //Debug.Log("��ư=" + chat.block.transform.GetChild(chat.tutoi));
            tutoblack.sprite = chat.cuttoonImageList[5];
            if (chat.tutoi < 4)
            {
                if(chat.tutoi ==0)
                    chat.Main_UI.SetActive(true);
                else if(chat.tutoi ==1)
                    chat.Main_UI.SetActive(false);
                chat.block.transform.GetChild(chat.tutoi).gameObject.SetActive(true);
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
        
        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_12"))
        {
            if (chat.tutoi < 7)
            {
                if (chat.tutoi == 0)
                    chat.tutoi += 4;
                Debug.Log("12���� Ʃ��i=" + chat.tutoi);
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