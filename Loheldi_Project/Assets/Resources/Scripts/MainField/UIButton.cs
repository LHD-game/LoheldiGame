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
    public GameObject GaguShop;           //가구점             
    public GameObject Market;           //슈퍼         
    public GameObject clothesShop;           //슈퍼
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

    public static bool is_pop_garden = false;

    [SerializeField]
    private GameObject c_seed;          //씨앗 카테고리


    void Start()
    {
        map = false;
    }

    public void JumpButton()                //점프버튼
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        /*if (Inter.Gacha)  가챠 다시 적용
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMGacha");
            GachaUI.SetActive(true);
        }*/
        if (OnLand && Inter.NearNPC)     //NPC주변에 있다면
        {
            Inter.NpcNameTF = false;
            if (chat.bicycleRide.Ride)
                chat.bicycleRide.RideOn();
            
            GameObject NPC;
            NPC = GameObject.Find(Inter.NameNPC);
            Vector3 targetPositionNPC;
            Vector3 targetPositionPlayer;
            if (Inter.NameNPC.Equals("WallMirror"))
                Debug.Log("거울");
            else if (chat.DontDestroy.QuestIndex.Equals("8_1"))
                Debug.Log("메이 퀘스트");
            else if (chat.DontDestroy.QuestIndex.Equals("13_1"))
                Debug.Log("수호 퀘스트");
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
        else                                                //NPC주변에 있지 않다면
        {
            Camera2.enabled = false;
            Camera1.enabled = true;
            if (OnLand && (SceneManager.GetActiveScene().name == "MainField"))                                         //Player가 바닥에 있다면
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
                //MainGameManager.NowExp = MainGameManager.NowExp + 100;
            }
        }
    }
}
