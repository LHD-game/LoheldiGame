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
    public bool Quest = false;               //���� ���� ����Ʈ ������ �ִ��� Ȯ���ϴ� �Լ� (MailLoad 211����)
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
        Debug.Log("����Ʈ��ũ��Ʈ��ŸƮ����" + String.IsNullOrEmpty(Load.ButtonPlusNpc) + Load.QuestMail);

        if (PlayerPrefs.GetString("QuestPreg").Equals("0_0"))
        {
            file.Tutorial();
            Load.QuestIndex = "0_1";
            Debug.Log("Ʃ�丮��");
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("0_1"))
        {
            Debug.Log("��");
            farm = true;
        }
        else if (Load.LastDay != Load.ToDay)
        {
            QuestChoice();
        }

    }

    private void QuestCheck()
    {
        Debug.Log("����Ʈ üũ ����");
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        MainQuestLoding();
    }

    public void MainQuestLoding()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            Mail = GameObject.Find("MailManager").GetComponent<MailLoad>();
            Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        }
        Debug.Log("����Ʈ ��ȣ" + Load.QuestIndex);
        QuestChoice();
        
    }
    int Q = 0;
private void GiveQuest()
    {
        
        for (int k = 0; k <= Quest_Mail.Count; k++)
        {
            if (Quest_Mail[k]["QID"].ToString().Equals(Load.QuestIndex))
            {
                Q= k;
                break;
            }
            else
            {
                continue;
            }
        }
        Load.QuestMail = true;
        Load.ButtonPlusNpc = Quest_Mail[Q]["authorName"].ToString(); ;
        //GameObject temp = Resources.Load<GameObject>("Prefabs/UI/QuestMail") as GameObject;

        QuestChoice();
    }

    private void CheckMail()
    {
        Load.QuestMail = true;
        Mail.NewMailCheck();
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
        else if (Load.QuestIndex.Equals("13_1"))
        {
            note = true;
            GameObject.Find(Load.ButtonPlusNpc).transform.position = new Vector3(170, 15, -122);
            GameObject.Find(Load.ButtonPlusNpc).transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Load.QuestIndex.Equals("14_1"))
        {
            chat.NPCRope.SetActive(true);
        }
        else if (Load.QuestIndex.Equals("21_1"))
        {
            Instantiate(Resources.Load<GameObject>("Models/NPC/parents"), new Vector3(30, 5, 33), Quaternion.Euler(new Vector3(0, 133, 0)));
        }
        if (SceneManager.GetActiveScene().name == "MainField")
            ExclamationMarkCreate();
    }

    private void ExclamationMarkCreate()
    {
        Instantiate(ExclamationMark[1], GameObject.Find(Load.ButtonPlusNpc).transform.position+new Vector3(0,6,0), GameObject.Find(Load.ButtonPlusNpc).transform.rotation);
        file.EPin.SetActive(true);
        file.EPin.GetComponent<MapPin>().Owner = GameObject.Find(Load.ButtonPlusNpc);
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
