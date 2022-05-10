using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
    public string NameNPC;
    public bool Door;

    private ChangeMode change;
    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = true;
            text.text = "��ȭ";
            NameNPC = other.gameObject.name.ToString();
        }
        if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change.changeCamera();
        }
    }
    private void Start()
    {
        change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
    }

    void OnTriggerStay(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (other.gameObject.name == "InDoor")          //�ݸ����� Tag�� NPC���
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
    }

    void OnTriggerExit(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" || other.gameObject.name == "ExitDoor")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = false;
            text.text = "����";
            NameNPC = " ";
        }
        if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change.changeCamera();
        }
    }
}