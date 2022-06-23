using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc; 
    public int QuestSubNum;         //����Ʈ ��ũ��Ʈ �չ�ȣ
    public int QuestIndex;   //���° ����Ʈ��������
    public bool QuestMail = false; //����Ʈ ������ �Դ��� Ȯ���ϴ� bool��
    public bool Quest =false; //����Ʈ �Ϸ��ߴ��� Ȯ���ϴ� bool��
    public int LastDay=0;  //���������� ����Ʈ�� Ŭ������ ��¥
    public int ToDay=0;      //���� ��¥
    //public bool RiciveQuest = false; //����Ʈ�� �޾Ҵ��� Ȯ���ϴ� bool��
    public int DayToday;
    public int DayYesterday;
    //public GameObject[] Mails;
    /// </summary>
    
    private void Start()
    {
        Dontdestroy();
    }
    public void Dontdestroy()
    {
        Debug.Log("����Ʈ���� ��Ÿ��");
        if (SceneManager.GetActiveScene().name == "Welcome")
            QuestIndex=0;    //���߿� ������ �����ؼ� �ʱⰪ 0, �� �ڰ��� ������ �����ϰ� �ٽ� �ҷ����� ������
        DontDestroyOnLoad(GameObject.Find("DontDestroyQuest"));
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

    }
}
