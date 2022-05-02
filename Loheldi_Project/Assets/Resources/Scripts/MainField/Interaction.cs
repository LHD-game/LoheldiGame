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

            if (other.gameObject.name == "Nari")
            {
                NameNPC = "����";
            }
            else if (other.gameObject.name == "tiger")
            {
                NameNPC = "tiger";
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