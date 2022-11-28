using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    public Drawing Draw;
    private LodingTxt chat; 
    public GameObject SoundEffectManager;
    public GameObject[] notes;
    /// 노트 애니메이션
    int dest=0;
    public void Destroy(int DestryedObject)
    {
        ++dest;
        if(dest == Draw.notes.Length)
        {
            chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
            chat.scriptLine();
            //Debug.Log("다버림");
        }
        Destroy(notes[DestryedObject]);
    }
    public Sprite noteImageSprite;
    static Image noteImage;
    public void NoteGone1(int Object)
    {
        SoundEffectManager.GetComponent<SoundEffect>().Sound("Paper");
        noteImageSprite = Resources.Load<Sprite>("Sprites/Quest/Note/note1");
        noteImage = notes[Object].GetComponent<Image>();
        noteImage.sprite = noteImageSprite;
    }
    public void NoteGone2(int Object)
    {
        GameObject notea = notes[Object];
        Transform[] allChildren = notea.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name == notea.name)
            { }
            else
                child.gameObject.SetActive(false);
        }
        noteImageSprite = Resources.Load<Sprite>("Sprites/Quest/Note/note2");
        noteImage = notea.GetComponent<Image>();
        noteImage.sprite = noteImageSprite;
    }
    /// 자전거 애니메이션

    public void SetPosition()
    {
        Draw.WearOut();
    }
    public void FinishNutrient()
    {
        Draw.Cook();
    }
}
