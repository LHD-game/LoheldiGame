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
            LastPlayerTransform.transform.position = new Vector3(46.8151436f, 5.57000017f, 55.7096672f);
        //QuestIndex=0;    //���߿� ������ �����ؼ� �ʱⰪ 0, �� �ڰ��� ������ �����ϰ� �ٽ� �ҷ����� ������
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnEnable()
    {
        Debug.Log("onEnable");
    }
}
