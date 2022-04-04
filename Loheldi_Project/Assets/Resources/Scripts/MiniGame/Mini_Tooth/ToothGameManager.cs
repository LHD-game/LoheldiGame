using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToothGameManager : MonoBehaviour
{
    private static ToothGameManager _instance;
    public static ToothGameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ToothGameManager>();
            }
            return _instance;
        }
    }

    //난이도 별 시간 측정 변수와 배열
    float[] timerLen = { 0f, 30f, 45f, 60f };
    float[] repeatLen = { 0f, 3.0f, 2.25f, 2.0f };
    public static float timer;
    [SerializeField]
    private Text timerTxt;

    public static int difficulty = 3;
    private int[] bTNum = { 0, 1, 2, 3 };   //difficulty에 따라 한번에 blackTooth가 되는 갯수
    public static int k = 0;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Transform blackToothP;   //검은 이빨 부모 오브젝트
    private Transform[] blackTooth; //검은 이빨들 들어가도록

    int rNum = 0;
    private bool isRun = false;
    public static bool isPause = false;

    public static int BlackCount = 0;

    public GameObject WinText;
    public GameObject falseText;

    [SerializeField]
    private GameObject WelcomePanel;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private GameObject PausePanel;
    void Start()
    {
        //자식오브젝트들을 전부 배열에 저장한다.
        blackTooth = blackToothP.gameObject.GetComponentsInChildren<Transform>();
        Welcome();
    }

    void Update()
    {
        if (isRun && !isPause)
        {
            timer -= Time.deltaTime;
            timerTxt.text = $"{timer:N2}";
            if (timer < 0)
            {
                timer = 0;
                timerTxt.text = $"{timer:N2}";
                GameOver();
            }
        }
    }

    void BlackToothArr()    //검은 이빨 초기화 함수
    {
        Debug.Log("blackTooth len: " + blackTooth.Length);
        for(int i=0; i<blackTooth.Length; i++)
        {
            //blackTooth 오브젝트들 비활성화 상태로 변경
            blackTooth[i].gameObject.SetActive(false);
        }
        blackToothP.gameObject.SetActive(true); //꺼진 부모 오브젝트 활성화
    }

    public void Welcome()  //초기화 함수
    {
        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        isRun = false;
        isPause = false;
        BlackCount = 0;
        timerTxt.text = "";
        WinText.SetActive(false);
        falseText.SetActive(false);

        Player.transform.position = new Vector3(0f, 2.5f, 10f);

        BlackToothArr();
        ToothCountDown.instance.ResetTimer();
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if (isPause)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
    }

    public void GameStart()
    {
        timer = timerLen[difficulty];   //난이도 선택 시 난이도에 따른 시간을 timer 변수에 저장.
        isRun = true;
        InvokeRepeating("BoxRandom", repeatLen[difficulty], repeatLen[difficulty]);
        Debug.Log("GameStart()");
    }

    void BoxRandom()
    {
        Debug.Log("BoxRandom()");
        if (!isPause)
        {
            for (int i = 0; i < bTNum[difficulty];) //난이도별로 한 번에 나오는 blackTooth의 갯수가 다르게
            {
                rNum = Random.Range(1, 20);
                if (BlackCount >= 13)
                {
                    GameOver();
                    break;
                }

                else
                {
                    if (blackTooth[rNum].gameObject.activeSelf == true)    //해당 blackTooth가 이미 활성화되어있는 경우
                    {
                        continue;
                    }
                    else if (blackTooth[rNum].gameObject.tag != "BTooth")   //Tag가 BTooth가 아닌경우(MovePosition인 경우)
                    {
                        continue;
                    }
                    else
                    {
                        i++;
                        BlackCount++;
                        blackTooth[rNum].gameObject.SetActive(true);
                        Debug.Log("BlackTooth[]: " + blackTooth[rNum].gameObject);
                    }
                }

            }
        }
        


    }

     public void GameOver()
     {
         isRun = false;
        isPause = true; //종료 후 캐릭터 못움직이게하기
         GameOverPanel.SetActive(true);
         CancelInvoke("BoxRandom");
         if (timer <= 0)  //승리 조건
         {
             WinText.SetActive(true);
         }
         else if(BlackCount >= 13)   //패배 조건
         {
             falseText.SetActive(true);
         }
         else
         {
             Welcome();
         }
         //그 외의 경우: 일시정지에서 처음으로 돌아가기 선택

     }
}
