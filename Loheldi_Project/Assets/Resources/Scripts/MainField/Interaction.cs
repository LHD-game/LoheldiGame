using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Text text;                               //점프버튼에 글자를 선언
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC근처에 있는지 확인하는 함수 선언
    public string NameNPC;
    public bool Door;
    public bool Gacha;
    public bool Farm = false;
    public bool ThankTree = false;

    public bool NpcNameTF = false;
    public List<string> Npcs = new List<string>();
    public GameObject[] NpcNames;

    public GameObject FarmingMaster;

    public Camera MainCam;
    public Camera TCam;
    public VirtualJoystick VJS;
    public FurnitureChangeClick FCC;
    public HousingElevator HE;


    private ChangeMode change;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Housing")
        {
            MainCam.enabled = true;
            TCam.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)             //다른 콜리더와 부딛혔을때
    {
        if (other.gameObject.tag == "NPC")          //콜리더의 Tag가 NPC라면
        {
            Door = false;
            Farm = false;
            if (other.gameObject.name == "WallMirror")
            {
                text.text = "거울";
            }
            else if (other.gameObject.name == "GachaMachine")
            {
                Gacha = true;
                text.text = "뽑기";
            }
            else if (other.gameObject.name == "ThankApplesTree")
            {
                ThankTree = true;
                text.text = "감사나무";
            }
            else
            {
                ThankTree = false;
                text.text = "대화";
            }
            NearNPC = true;
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject);
            Debug.Log("NPC이름=" + NameNPC);
            
        }
        else if (other.gameObject.name == "change")          //콜리더의 name가 change라면 (하우징)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
        }
        else if (other.gameObject.name == "InDoor")          //콜리더의 Tag가 InDoor라면
        {
            ThankTree = false;
            Door = true;
            text.text = "들어가기";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(true);
            Door = true;
            text.text = "나가기";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "Field")
        {
            NearNPC = false;
            ThankTree = false;
            Farm = true;
            text.text = "농장";
        }
    }

    void OnTriggerExit(Collider other)              //다른 콜리더와 떨어졌을때
    {
        if (SceneManager.GetActiveScene().name == "Housing")
        {
            if (other.gameObject.name == "ExitDoor")
            {
                JumpButton.SetActive(false);
            }
            else if(other.gameObject.name == "Line")
            {
                if(this.transform.position.z<4.5f)
                {
                    MainCam.enabled = true;
                    TCam.enabled = false;
                    VJS.TempInt = 1;
                    FCC.getCamera = MainCam;
                }
                else
                {
                    MainCam.enabled = false;
                    TCam.enabled = true;
                    VJS.TempInt = 2;
                    FCC.getCamera = TCam;
                }
            }
        }
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" || other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //콜리더의 Tag가 NPC라면
        {
            if (other.gameObject.tag == "NPC")
            {
                NpcNameTF = false;
            }
            Door = false;
            Gacha = false;
            Farm = false;
            ThankTree = false;
            NearNPC = false;
            text.text = "점프";
            NameNPC = " ";
        }
        
    }
    public void NpcNameActive(GameObject other)
    {
        if (NameNPC == "ThankApplesTree") ;
        else
        {
            int NpcNum = Npcs.IndexOf(NameNPC);
            NpcNameTF = true;

            NpcNames[NpcNum].SetActive(true);
            StartCoroutine(NpcNameFollow(other, NpcNum));
        }
    }
    IEnumerator NpcNameFollow(GameObject Npc, int NpcNum)
    {
        GameObject NPCName_ = NpcNames[NpcNum];
        while (NpcNameTF)
        {
            NPCName_.transform.position = Camera.main.WorldToScreenPoint(Npc.transform.position + new Vector3(0, 7f, 0));
            yield return null;
        }
        NPCName_.SetActive(false);
        yield break;
    }
}