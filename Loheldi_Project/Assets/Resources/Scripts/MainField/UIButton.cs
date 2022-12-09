using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player�� �ٴڿ� �ִ��� Ȯ��
    public GameObject Player;             //Player����
    public GameObject Map;                //Map����                
    public GameObject GaguShop;           //������             
    public GameObject Market;           //����         
    public GameObject clothesShop;        
    public GameObject HairShop;       
    public Rigidbody Playerrb;            //Player�� Rigidbody����
    public GameObject JoyStick;
    public GameObject Main_UI;
    public GameObject FarmUI;
    public GameObject chatBlock;


    public Camera Camera1;
    public Camera Camera2;

    //public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundEffectManager;

    public static bool is_pop_garden = false;

    public int time=0;

    [SerializeField]
    private Animator PA;

    void Start()
    {
        map = false;
    }
    string NPCName=null;
    public Transform NPC;
    public void JumpButton()                //������ư
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        if (OnLand && Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            NPCName = Inter.NameNPC;
            Inter.NpcNameTF = false;
            if (chat.bicycleRide.Ride)
                chat.bicycleRide.RideOn();

            Vector3 targetPositionNPC;
            NPC = GameObject.Find(Inter.NameNPC).transform;
            targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine"))
            { stopCorou(); }
            else if (chat.DontDestroy.QuestIndex.Equals("8_1") && Inter.NameNPC.Equals("Mei"))
            { stopCorou(); }
            else if (chat.DontDestroy.QuestIndex.Equals("13_1") && Inter.NameNPC.Equals("Suho"))
            { stopCorou(); }
            else if (Inter.NameNPC.Equals("ThankApplesTree"))
            {
                TimeCheck();
            }
            else
            {
                StartCoroutine(NPCturn(NPC, targetPositionNPC));
                //NPC.transform.LookAt(targetPositionNPC);
            }
            
            chatBlock.SetActive(true);
            StartCoroutine(Playerturn(NPC));
            //Player.transform.LookAt(targetPositionPlayer);
            Invoke("ChatStart", 1f);
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

                GardenCategory.instance.PopGarden();
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
            if (OnLand && (SceneManager.GetActiveScene().name == "MainField"))                                         //Player�� �ٴڿ� �ִٸ�
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 15000);
            }
        }

        
    }
    void stopCorou()
    {
        if (Pstop)
            Pstop = false;
        if (Nstop)
            Nstop = false;
    }
    void ChatStart()
    {
        Main_UI.SetActive(false);
        Chat.NpcChoice(NPCName);
        chatBlock.SetActive(false);
    }
    public bool Pstop = true;
    public bool Nstop = true;
    public Transform Ptransform=null;
    public Transform Ntransform=null;
    public IEnumerator Playerturn(Transform NPC)
    {
        Invoke("stopCorou", 1f);
        PA.SetBool("ChatMove", true);
        yield return new WaitForEndOfFrame();
        Pstop = true;
        Vector3 targetPositionPlayer;
        targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.position.z);
        Ptransform = Player.transform;
        while (Pstop)
        {
            if (Player.transform.rotation == Quaternion.LookRotation(targetPositionPlayer - Ptransform.position))
            {
                Debug.Log("NPC��ž");
                Pstop = false;
            }
            Player.transform.rotation = Quaternion.Lerp(Ptransform.rotation, Quaternion.LookRotation(targetPositionPlayer - Player.transform.position), Time.deltaTime * 5f);
            yield return null;
        }
        PA.SetBool("ChatMove", false);
    }
    public IEnumerator NPCturn(Transform NPC, Vector3 targetPositionNPC)
    {
        if (NPC.gameObject.name.Equals("WallMirror"))
        { }
        else
        {
            Animator NA = NPC.GetChild(0).Find("Armature_").gameObject.GetComponent<Animator>();
            NA.SetBool("NpcMove", true);

            yield return new WaitForEndOfFrame();
            Nstop = true;
            //Vector3 targetPositionNPC;
            //targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            Ntransform = NPC.transform;
            while (Nstop)
            {
                if (NPC.rotation == Quaternion.LookRotation(targetPositionNPC - NPC.transform.position))
                {
                    Debug.Log("NPC��ž");
                    Nstop = false;
                }
                NPC.rotation = Quaternion.Lerp(NPC.rotation, Quaternion.LookRotation(targetPositionNPC - NPC.position), Time.deltaTime * 5f);
                yield return null;
            }
            NA.SetBool("NpcMove", false);
        }
    }

    public void TimeCheck()
    {
        var bro = Backend.GameData.GetMyData("USER_SUBQUEST", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            Param param = new Param();  // �� ��ü ����

            param.Add("LastThankTreeTime", time);    //��ü�� �� �߰�

            Backend.GameData.Insert("USER_SUBQUEST", param);   //��ü�� ������ ���ε�
        }
        else
        {
            var json = bro.GetReturnValuetoJSON();
            var json_data = json["rows"][0];
            ParsingJSON pj = new ParsingJSON();
            MySubQuest data = pj.ParseBackendData<MySubQuest>(json_data);
            time = data.LastThankTreeTime;
        }
    }
}
