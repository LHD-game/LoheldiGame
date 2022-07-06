using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    public Drawing Draw;
    private LodingTxt chat;
    /// 노트 애니메이션
    int dest=0;
    void destroy(string DestryedObject)
    {
        ++dest;
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        if(dest == Draw.notes.Length)
        {
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
            chat.scriptLine();
            Debug.Log("다버림");
        }
        Destroy(GameObject.Find(DestryedObject));
    }
    /// 자전거 애니메이션

    void SetPosition()
    {
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        Draw.WearOut();
    }
}
