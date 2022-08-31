using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuestScript : MonoBehaviour
{
    public bool Quest = false;               //현재 받은 퀘스트 메일이 있는지 확인하는 함수 (MailDontDestroy 211참조)
    [SerializeField]

    public MailLoad Mail;
    public LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;

    public bool note=false;
    public bool farm = false;

    private int QuestNum;
    public FlieChoice file;

    public QuestDontDestroy DontDestroy;

    
    public void QuestStart()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Debug.Log("퀘스트스크립트스타트실행" + String.IsNullOrEmpty(DontDestroy.ButtonPlusNpc) + DontDestroy.QuestMail);

        if (PlayerPrefs.GetString("QuestPreg").Equals("0_0"))
        {
            file.Tutorial();
            DontDestroy.QuestIndex = "0_1";
            Debug.Log("튜토리얼");
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("0_1"))
        {
            StartCoroutine("QFarmLoop");
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("22_1"))
        {
            Debug.Log("양치");
            DontDestroy.ToothQ = true;
        }
        else if (DontDestroy.LastDay != DontDestroy.ToDay)
        {
            QuestChoice();
        }

    }

    public void QuestChoice()
    {
        if (DontDestroy.QuestIndex.Equals("4_1"))
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
        else if (DontDestroy.QuestIndex.Equals("8_1"))
        {
            note = true;
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (DontDestroy.QuestIndex.Equals("11_1")|| DontDestroy.QuestIndex.Equals("14_1")|| DontDestroy.QuestIndex.Equals("18_1")|| DontDestroy.QuestIndex.Equals("20_1")|| DontDestroy.QuestIndex.Equals("22_1"))
        {
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(54, 5, 37);
        }
        else if (DontDestroy.QuestIndex.Equals("13_1"))
        {
            note = true;
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(125, 15, 170);
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 77, 0));
        }
        else if (DontDestroy.QuestIndex.Equals("19_1"))
        {
            //GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(125, 15, 170);
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 157, 0));

            GameObject NariIm = GameObject.Find("Nari");
            NariIm.transform.position = new Vector3(68, 5, -16);
            NariIm.transform.rotation = Quaternion.Euler(new Vector3(0, 207, 0));
        }
        else if (DontDestroy.QuestIndex.Equals("21_1")|| DontDestroy.QuestIndex.Equals("23_1")|| DontDestroy.QuestIndex.Equals("24_1"))
        {
            Instantiate(Resources.Load<GameObject>("Models/NPC/npc/parents"), new Vector3(30, 5, 33), Quaternion.Euler(new Vector3(0, 133, 0)));
        }
        if (SceneManager.GetActiveScene().name == "MainField")
            ExclamationMarkCreate();
    }


    IEnumerator QFarmLoop()
    {
        while (true)
        {
            GameObject click = EventSystem.current.currentSelectedGameObject;
            if (click == null)
            { }
            else if (click.name.Equals("ItemBtn_"))
            {
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                PlayInfoManager.GetQuestPreg();
                farm = false;
                chat.QuestLoad.QuestLoadStart();
                StopCoroutine("QFarmLoop");
            }
            yield return null;
        }
    }
    private void ExclamationMarkCreate()
    {
        Transform Parent = GameObject.Find(DontDestroy.ButtonPlusNpc).GetComponent<Transform>();
        GameObject child;
        child = Instantiate(ExclamationMark[1], GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position+new Vector3(0,6,0), GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation);
        child.transform.parent = Parent;
        file.EPin.SetActive(true);
        file.EPin.GetComponent<MapPin>().Owner = GameObject.Find(DontDestroy.ButtonPlusNpc);

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
