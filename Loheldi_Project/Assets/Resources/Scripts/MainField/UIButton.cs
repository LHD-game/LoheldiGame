using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player가 바닥에 있는지 확인
    public GameObject Player;             //Player선언
    public GameObject Map;                //Map선언
    public GameObject Inv;
    public GameObject ConditionWindow;
    public Rigidbody Playerrb;            //Player의 Rigidbody선언
    public Text conditionLevelText;            //상태창 레벨
    public GameObject JumpButtons;

    //public GameObject ShopMok;             // 목공방
    bool map;                              //지도가 열려있는지 확인
    bool inv;

    private FlieChoice Chat;
    public Interaction Inter;

    public GameObject SoundManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            ConditionWindow.SetActive(true); //상태창 열기
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //뱃지 태그 저장

        ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/"); //이미지 경로

        if (SceneManager.GetActiveScene().name == "MainField")
            ConditionWindow.SetActive(false);//상태창 닫기
    }

    void Start()
    {
        map = false;
        Inter = GameObject.Find("Player").GetComponent<Interaction>();
        if (SceneManager.GetActiveScene().name == "MainField")
            Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
    }

    public void JumpButton()                //점프버튼
    {
        if (Inter.NearNPC)     //NPC주변에 있다면
        {
            JumpButtons.SetActive(false);
            if (Inter.NameNPC.Equals("Himchan"))  //NPC이름이 이거면
            {
                Chat.tiger();
            }
            else if (Inter.NameNPC.Equals("Markatman"))  //NPC이름이 이거면
            {
                Chat.cat();
            }
            else if (Inter.NameNPC.Equals("Hami"))  //NPC이름이 이거면
            {
                Chat.chick();
            }
            else if (Inter.NameNPC.Equals("Suho"))  //NPC이름이 이거면
            {
                Chat.rabbit();
            }
            else if (Inter.NameNPC.Equals("Nari"))  //NPC이름이 이거면
            {
                Chat.squirrel();
            }
            else if (Inter.NameNPC.Equals("Mei"))  //NPC이름이 이거면
            {
                Chat.goat();
            }
            else if (Inter.NameNPC.Equals("Yomi"))  //NPC이름이 이거면
            {
                Chat.fox2();
            }
            else if (Inter.NameNPC.Equals("Yeomi"))  //NPC이름이 이거면
            {
                Chat.fox1();
            }
            else if (Inter.NameNPC.Equals("Mu"))  //NPC이름이 이거면
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
        else                                                //NPC주변에 있지 않다면
        {
            if (OnLand&&(SceneManager.GetActiveScene().name == "MainField"))                                         //Player가 바닥에 있다면
            {
                SoundManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
                MainGameManager.exp = MainGameManager.exp + 100;
            }
        }
    }


    public void NPCButtonUI()
    {
        if (Inter.NameNPC.Equals("Himchan"))  //NPC이름이 이거면
        {
            SceneLoader.instance.GotoLobby();
        }
        else if (Inter.NameNPC.Equals("Yomi"))  //NPC이름이 이거면
        {
            SceneLoader.instance.GotoPlayerCustom();
        }
        /*else if (Inter.NameNPC.Equals("Hami"))  //NPC이름이 이거면
        {
            Chat.chick();
        }
        else if (Inter.NameNPC.Equals("Suho"))  //NPC이름이 이거면
        {
            Chat.rabbit();
        }
        else if (Inter.NameNPC.Equals("Nari"))  //NPC이름이 이거면
        {
            Chat.squirrel();
        }
        else if (Inter.NameNPC.Equals("Mei"))  //NPC이름이 이거면
        {
            Chat.goat();
        }
        else if (Inter.NameNPC.Equals("Markatman"))  //NPC이름이 이거면
        {
            Chat.fox2();
        }
        else if (Inter.NameNPC.Equals("Yeomi"))  //NPC이름이 이거면
        {
            Chat.fox1();
        }
        else if (Inter.NameNPC.Equals("Mu"))  //NPC이름이 이거면
        {
            Chat.dog();
        }*/
    }

    /*public void MapButton()                 //지도버튼
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

    public  void ConditionButton()                 //상태창버튼
    {
        if (conditionWindow)                                            //상태창이 열려있다면
        {
            ConditionWindow.SetActive(false);
            conditionWindow = false;
        }
        else                                                //상태창이 닫혀있다면
        {
            ConditionWindow.SetActive(true);
            conditionWindow = true;
            conditionLevelText.text = MainGameManager.level.ToString();
        }
    }*/
}
