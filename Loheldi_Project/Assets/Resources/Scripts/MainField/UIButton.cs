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
    public GameObject Main_UI;
    public GameObject GachaUI;

    //public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            ConditionWindow.SetActive(true); //����â ����
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>(); 
        }
            
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //���� �±� ����

        //ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/"); //�̹��� ���

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
        if (Inter.Gacha)
        {
            GachaUI.SetActive(true);
        }
        else if (Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
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
}
