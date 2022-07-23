using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public static GameObject button;
    public LodingTxt chat;
    private FlieChoice Chat;
    public int tutoi;                            //튜토리얼 하이라이트 이미지용

    public void Tutorial()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();

        if (!chat.tuto)
            chat.Chat.SetActive(false);
        if(chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_4")&& !chat.DontDestroy.tutorialLoading)
        {
            chat.Main_UI.SetActive(true);
            chat.block.transform.Find("Button8").gameObject.SetActive(true);
        }

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_6")||(tutoi<=4&&tutoi>0))
        {
            if (tutoi < 4)
            {
                if (tutoi == 0)
                {
                    chat.Main_UI.SetActive(true);
                }
                else if (tutoi == 1)
                    chat.Main_UI.SetActive(false);
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
                chat.tutoFinish = true;
                chat.Chat.SetActive(true);
                chat.Line();
            }
        }

        if (chat.data_Dialog[chat.j]["scriptNumber"].ToString().Equals("0_12"))
        {
            if (tutoi < 7)
            {
                if (tutoi == 4)
                    chat.Main_UI.SetActive(true);
                else
                    chat.Main_UI.SetActive(false);
                chat.block.transform.GetChild(tutoi).gameObject.SetActive(true);
                if (tutoi > 4)
                    chat.block.transform.GetChild(tutoi - 1).gameObject.SetActive(false);
                tutoi++;
                if (!chat.tuto)
                    chat.tuto = true;
            }
            else
            {
                tutoi++;
                chat.block.transform.GetChild(6).gameObject.SetActive(false);
                chat.tutoFinish = true;
                chat.Chat.SetActive(true);
                chat.Line();
                chat.move = false;
                GameObject SoundManager = GameObject.Find("SoundManager");
                SoundManager.GetComponent<SoundManager>().Sound("BGMField");
                chat.DontDestroy.tutorialLoading = false;
            }
        }
    }

}
