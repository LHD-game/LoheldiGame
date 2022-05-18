using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player�� �ٴڿ� �ִ��� Ȯ��
    public GameObject Player;             //Player����
    public GameObject Map;                //Map����
    public GameObject Inv;
    public GameObject ConditionWindow;
    public Rigidbody Playerrb;            //Player�� Rigidbody����
    public Text conditionLevelText;            //����â ����
    public GameObject JumpButtons;

    //public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��
    bool inv;

    private FlieChoice Chat;
    public Interaction Inter;

    public GameObject SoundManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            ConditionWindow.SetActive(true); //����â ����
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //���� �±� ����

        ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/"); //�̹��� ���

        if (SceneManager.GetActiveScene().name == "MainField")
            ConditionWindow.SetActive(false);//����â �ݱ�
    }

    void Start()
    {
        map = false;
        Inter = GameObject.Find("Player").GetComponent<Interaction>();
        if (SceneManager.GetActiveScene().name == "MainField")
            Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
    }

    public void JumpButton()                //������ư
    {
        if (Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            JumpButtons.SetActive(false);
            if (Inter.NameNPC.Equals("Himchan"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.tiger();
            }
            else if (Inter.NameNPC.Equals("Markatman"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.cat();
            }
            else if (Inter.NameNPC.Equals("Hami"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.chick();
            }
            else if (Inter.NameNPC.Equals("Suho"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.rabbit();
            }
            else if (Inter.NameNPC.Equals("Nari"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.squirrel();
            }
            else if (Inter.NameNPC.Equals("Mei"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.goat();
            }
            else if (Inter.NameNPC.Equals("Yomi"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.fox2();
            }
            else if (Inter.NameNPC.Equals("Yeomi"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.fox1();
            }
            else if (Inter.NameNPC.Equals("Mu"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.dog();
            }
            //ShopMok.SetActive(true);
        }
        else if (Inter.Door)
        {
            if (Inter.NameNPC.Equals("InDoor"))
            {
                SoundManager.GetComponent<SoundEffect>().Sound("OpenDoor");
                SceneLoader.instance.GotoHouse();
            }
                
            else if (Inter.NameNPC.Equals("ExitDoor"))
                SceneLoader.instance.GotoMainField();
            Inter.Door = false;
        }
        else                                                //NPC�ֺ��� ���� �ʴٸ�
        {
            if (OnLand&&(SceneManager.GetActiveScene().name == "MainField"))                                         //Player�� �ٴڿ� �ִٸ�
            {
                SoundManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
                MainGameManager.exp = MainGameManager.exp + 100;
            }
        }
    }


    public void NPCButtonUI()
    {
        if (Inter.NameNPC.Equals("Himchan"))  //NPC�̸��� �̰Ÿ�
        {
            SceneLoader.instance.GotoLobby();
        }
        else if (Inter.NameNPC.Equals("Yomi"))  //NPC�̸��� �̰Ÿ�
        {
            SceneLoader.instance.GotoPlayerCustom();
        }
        /*else if (Inter.NameNPC.Equals("Hami"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.chick();
        }
        else if (Inter.NameNPC.Equals("Suho"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.rabbit();
        }
        else if (Inter.NameNPC.Equals("Nari"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.squirrel();
        }
        else if (Inter.NameNPC.Equals("Mei"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.goat();
        }
        else if (Inter.NameNPC.Equals("Markatman"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.fox2();
        }
        else if (Inter.NameNPC.Equals("Yeomi"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.fox1();
        }
        else if (Inter.NameNPC.Equals("Mu"))  //NPC�̸��� �̰Ÿ�
        {
            Chat.dog();
        }*/
    }

    /*public void MapButton()                 //������ư
    {
        Map.SetActive(true);
    }

    public void InvButton()
    {
        if (inv)
        {
            Inv.SetActive(false);
            map = false;
        }
        else
        {
            Inv.SetActive(true);
            map = true;
        }
    }

    public  void ConditionButton()                 //����â��ư
    {
        if (conditionWindow)                                            //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(false);
            conditionWindow = false;
        }
        else                                                //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(true);
            conditionWindow = true;
            conditionLevelText.text = MainGameManager.level.ToString();
        }
    }*/
}
