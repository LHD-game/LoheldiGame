using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};
    public string ButtonPlusNpc;
    public int QuestSubNum;                                  //����Ʈ ��ũ��Ʈ �չ�ȣ
    private void Start()
    {

        DontDestroyOnLoad(GameObject.Find("DontDestroyQuest"));
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

    }
}
