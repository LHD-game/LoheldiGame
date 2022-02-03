using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;
    public bool NearNPC = false;

    void OnTriggerStay(Collider other)
    {
        if (NearNPC)
        {
            text.text = "��ȭ";
        }
        else
        {
            text.text = "����";
        }

        if (other.gameObject.tag == "NPC")
        {
            NearNPC = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        NearNPC = false;
    }
}
