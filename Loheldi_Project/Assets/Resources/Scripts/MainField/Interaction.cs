using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
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
    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            Door = false;
            Farm = false;
            if (other.gameObject.name == "WallMirror")
            {
                text.text = "�ſ�";
            }
            else if (other.gameObject.name == "GachaMachine")
            {
                Gacha = true;
                text.text = "�̱�";
            }
            else if (other.gameObject.name == "ThankApplesTree")
            {
                ThankTree = true;
                text.text = "���糪��";
            }
            else
            {
                ThankTree = false;
                text.text = "��ȭ";
            }
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
            ThankTree = false;
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
        else if (other.gameObject.name == "Field")
        {
            NearNPC = false;
            ThankTree = false;
            Farm = true;
            text.text = "����";
        }
    }

    void OnTriggerExit(Collider other)              //�ٸ� �ݸ����� ����������
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
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" || other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //�ݸ����� Tag�� NPC���
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
            text.text = "����";
            NameNPC = " ";
        }
        
    }
    public void NpcNameActive(GameObject other)
    {
        if (NameNPC == "ThankApplesTree" || NameNPC == "parents(Clone)") ;
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