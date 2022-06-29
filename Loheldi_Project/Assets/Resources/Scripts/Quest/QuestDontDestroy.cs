using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc;  //버튼 추가되는 NPC이름(퀘스트)
    public int QuestSubNum;         //퀘스트 스크립트 앞번호
    public int QuestIndex;   //몇번째 퀘스트차례인지 (서버저장)
    public bool QuestMail = false; //퀘스트 메일이 왔는지 확인하는 bool값
    public bool Quest =false; //퀘스트 완료했는지 확인하는 bool값
    public int LastDay=0;  //마지막으로 퀘스트를 클리어한 날짜(서버 저장)
    public int ToDay=0;      //오늘 날짜
    public List<int> badgeList = new List<int>();  //획득한 벳지 

    public bool tutorialLoading; //튜토리얼 진행 중인지(하우징갔다가 메인왔을 때 진행 시키는 용)
    //public bool RiciveQuest = false; //퀘스트를 받았는지 확인하는 bool값

    public int DayToday;     //이 둘은 뭐하는 애지 내가 만든건가
    public int DayYesterday;

    public Transform LastPlayerTransform;
    //public GameObject[] Mails;
    /// </summary>

    private void Awake()
    {
        Dontdestroy();
    }
    public void Dontdestroy()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            GameObject.Find("Player").transform.position = LastPlayerTransform.position;
        Debug.Log("플레이어 위치 설정"+ LastPlayerTransform.position);
        if (SceneManager.GetActiveScene().name == "Welcome")
            QuestIndex=0;    //나중에 서버랑 연동해서 초기값 0, 그 뒤값을 서버에 저장하고 다시 불러오는 식으로
        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

    }
}
