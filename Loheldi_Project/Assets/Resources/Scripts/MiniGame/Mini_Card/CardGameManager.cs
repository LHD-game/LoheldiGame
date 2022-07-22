using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    GameObject[] cards;     //ī�带 �����鿡�� ����
    GameObject[] cardsMix;  //cards �迭���� ���� ���� ����
    int[] level = { 0, 4, 5, 6, 8 };      //���� �� ī�� ���� ����
    
    private List<GameObject> AllCard = new List<GameObject>();    //������ ī�� ������Ʈ

    public static GameObject OpenCard;
    public static GameObject LastCard;
    public static bool GameStart;

    private bool isPause = false; //true�϶� pause ����

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    //��� ���
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    int cardCnt;
    int flipCnt = 0;

    static public int stageNum = 1;
    [SerializeField]
    private Text stageTxt;

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
                    Card_TimeSlider.instance.TimeDel();
                    break;
                case STATE.FAIL:    //�ð��� �� �Ǿ� ���ӿ���
                    StageFail();
                    break;
                case STATE.IDLE:    //�⺻ ����
                    Card_TimeSlider.instance.TimeDel();
                    break;
            }
            //�ð��� 0�� �Ǹ� ���� ����
            if (Card_TimeSlider.nowTime <= 0)
            {
                state = STATE.FAIL;
            }
            
            
        }
    }

    public void Welcome()   //���� �ʱ�ȭ �Լ�
    {
        state = STATE.START;
        GameStart = false;
        isPause = false;
        a = 0;
        cards = Resources.LoadAll<GameObject>("Prefabs/Cards/");    //ī����� ���� �����Ѵ�.
        stageNum = 1;   //�������� 1�� �ʱ�ȭ
        //Card_TimeSlider.instance.TimeInit();

        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        

    }

    void CardSet()  // �������� ī�� ��� �迭�� ����
    {
        AllCard.Clear();

        StageText();
        int arrLen;
        if (stageNum <= 4)//���������� ��ġ�� ī�� ���� ����
        {
            arrLen = level[stageNum];
        }
        else    //4�ܰ���ʹ� 3�ܰ�� �����ϰ�
        {
            arrLen = level[4];
        }
         
        cardCnt = arrLen * 2;   //���������� ��ġ�� ī���� �� ����
        Card_TimeSlider.instance.TimeAdd();   //10�� ����
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
    }

    void Clear()
    {
        GameOverPanel.SetActive(true);
        GameStart = false;
        GameResult();
    }
    public void StageFail()
    {
        GameStart = false;
        GameOverPanel.SetActive(true);
        DestroyAll();
        GameResult();
    }

    void GameResult()   //������ ���� ���� ȹ�� �޼ҵ�
    {
        float get_exp = 10f;
        int get_coin = stageNum * 5;    //�ܰ躰�� * 5 (5, 10, 15, 20, 25...)
        

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //���� ����� ȭ�鿡 ���
        ResultTxt.text = stageNum + " �ܰ�";
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    IEnumerator MakeStage()
    {
        state = STATE.WAIT;

        float sx = -1.2f;
        float sz = 4.2f;
        if(stageNum >= 3)
        {
            sz = 3.3f;
        }
        if(stageNum >= 4)
        {
            sx = -2.0f;
        }

        int n = 1;

        string[] str;
        if (stageNum <= 4)
        {
            str = SetStage.stage[stageNum - 1];
        }
        else
        {
            str = SetStage.stage[3];
        }
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
                        Tempcard.transform.GetComponent<CardCtrl>().SoundManager = this.gameObject;
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