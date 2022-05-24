using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NpcButtonClick : MonoBehaviour
{
    [SerializeField]
    Text SecondButtonTxt;
    //private Text TButtonTxt;
    private UIButton UIB;
    // Start is called before the first frame update

    public void SecondButtonClick()
    {
        UIB = GameObject.Find("EventSystem").GetComponent<UIButton>();
        Debug.Log(SecondButtonTxt.text);
        if (SecondButtonTxt.text.Equals("미니게임 하기"))
            SceneLoader.instance.GotoLobby();
        else if (SecondButtonTxt.text.Equals("미용실 이용하기"))
            SceneLoader.instance.GotoPlayerCustom();
        else if (SecondButtonTxt.text.Equals("가구점 이용하기"))
        {
            UIB.shop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (SecondButtonTxt.text.Equals("퀘스트 하미"))
        {
           // UIB.chat.
        }
    }
}
