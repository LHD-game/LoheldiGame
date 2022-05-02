using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
    public string NameNPC;


    /*private void OnControllerColloderHit(ControllerColliderHit hit) 
    {
        if (hit.collider.gameObject.name == "Nari")
        {
            Debug.Log("����");
        }

        if (hit.collider.gameObject.name == "Nari")
        {
            Debug.Log("����");
        }
    }*/

    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            NearNPC = true;
            //text.text = "��ȭ";
            Debug.Log("��ȭ");

            if (other.gameObject.name == "Nari")
            {
                Debug.Log("����");
            }
            else if (other.gameObject.name == "Nani")
            {
                Debug.Log("����");
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
            //text.text = "����";
            Debug.Log("����");
        }
    }
}
