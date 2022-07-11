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
    public GameObject shop;
    public GameObject ConditionWindow;
    public Rigidbody Playerrb;            //Player�� Rigidbody����
    public GameObject JoyStick;
    public GameObject Main_UI;
    public GameObject GachaUI;
    public GameObject FarmUI;

    public Camera Camera1;
    public Camera Camera2;

    //public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundEffectManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            ConditionWindow.SetActive(true); //����â ����
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>(); 
        }
            
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //���� �±� ����

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
        GameObject SoundManager = GameObject.Find("SoundManager");
        if (Inter.Gacha)
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMGacha");
            GachaUI.SetActive(true);
        }
        else if (OnLand && Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
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
        else                                                //NPC�ֺ��� ���� �ʴٸ�
        {
            Camera2.enabled = false;
            Camera1.enabled = true;
            if (OnLand && (SceneManager.GetActiveScene().name == "MainField"))                                         //Player�� �ٴڿ� �ִٸ�
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
                MainGameManager.NowExp = MainGameManager.NowExp + 100;
            }
        }
    }
}
