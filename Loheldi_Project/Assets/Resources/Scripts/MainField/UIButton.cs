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
    public GameObject GaguShop;           //������             
    public GameObject Market;           //����         
    public GameObject clothesShop;           //����
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

    public static bool is_pop_garden = false;

    [SerializeField]
    private GameObject c_seed;          //���� ī�װ�


    void Start()
    {
        map = false;
    }

    public void JumpButton()                //������ư
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        /*if (Inter.Gacha)  ��í �ٽ� ����
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMGacha");
            GachaUI.SetActive(true);
        }*/
        if (OnLand && Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            Inter.NpcNameTF = false;
            if (chat.bicycleRide.Ride)
                chat.bicycleRide.RideOn();
            
            GameObject NPC;
            NPC = GameObject.Find(Inter.NameNPC);
            Vector3 targetPositionNPC;
            Vector3 targetPositionPlayer;
            if (Inter.NameNPC.Equals("WallMirror"))
                Debug.Log("�ſ�");
            else if (chat.DontDestroy.QuestIndex.Equals("8_1"))
                Debug.Log("���� ����Ʈ");
            else if (chat.DontDestroy.QuestIndex.Equals("13_1"))
                Debug.Log("��ȣ ����Ʈ");
            else
            {
                targetPositionNPC = new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z);
                NPC.transform.LookAt(targetPositionNPC);
            }
            targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.transform.position.z);
            Main_UI.SetActive(false);
            Chat.NpcChoice();
            Player.transform.LookAt(targetPositionPlayer);
             
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
            GardenControl gcl = new GardenControl();
            if (Camera1.enabled == true)
            {
                Camera1.enabled = false;
                Camera2.enabled = true;
                JoyStick.SetActive(false);
                FarmUI.SetActive(true);

                GardenCategory gc = new GardenCategory();
                gc.PopGarden(c_seed);
                for (int i = 0; i < 4; i++)
                    if (gcl.garden_crops[i].GetComponent<CropsSell>())
                        gcl.garden_crops[i].GetComponent<CropsSell>().enabled = true;
            }
            else
            {
                Camera2.enabled = false;
                Camera1.enabled = true;
                JoyStick.SetActive(true);
                FarmUI.SetActive(false);
                for (int i = 0; i < 4; i++)
                    if (gcl.garden_crops[i].GetComponent<CropsSell>())
                        gcl.garden_crops[i].GetComponent<CropsSell>().enabled = false;
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
                //MainGameManager.NowExp = MainGameManager.NowExp + 100;
            }
        }
    }
}
