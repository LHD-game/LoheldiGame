using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class QuestScript : MonoBehaviour
{
    public Transform QMailList;
    public bool Quest = false;               //현재 받은 퀘스트 메일이 있는지 확인하는 함수 (MailLoad 211참조)
    [SerializeField]

    public MailLoad Mail;
    private LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;
    public List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    public bool note=false;
    public bool farm = false;

    private int QuestNum;
    FlieChoice file;

    public QuestDontDestroy Load;

    public void QuestStart()
    {
        file = GameObject.Find("chatManager").GetComponent<FlieChoice>();
        Load = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Debug.Log("퀘스트스크립트스타트실행" + String.IsNullOrEmpty(Load.ButtonPlusNpc) + Load.QuestMail);

        if (PlayerPrefs.GetString("QuestPreg").Equals("0_0"))
        {
            file.Tutorial();
            Load.QuestIndex = "0_1";
            Debug.Log("튜토리얼");
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("0_1"))
        {
            Debug.Log("팜");
            farm = true;
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("22_1"))
        {
            Debug.Log("양치");
            Load.ToothQ = true;
        }
        else if (Load.LastDay != Load.ToDay)
        {
            QuestChoice();
        }

    }

    public void QuestChoice()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
        if (Load.QuestIndex.Equals("4_1"))
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
        else if (Load.QuestIndex.Equals("8_1"))
        {
            note = true;
            GameObject.Find(Load.ButtonPlusNpc).transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Load.QuestIndex.Equals("6_1")|| Load.QuestIndex.Equals("7_1")|| Load.QuestIndex.Equals("11_1")|| Load.QuestIndex.Equals("18_1")|| Load.QuestIndex.Equals("20_1")|| Load.QuestIndex.Equals("22_1")|| Load.QuestIndex.Equals("25_1"))
        {
            GameObject.Find(Load.ButtonPlusNpc).transform.position = new Vector3(54, 5, 37);
        }
        else if (Load.QuestIndex.Equals("13_1"))
        {
            note = true;
            GameObject.Find(Load.ButtonPlusNpc).transform.position = new Vector3(125, 15, 170);
            GameObject.Find(Load.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 77, 0));
        }
        else if (Load.QuestIndex.Equals("14_1"))
        {
            chat.NPCRope.SetActive(true);
        }
        else if (Load.QuestIndex.Equals("19_1"))
        {
            GameObject.Find("Nari").transform.position = new Vector3(68, 5, -20);
            GameObject.Find(Load.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 30, 0));
        }
        else if (Load.QuestIndex.Equals("21_1")|| Load.QuestIndex.Equals("23_1")|| Load.QuestIndex.Equals("24_1"))
        {
            Instantiate(Resources.Load<GameObject>("Models/NPC/npc/parents"), new Vector3(30, 5, 33), Quaternion.Euler(new Vector3(0, 133, 0)));
        }
        if (SceneManager.GetActiveScene().name == "MainField")
            ExclamationMarkCreate();
    }

    private void ExclamationMarkCreate()
    {
        Transform Parent = GameObject.Find(Load.ButtonPlusNpc).GetComponent<Transform>();
        GameObject child;
        child = Instantiate(ExclamationMark[1], GameObject.Find(Load.ButtonPlusNpc).transform.position+new Vector3(0,6,0), GameObject.Find(Load.ButtonPlusNpc).transform.rotation);
        child.transform.parent = Parent;
        file.EPin.SetActive(true);
        file.EPin.GetComponent<MapPin>().Owner = GameObject.Find(Load.ButtonPlusNpc);

        GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");
        if(clone.Length >2)
            Destroy(clone[0]);
    }

    public void ChangeDrawCamera()
    {
        if (DrawCamera.enabled == false)
        {
            Draw=true;
            MainCamera.enabled = false;
            DrawCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            DrawCamera.enabled = false;
        }
    }
}
