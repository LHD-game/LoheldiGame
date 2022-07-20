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

    public GameObject FarmingMaster;

    private ChangeMode change;
    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = true;
            text.text = "��ȭ";
            NameNPC = other.gameObject.name.ToString();
            Debug.Log("NPC�̸�="+NameNPC);
        }
        if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
            change.changeCamera();
        }
        if (other.gameObject.name == "InDoor")          //�ݸ����� Tag�� InDoor���
        {
            Door = true;
            text.text = "����";
            NameNPC = other.gameObject.name.ToString();
        }
        if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(true);
            Door = true;
            text.text = "������";
            NameNPC = other.gameObject.name.ToString();
        }
        if (other.gameObject.name == "GachaMachine")
        {
            Gacha = true;
            text.text = "�̱�";
        }
        if (other.gameObject.name == "Field")
        {
            FarmingMaster.GetComponent<FarmingMaster>().SeedList();
            Farm = true;
            text.text = "����";
        }
        if (other.gameObject.name == "WallMirror")
        {
            Farm = true;
            text.text = "�ſ�";
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
        if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change.changeCamera();
        }
    }
}