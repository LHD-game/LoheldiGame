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
            //Debug.Log("다버림");
        }
        Destroy(GameObject.Find(DestryedObject));
    }
    public Sprite noteImageSprite;
    static Image noteImage;
    void noteGone1(string Object)
    {
        noteImageSprite = Resources.Load<Sprite>("Sprites/Quest/Note/note1");
        noteImage = GameObject.Find(Object).GetComponent<Image>();
        noteImage.sprite = noteImageSprite;
    }
    void noteGone2(string Object)
    {
        GameObject notea = GameObject.Find(Object);
        notea.transform.GetChild(2).gameObject.SetActive(false);
        noteImageSprite = Resources.Load<Sprite>("Sprites/Quest/Note/note2");
        noteImage = GameObject.Find(Object).GetComponent<Image>();
        noteImage.sprite = noteImageSprite;
    }
    /// 자전거 애니메이션

    void SetPosition()
    {
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        Draw.WearOut();
    }
}
