using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Drawing Draw;
    private LodingTxt chat;
    int dest=0;
    void destroy(string DestryedObject)
    {
        ++dest;
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        if(dest == Draw.notes.Length)
        {
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
            chat.scriptLine();
            Debug.Log("´Ù¹ö¸²");
        }
        Destroy(GameObject.Find(DestryedObject));
    }
}
