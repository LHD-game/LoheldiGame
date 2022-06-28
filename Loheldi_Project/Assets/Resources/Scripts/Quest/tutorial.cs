using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public static GameObject button;
    public LodingTxt chat;
    private FlieChoice Chat;
    public int tutoi;                            //∆©≈‰∏ÆæÛ «œ¿Ã∂Û¿Ã∆Æ ¿ÃπÃ¡ˆøÎ

    private void Awake()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
    }

    public void Tutorial()
    {
        Debug.Log("∆©≈‰∏ÆæÛ Ω«¡P§∑22");

        Image tutoblack = chat.block.GetComponent<Image>();
        if (tutoi < 7)
            tutoblack.sprite = chat.cuttoonImageList[4 + tutoi];
        chat.color.a = 1f;
        chat.block.GetComponent<Image>().color = chat.color;
        if (!chat.tuto)
            chat.ChatWin.SetActive(false);
        //chat.j--;
        Debug.Log("∆©≈‰i=" + tutoi);
        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_6")||(tutoi<=4&&tutoi>0))
        {
            //Debug.Log("∆©≈‰i=" + tutoi);
            //Debug.Log("πˆ∆∞=" + chat.block.transform.GetChild(tutoi));
            if (tutoi < 4)
            {
                Debug.Log("6Ω««‡ ∆©≈‰i=" + tutoi);
                //tutoblack.sprite = chat.cuttoonImageList[4+tutoi];
                if (tutoi == 0)
                {
                    chat.Main_UI.SetActive(true);
                    //chat.JumpButtons.Main_UI.SetActive(true);
                }
                else if (tutoi == 1)
                    chat.Main_UI.SetActive(false);
                //chat.c += 1;
                //chat.Cuttoon();
                chat.block.transform.GetChild(tutoi).gameObject.SetActive(true);
                if(tutoi>0)
                    chat.block.transform.GetChild(tutoi-1).gameObject.SetActive(false);
                tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                chat.block.transform.GetChild(3).gameObject.SetActive(false);
                //chat.tutoi = 6;
                chat.tutoFinish = true;
                //chat.ChatEnd();
                chat.Line();
                //forTutorial();
            }
        }

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_12"))
        {
            if (tutoi == 0)
                tutoi += 4;
            if (tutoi < 7)
            {
                //Debug.Log("12Ω««‡ ∆©≈‰i=" + (tutoi));
                //tutoblack.sprite = chat.cuttoonImageList[4+tutoi];
                chat.Main_UI.SetActive(true);
                //Debug.Log(chat.block.transform.GetChild(tutoi).gameObject.name);
                chat.block.transform.GetChild(tutoi).gameObject.SetActive(true);
                if (tutoi > 4)
                    chat.block.transform.GetChild(tutoi - 1).gameObject.SetActive(false);
                tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                chat.block.transform.GetChild(6).gameObject.SetActive(false);
                //chat.tutoi = 12;
                chat.tutoFinish = true;
                //chat.ChatEnd();
                chat.Line();
                chat.move = false;
                //forTutorial();
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
