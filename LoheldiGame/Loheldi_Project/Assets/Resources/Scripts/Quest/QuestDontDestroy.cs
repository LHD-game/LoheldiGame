using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc;  //��ư �߰��Ǵ� NPC�̸�(����Ʈ)
    public string From;  //��õ�غ��� ���� ��
    //public int QuestSubNum;         //����Ʈ ��ũ��Ʈ �չ�ȣ
    public string QuestIndex;   //���° ����Ʈ�������� (��������)

    public int LastDay=0;  //���������� ����Ʈ�� Ŭ������ ��¥(���� ����)
    public int ToDay=0;      //���� ��¥
    public List<int> badgeList = new List<int>();  //ȹ���� ���� 

    public bool ToothQ = false;
    public bool weekend = false;
    public bool SDA = false;
    public bool tutorialLoading=false; //Ʃ�丮�� ���� ������(�Ͽ�¡���ٰ� ���ο��� �� ���� ��Ű�� ��)
    //public bool RiciveQuest = false; //����Ʈ�� �޾Ҵ��� Ȯ���ϴ� bool��

    public GameObject LastPlayerTransform;
    //public GameObject[] Mails;
    /// </summary>

    void Awake()
    {
        Dontdestroy();
    }
    public void Dontdestroy()
    {
        //Debug.Log("����");
        LastPlayerTransform = this.gameObject;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");
        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }/*
        else if (SceneManager.GetActiveScene().name == "Game_Tooth")
        {
            Debug.Log("��ġ");
            Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/Canvas"), new Vector3(0, 0, -0), Quaternion.Euler(0, 0, 0));
            Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/QToothBrush"), new Vector3(0, 0, -0), Quaternion.Euler(0, 0, 0));
            GameObject.Find("Canvas").SetActive(false);
            GameObject.Find("Player").SetActive(false);
            GameObject.Find("mouth").SetActive(false);
        }*/
        DontDestroyOnLoad(this.gameObject);
    }

    public void WeekChange()
    {
        if(weekend)
            weekend=false;
        else
            weekend=true;
    }
}
