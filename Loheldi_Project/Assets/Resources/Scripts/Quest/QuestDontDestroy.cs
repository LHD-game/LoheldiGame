using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestDontDestroy : MonoBehaviour
{

    //public string[] ButtonPlusNpc = new string[2]{"",""};

    public string ButtonPlusNpc; 
    public int QuestSubNum;         //퀘스트 스크립트 앞번호
    public int QuestIndex;   //몇번째 퀘스트차례인지
    public bool QuestMail = false; //퀘스트 메일이 왔는지 확인하는 bool값
    public bool Quest =false; //퀘스트 완료했는지 확인하는 bool값
    public int LastDay=0;  //마지막으로 퀘스트를 클리어한 날짜
    public int ToDay=0;      //오늘 날짜
    //public bool RiciveQuest = false; //퀘스트를 받았는지 확인하는 bool값
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
        Debug.Log("돈디스트로이 스타ㅡ");
        if (SceneManager.GetActiveScene().name == "Welcome")
            QuestIndex=0;    //나중에 서버랑 연동해서 초기값 0, 그 뒤값을 서버에 저장하고 다시 불러오는 식으로
        DontDestroyOnLoad(GameObject.Find("DontDestroyQuest"));
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Quest");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

    }
}
