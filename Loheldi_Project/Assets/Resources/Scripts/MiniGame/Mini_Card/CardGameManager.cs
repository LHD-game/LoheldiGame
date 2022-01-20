using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class CardGameManager : MonoBehaviour
{
    public List<GameObject> CardList = new List<GameObject>();
    public List<GameObject> TempCardList = new List<GameObject>();
    private List<GameObject> MixCardList = new List<GameObject>();
    private List<GameObject> AllCard = new List<GameObject>();

    public static GameObject OpenCard;
    public static GameObject LastCard;
    public static bool GameStart;
    public GameObject FndText;
    public GameObject EndText;

    public Slider TimeSlider;

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;

    int cardCnt;
    int flipCnt = 0;

    static public int stageNum = 1;
    int stageCount = 3;
    public static float Timer = 64f;
    public Text timeText;

    int a;

    public enum STATE   //현재 게임 상태 저장
    {
        START, HIT, WAIT, IDLE, CLEAR, FAIL
    };
    static public STATE state;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (GameStart)
        {
            switch (state)
            {
                case STATE.START:   //게임 시작
                    SetUp();
                    break;
                case STATE.HIT:     //카드 눌렀을 때
                    CheckCard();
                    break;
                    break;
                case STATE.CLEAR:   //한 스테이지 클리어
                    StartCoroutine(StageClear());
                    break;
                case STATE.WAIT:    //카드를 열고 기다리는 상태
                    Timer -= Time.deltaTime;
                    break;
                case STATE.FAIL:    //시간이 다 되어 게임오버
                    StageFail();
                    break;
                case STATE.IDLE:    //기본 상태
                    Timer -= Time.deltaTime;
                    break;
            }
            if (stageNum == 3)
            {
                if (Timer <= 50f)
                {
                    timeText.text = string.Format("시간: {0:N0}", Timer);
                }
                else
                {
                    a = 50;
                    timeText.text = "시간: " + a;
                }
            }
            else
            {
                if (Timer <= 60f)
                {
                    timeText.text = string.Format("시간: {0:N0}", Timer);
                }
                else
                {
                    a = 60;
                    timeText.text = "시간: " + a;
                }
            }
            if (Timer <= 0)
            {
                state = STATE.FAIL;
            }
            TimeSlider.maxValue = a;
            TimeSlider.value = Timer;
        }
    }

    public void Reset()
    {
        state = STATE.START;
        GameStart = false;
        a = 0;

        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    void CheckCard()
    {
        state = STATE.WAIT;

        if (LastCard == null)
        {
            LastCard = OpenCard;
            state = STATE.IDLE;
            return;
        }
        else if (LastCard.gameObject.tag != OpenCard.gameObject.tag)
        {
            StartCoroutine(CloseTwoCards());
            return;
        }
        else if (LastCard.gameObject.tag == OpenCard.gameObject.tag)
        {
            flipCnt = flipCnt + 2;

            LastCard.SendMessage("DestroyCard", SendMessageOptions.DontRequireReceiver);
            OpenCard.SendMessage("DestroyCard", SendMessageOptions.DontRequireReceiver);

            if (flipCnt == cardCnt)
            {
                state = STATE.CLEAR;
                return;
            }
            LastCard = null;
            OpenCard = null;
        }
        LastCard = null;
        state = STATE.IDLE;
    }
    void SetUp()
    {
        TempCardList.Clear();
        AllCard.Clear();

        if (stageNum == 1)
        {
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[3]);
            TempCardList.Add(CardList[3]);
            cardCnt = 8;
            Timer = 64f;
        }
        else if (stageNum == 2)
        {
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[3]);
            TempCardList.Add(CardList[3]);
            TempCardList.Add(CardList[4]);
            TempCardList.Add(CardList[4]);
            TempCardList.Add(CardList[5]);
            TempCardList.Add(CardList[5]);
            //TempCardList.Add(CardList[6]);
            //TempCardList.Add(CardList[6]);
            //TempCardList.Add(CardList[7]);
            //TempCardList.Add(CardList[7]);
            cardCnt = 12;
            Timer = 64f;
        }
        else if (stageNum == 3)
        {
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[0]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[1]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[2]);
            TempCardList.Add(CardList[3]);
            TempCardList.Add(CardList[3]);
            TempCardList.Add(CardList[4]);
            TempCardList.Add(CardList[4]);
            TempCardList.Add(CardList[5]);
            TempCardList.Add(CardList[5]);
            TempCardList.Add(CardList[6]);
            TempCardList.Add(CardList[6]);
            TempCardList.Add(CardList[7]);
            TempCardList.Add(CardList[7]);
            //TempCardList.Add(CardList[8]);
            //TempCardList.Add(CardList[8]);
            //TempCardList.Add(CardList[9]);
            //TempCardList.Add(CardList[9]);
            //TempCardList.Add(CardList[10]);
            //TempCardList.Add(CardList[10]);
            //TempCardList.Add(CardList[11]);
            //TempCardList.Add(CardList[11]);
            cardCnt = 16;//24;
            Timer = 54f;
        }
        StartCoroutine(MakeStage());
    }
    void Clear()
    {
        EndText.SetActive(true);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator CloseTwoCards()
    {
        yield return new WaitForSeconds(0.2f);
        LastCard.SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);
        OpenCard.SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);

        LastCard = null;
        OpenCard = null;
        state = STATE.IDLE;
    }
    IEnumerator StageClear()
    {
        state = STATE.WAIT;

        ++stageNum;
        flipCnt = 0;

        yield return new WaitForSeconds(1);
        state = STATE.START;
        if (stageNum == 4)
        {
            Clear();
            state = STATE.WAIT;
        }
    }
    void StageFail()
    {

        FndText.SetActive(true);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator MakeStage()
    {
        state = STATE.WAIT;

        float sx = -2.05f;
        float sz = 3.32f;
        ShuffleCard();
        SetCardPos(out sx, out sz);

        int n = 1;

        string[] str = SetStage.stage[stageNum - 1];
        foreach (string t in str)
        {
            char[] ch = t.Trim().ToCharArray();
            float x = sx;

            foreach (char c in ch)
            {

                switch (c)
                {
                    case '*':

                        GameObject Tempcard = Instantiate(TempCardList[n - 1]);
                        Tempcard.transform.position = new Vector3(x, 4.84f, sz);
                        AllCard.Add(Tempcard);

                        x = x + 1.35f;
                        n++;
                        break;
                    case '.':
                        x = x + 1.35f;
                        break;
                }

                if (c == '*')
                {
                    yield return new WaitForSeconds(0.03f);
                }
            }
            sz = sz - 2.13f;
        }
        for (int k = 0; k != cardCnt; k++)
        {
            AllCard[k].SendMessage("OpenCard", SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitForSeconds(3);
        for (int k = 0; k != cardCnt; k++)
        {
            AllCard[k].SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);
        }
        state = STATE.IDLE;
    }

    void SetCardPos(out float sx, out float sz)
    {
        float x = -2.05f;
        float z = 3.32f;
        string[] str = SetStage.stage[stageNum - 1];

        for (int i = 0; i < str.Length; i++)
        {
            string t = str[i].Trim();
            x = 0;

            for (int j = 0; j < t.Length; j++)
            {
                switch (t[j])
                {
                    case '.':
                    case '*':
                        x++;
                        break;
                }
            }
            z = z + 0.5f;
        }
        if (stageNum == 1)
        {
            sx = -1.45f;
            sz = 2.13f;
        }
        else
        {
            sx = -2.05f;
            sz = 3.32f;
        }
    }
    
    void ShuffleCard()
    {
        int count = TempCardList.Count;
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, TempCardList.Count);
            MixCardList.Add(TempCardList[rand]);
            TempCardList.RemoveAt(rand);
        }
        TempCardList = MixCardList;
    }
}