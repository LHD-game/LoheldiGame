using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //점프버튼에 글자를 선언
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC근처에 있는지 확인하는 함수 선언
    public string NameNPC;
    public bool Door;
    public bool Gacha;
    public bool Farm = false;

    public bool NpcNameTF = false;
    public List<string> Npcs = new List<string>();
    //public GameObject[] Npcs;
    public GameObject[] NpcNames;

    public GameObject FarmingMaster;

    private ChangeMode change;
    void OnTriggerEnter(Collider other)             //다른 콜리더와 부딛혔을때
    {
        if (other.gameObject.tag == "NPC")          //콜리더의 Tag가 NPC라면
        {
            if (other.gameObject.name == "WallMirror")
            {
                text.text = "거울";
            }
            else
                text.text = "대화";
            NearNPC = true;
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject, "in");
            Debug.Log("NPC이름=" + NameNPC);
            
        }
        else if (other.gameObject.name == "change")          //콜리더의 name가 change라면 (하우징)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
            change.changeCamera();
        }
        else if (other.gameObject.name == "InDoor")          //콜리더의 Tag가 InDoor라면
        {
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
        else if (other.gameObject.name == "GachaMachine")
        {
            Gacha = true;
            text.text = "뽑기";
        }
        else if (other.gameObject.name == "Field")
        {
            Farm = true;
            text.text = "농장";
        }
        
    }
    /*private void Start()
    {
        change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
    }*/

    /*void OnTriggerStay(Collider other)              //다른 콜리더와 겹쳐있을때
    {
        if (other.gameObject.name == "InDoor")          //콜리더의 Tag가 InDoor라면
        {
            Door = true;
            text.text = "들어가기";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            Door = true;
            text.text = "나가기";
            NameNPC = other.gameObject.name.ToString();
        }
    }*/

    void OnTriggerExit(Collider other)              //다른 콜리더와 떨어졌을때
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" || other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //콜리더의 Tag가 NPC라면
        {
            if (other.gameObject.tag == "NPC")
            {
                NpcNameTF = false;
            }
            Door = false;
            Gacha = false;
            Farm = false;
            NearNPC = false;
            text.text = "점프";
            NameNPC = " ";
        }
        if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(false);
        }
        if (other.gameObject.name == "change")          //콜리더의 name가 change라면 (하우징)
        {
            change.changeCamera();
        }
    }
    public void NpcNameActive(GameObject other, string status)
    {
        int NpcNum = Npcs.IndexOf(NameNPC);
        if (status.Equals("in"))
        {
            NpcNameTF = true;
            NpcNames[NpcNum].SetActive(true);
            StartCoroutine(NpcNameFollow(other, NpcNum));
        }
    }
    IEnumerator NpcNameFollow(GameObject Npc, int NpcNum)
    {
        while (NpcNameTF)
        {
            NpcNames[NpcNum].transform.position = Camera.main.WorldToScreenPoint(Npc.transform.position + new Vector3(0, 7f, 0));
            yield return null;
        }
        NpcNames[NpcNum].SetActive(false);
        yield break;
    }
}