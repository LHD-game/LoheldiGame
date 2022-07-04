using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc;  //��ư �߰��Ǵ� NPC�̸�(����Ʈ)
    //public int QuestSubNum;         //����Ʈ ��ũ��Ʈ �չ�ȣ
    public int QuestIndex;   //���° ����Ʈ�������� (��������)
    public bool QuestMail = false; //����Ʈ ������ �Դ��� Ȯ���ϴ� bool��
    public int LastDay=0;  //���������� ����Ʈ�� Ŭ������ ��¥(���� ����)
    public int ToDay=0;      //���� ��¥
    public List<int> badgeList = new List<int>();  //ȹ���� ���� 

    public bool tutorialLoading=false; //Ʃ�丮�� ���� ������(�Ͽ�¡���ٰ� ���ο��� �� ���� ��Ű�� ��)
    //public bool RiciveQuest = false; //����Ʈ�� �޾Ҵ��� Ȯ���ϴ� bool��

    public int DayToday;     //�� ���� ���ϴ� ���� ���� ����ǰ�
    public int DayYesterday;

    public GameObject LastPlayerTransform;
    //public GameObject[] Mails;
    /// </summary>

    void Awake()
    {
        Dontdestroy();
    }
    public void Dontdestroy()
    {
        LastPlayerTransform = this.gameObject;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");
        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }
        if (SceneManager.GetActiveScene().name == "Welcome")
            //QuestIndex=0;    //���߿� ������ �����ؼ� �ʱⰪ 0, �� �ڰ��� ������ �����ϰ� �ٽ� �ҷ����� ������
        DontDestroyOnLoad(this.gameObject);
        GameObject.Find("Player").transform.position = LastPlayerTransform.transform.position;
        Debug.Log("�÷��̾� ��ġ ����" + LastPlayerTransform.transform.position);
    }
    public void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            GameObject.Find("Player").transform.position = LastPlayerTransform.transform.position;
            Debug.Log("�÷��̾� ��ġ ����" + LastPlayerTransform.transform.position);
        }
    }
}
