using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class CardGameManager : MonoBehaviour
{
    GameObject[] cards;     //ī�带 �����鿡�� ����
    GameObject[] cardsMix;  //cards �迭���� ���� ���� ����
    int[] level = { 0, 4, 6, 8 };      //���� �� ī�� ���� ����
    float[] timerLen = { 0f, 64f, 64f, 54f };
    public static float timer;
    private List<GameObject> AllCard = new List<GameObject>();    //������ ī�� ������Ʈ

    public static GameObject OpenCard;
    public static GameObject LastCard;
    public static bool GameStart;

    public Slider TimeSlider;
    private bool isPause = false; //true�϶� pause ����

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    int cardCnt;
    int flipCnt = 0;

    static public int stageNum = 1;
    [SerializeField]
    private Text stageTxt;
    public Text timeText;

    int a;

    public enum STATE   //���� ���� ���� ����
    {
        START, HIT, WAIT, IDLE, CLEAR, FAIL
    };
    static public STATE state;

    void Start()
    {
        Welcome();
    }

    void Update()
    {
        if (GameStart && !isPause)
        {
            switch (state)
            {
                case STATE.START:   //���� ����
                    CardSet();
                    break;
                case STATE.HIT:     //ī�� ������ ��
                    CheckCard();
                    break;
                case STATE.CLEAR:   //�� �������� Ŭ����
                    StartCoroutine(StageClear());
                    break;
                case STATE.WAIT:    //ī�带 ���� ��ٸ��� ����
                    timer -= Time.deltaTime;
                    break;
                case STATE.FAIL:    //�ð��� �� �Ǿ� ���ӿ���
                    StageFail();
                    break;
                case STATE.IDLE:    //�⺻ ����
                    timer -= Time.deltaTime;
                    break;
            }

            if (stageNum < 4)
            {
                if (timer <= timerLen[stageNum])
                {
                    timeText.text = string.Format("�ð�: {0:N0}", timer);
                }
                else
                {
                    a = (int)timerLen[stageNum] - 4;
                    timeText.text = "�ð�: " + a;
                }

                if (timer <= 0)
                {
                    state = STATE.FAIL;
                }
                TimeSlider.maxValue = a;
                TimeSlider.value = timer;
            }
            
        }
    }

    public void Welcome()   //���� �ʱ�ȭ �Լ�
    {
        state = STATE.START;
        GameStart = false;
        isPause = false;
        a = 0;
        cards = Resources.LoadAll<GameObject>("Prefebs/Cards/");    //ī����� ���� �����Ѵ�.
        stageNum = 1;   //�������� 1�� �ʱ�ȭ
        timer = timerLen[stageNum];

        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        

    }

    void CardSet()  // �������� ī�� ��� �迭�� ����
    {
        AllCard.Clear();

        StageText();

        int arrLen = level[stageNum]; //���������� ��ġ�� ī�� ���� ����
        cardCnt = arrLen * 2;   //���������� ��ġ�� ī���� �� ����
        timer = timerLen[stageNum];
        GameObject[] arrTmp = new GameObject[cardCnt]; //�� ī�� �� �� �徿 �����ϰ� �� ���̹Ƿ�
        Debug.Log("arrTmp: " + arrTmp.Length);
        for(int i = 0; i < arrLen; i++)
        {
            for (int j = 0; j<2; j++)
            {
                while (true)
                {
                    int rand = Random.Range(0, arrTmp.Length);
                    if(arrTmp[rand] == null)    //arrTmp[rand]�� ���� ���� ���, card[i]�� �ִ´�. �̸� �� �� �ݺ��Ͽ� ���� ī�尡 �� �� ����ǵ���.
                    {
                        arrTmp[rand] = cards[i];
                        Debug.Log("arrTmp: " + rand + ", card[i]: " + cards[i]);
                        break;
                    }
                }

            }
        }

        cardsMix = new GameObject[arrTmp.Length];   //�迭 ���� �ʱ�ȭ
        cardsMix = arrTmp; //���� ī�带 cardsMix �迭�� �ִ´�.

        StartCoroutine(MakeStage());
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

        stageNum++;
        flipCnt = 0;

        yield return new WaitForSeconds(1);
        state = STATE.START;
        if (stageNum == 4)
        {
            Clear();
            state = STATE.WAIT;
        }
    }

    void Clear()
    {
        GameOverPanel.SetActive(true);
        GameStart = false;
    }
    public void StageFail()
    {
        GameStart = false;
        GameOverPanel.SetActive(true);
        DestroyAll();
    }
    IEnumerator MakeStage()
    {
        state = STATE.WAIT;

        float sx = -2.05f;
        float sz = 3.32f;

        SetCardPos(out sx, out sz); //ī�� ��ġ ���� �Լ�

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

                        GameObject Tempcard = Instantiate(cardsMix[n-1]);
                        Tempcard.transform.position = new Vector3(x, 1f, sz);
                        AllCard.Add(Tempcard);  //���õ� ī�� ������Ʈ�� AllCard�� ����

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
        string[] str = SetStage.stage[stageNum-1];

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

    private void StageText()
    {
        stageTxt.text = stageNum + " �ܰ�";
    }

    public void GamePause() //�Ͻ� ���� ������ isPause = true, �簳 ������ isPause = false
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

    void DestroyAll()
    {
        for(int i=0; i<AllCard.Count; i++)
        {
            Destroy(AllCard[i]);
        }
        AllCard.Clear();
    }
}