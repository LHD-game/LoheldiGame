using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
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
    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            if (other.gameObject.name == "WallMirror")
            {
                text.text = "�ſ�";
            }
            else if (other.gameObject.name == "GachaMachine")
            {
                Gacha = true;
                text.text = "�̱�";
            }
            else
                text.text = "��ȭ";
            NearNPC = true;
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject);
            Debug.Log("NPC�̸�=" + NameNPC);
            
        }
        else if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
        }
        else if (other.gameObject.name == "InDoor")          //�ݸ����� Tag�� InDoor���
        {
            Door = true;
            text.text = "����";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(true);
            Door = true;
            text.text = "������";
            NameNPC = other.gameObject.name.ToString();
        }
        /*else if (other.gameObject.name == "GachaMachine")
        {
            Gacha = true;
            text.text = "�̱�";
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject);
        }*/
        else if (other.gameObject.name == "Field")
        {
            Farm = true;
            text.text = "����";
        }
        
    }
    /*private void Start()
    {
        change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
    }*/

    /*void OnTriggerStay(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (other.gameObject.name == "InDoor")          //�ݸ����� Tag�� InDoor���
        {
            Door = true;
            text.text = "����";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            Door = true;
            text.text = "������";
            NameNPC = other.gameObject.name.ToString();
        }
    }*/

    void OnTriggerExit(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" || other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //�ݸ����� Tag�� NPC���
        {
            if (other.gameObject.tag == "NPC")
            {
                NpcNameTF = false;
            }
            Door = false;
            Gacha = false;
            Farm = false;
            NearNPC = false;
            text.text = "����";
            NameNPC = " ";
        }
        if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(false);
        }
    }
    public void NpcNameActive(GameObject other)
    {
        int NpcNum = Npcs.IndexOf(NameNPC);
            NpcNameTF = true;
            NpcNames[NpcNum].SetActive(true);
            StartCoroutine(NpcNameFollow(other, NpcNum));
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