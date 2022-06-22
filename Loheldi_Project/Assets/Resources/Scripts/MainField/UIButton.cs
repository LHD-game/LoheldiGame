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
    public GameObject Main_UI;

    //public GameObject ShopMok;             // 목공방
    bool map;                              //지도가 열려있는지 확인

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            ConditionWindow.SetActive(true); //상태창 열기
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>(); 
        }
            
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //뱃지 태그 저장

        //ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/"); //이미지 경로

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

            GameObject NPC;
            NPC = GameObject.Find(Inter.NameNPC);
            Vector3 targetPositionNPC;
            Vector3 targetPositionPlayer;
            targetPositionNPC = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            targetPositionPlayer = new Vector3(NPC.transform.position.x, transform.position.y, NPC.transform.position.z);
            Main_UI.SetActive(false);
            Chat.NpcChoice();
            NPC.transform.LookAt(targetPositionNPC);
            Player.transform.LookAt(targetPositionPlayer);
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
}
