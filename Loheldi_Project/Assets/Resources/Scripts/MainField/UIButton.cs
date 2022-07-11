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
    public GameObject shop;
    public GameObject ConditionWindow;
    public Rigidbody Playerrb;            //Player의 Rigidbody선언
    public GameObject JoyStick;
    public GameObject Main_UI;
    public GameObject GachaUI;
    public GameObject FarmUI;

    public Camera Camera1;
    public Camera Camera2;

    //public GameObject ShopMok;             // 목공방
    bool map;                              //지도가 열려있는지 확인

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundEffectManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            ConditionWindow.SetActive(true); //상태창 열기
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>(); 
        }
            
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //뱃지 태그 저장

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
        GameObject SoundManager = GameObject.Find("SoundManager");
        if (Inter.Gacha)
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMGacha");
            GachaUI.SetActive(true);
        }
        else if (OnLand && Inter.NearNPC)     //NPC주변에 있다면
        {

            GameObject NPC;
            NPC = GameObject.Find(Inter.NameNPC);
            Vector3 targetPositionNPC;
            Vector3 targetPositionPlayer;
            targetPositionNPC = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            targetPositionPlayer = new Vector3(NPC.transform.position.x, NPC.transform.position.y, NPC.transform.position.z);
            Main_UI.SetActive(false);
            Chat.NpcChoice();
            NPC.transform.LookAt(targetPositionNPC);
            Player.transform.LookAt(targetPositionPlayer);

            if (chat.bicycleRide.Ride)
                chat.bicycleRide.RideOn();
        }
        else if (Inter.Door)
        {
            if (Inter.NameNPC.Equals("InDoor"))
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("OpenDoor");
                SceneLoader.instance.GotoHouse();
            }

            else if (Inter.NameNPC.Equals("ExitDoor"))
                SceneLoader.instance.GotoMainField();
            Inter.Door = false;
        }
        else if (Inter.Farm)
        {
            if (Camera1.enabled == true)
            {
                Camera1.enabled = false;
                Camera2.enabled = true;
                JoyStick.SetActive(false);
                FarmUI.SetActive(true);
            }
            else
            {
                Camera2.enabled = false;
                Camera1.enabled = true;
                JoyStick.SetActive(true);
                FarmUI.SetActive(false);
            }
        }
        else                                                //NPC주변에 있지 않다면
        {
            Camera2.enabled = false;
            Camera1.enabled = true;
            if (OnLand && (SceneManager.GetActiveScene().name == "MainField"))                                         //Player가 바닥에 있다면
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
                MainGameManager.NowExp = MainGameManager.NowExp + 100;
            }
        }
    }
}
