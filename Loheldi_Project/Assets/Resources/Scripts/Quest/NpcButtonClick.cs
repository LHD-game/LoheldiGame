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
        if (SecondButtonTxt.text.Equals("�̴ϰ��� �ϱ�"))
            SceneLoader.instance.GotoLobby();
        else if (SecondButtonTxt.text.Equals("�̿�� �̿��ϱ�"))
            SceneLoader.instance.GotoPlayerCustom();
        else if (SecondButtonTxt.text.Equals("������ �̿��ϱ�"))
        {
            UIB.shop.SetActive(true);
            UIB.chat.ChatEnd();
        }
        else if (SecondButtonTxt.text.Equals("����Ʈ �Ϲ�"))
        {
           // UIB.chat.
        }
    }
}
