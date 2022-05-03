using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
    public string NameNPC;
    

    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = true;
            text.text = "��ȭ";

            if (other.gameObject.name == "tiger")
            {
                NameNPC = "tiger";
            }
            else if (other.gameObject.name == "cat")
            {
                NameNPC = "cat";
            }
            else if (other.gameObject.name == "chick")
            {
                NameNPC = "chick";
            }
            else if (other.gameObject.name == "rabbit")
            {
                NameNPC = "rabbit";
            }
            else if (other.gameObject.name == "squirrel")
            {
                NameNPC = "squirrel";
            }
            else if (other.gameObject.name == "goat")
            {
                NameNPC = "goat";
            }
            else if (other.gameObject.name == "fox2")
            {
                NameNPC = "fox2";
            }
            else if (other.gameObject.name == "fox1")
            {
                NameNPC = "fox1";
            }
            else if (other.gameObject.name == "dog")
            {
                NameNPC = "dog";
            }
        }
    }

    void OnTriggerStay(Collider other)              //�ٸ� �ݸ����� ����������
    {

    }

    void OnTriggerExit(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = false;
            text.text = "����";
        }
    }
}