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
            text.text = "대화";
        }
        else
        {
            text.text = "점프";
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
