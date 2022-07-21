using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunGameManager : MonoBehaviour
{
    public static int difficulty = 0;

    public Transform[] NPC = new Transform[4];  //none, hami, nari, himchan 순서대로
    public Transform[] MAP = new Transform[4];  //들판, 바다, 하늘 순서대로
    
    public Transform Player;
    private float NPCz;
    private float Playerz;
    public Rigidbody player;

    public Transform NMarker;
    public Transform PMarker;

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject RunBtnPanel;

    //결과 출력
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;
    static bool is_win = false; //승리시 true

    private Vector3 markerPos;

    public Transform EndLine;
    public Transform EndBlock;
    public static int Goll;  //골지점
    public static bool isPause = false;
    RunPlayerRun Nownpc;
    RunCountDown Count;

    public GameObject SoundManager;
    public bool FinishSound;

    void Start()
    {
        markerPos = NMarker.localPosition;
        Reset();
        MAP[0].gameObject.SetActive(true);
        Nownpc = GameObject.Find("Player").GetComponent<RunPlayerRun>();
        Count = GameObject.Find("CountDown").GetComponent<RunCountDown>();
    }

    public Transform nowNPC;
    private Transform nowMAP;
    void Update()
    {
        if (difficulty != 0)
        {
            nowNPC = NPC[difficulty];
            nowNPC.gameObject.SetActive(true);

            MAP[0].gameObject.SetActive(false);
            nowMAP = MAP[difficulty];
            nowMAP.gameObject.SetActive(true);
            Nownpc.NPC_ = nowNPC;
            //Nownpc.NPC_ = nowNPC;

            EndMove();

            if (nowNPC.position.z >= Goll)  //패배
            {
                is_win = false;
                nowNPC.gameObject.GetComponent<RunNPC>().enabled = false;
                RunBtnPanel.SetActive(false);
                GameOverPanel.SetActive(true);

                

                if (!FinishSound)
                {
                    GameResult();   // 1번만 실행되어야 해서 이 조건문에 낑겨 넣었습니다^^;
                    SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                    sound.Loose();
                    FinishSound = true;
                }
            }
            else if (Player.position.z >= Goll) //승리
            {
                is_win = true;
                nowNPC.gameObject.GetComponent<RunNPC>().enabled = false;
                RunBtnPanel.SetActive(false);
                GameOverPanel.SetActive(true);

                

                if (!FinishSound)
                {
                    GameResult();
                    SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                    sound.Win();
                    FinishSound = true;
                }
            }
            NPCz = nowNPC.position.z;
            Playerz = Player.position.z;

            PMarker.localPosition = new Vector3(Playerz / Goll * 2560 - 1365, 180, 0);      // 플레이어 위치 / 트랙길이 * 미터라인 길이 - 1370
            NMarker.localPosition = new Vector3(NPCz / Goll * 2560 - 1365, 180, 0);         //  플레이어 위치에 백분률  * 미터기 길이   + 위치조정

            player.velocity = player.velocity / 1.0085f;
        }
    }

    public void Reset()
    {
        //player, npc, marker 위치 원상복귀, difficulty 값 초기화(0), welcomepanel 활성화, 기타 비활성화
        Vector3 startPpos = new Vector3(100.0f, 0.0f, 0.0f);
        Vector3 startNpos = new Vector3(0.0f, 0.0f, 0.0f);

        Player.position = startPpos;
        NPC[1].position = startNpos;
        NPC[2].position = startNpos;
        NPC[3].position = startNpos;

        NMarker.localPosition = markerPos;
        PMarker.localPosition = markerPos;

        NPC[1].gameObject.SetActive(false);
        NPC[2].gameObject.SetActive(false);
        NPC[3].gameObject.SetActive(false);

        MAP[1].gameObject.SetActive(false);
        MAP[2].gameObject.SetActive(false);
        MAP[3].gameObject.SetActive(false);
        MAP[0].gameObject.SetActive(true);

        difficulty = 0;
        isPause = false;

        GameOverPanel.SetActive(false);
        WelcomePanel.SetActive(true);
        PausePanel.SetActive(false);
        RunBtnPanel.SetActive(false);

        NPC[1].gameObject.GetComponent<RunNPC>().enabled = true;
        NPC[2].gameObject.GetComponent<RunNPC>().enabled = true;
        NPC[3].gameObject.GetComponent<RunNPC>().enabled = true;

        player.velocity = new Vector3(0, 0, 0);

        FinishSound = false;
        //Count.ResetTimer();
    }

    public void GamePause()
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

    public void EndMove()
    {
        if (difficulty == 3)
        {
            Goll = 5000;
            EndLine.position = new Vector3(50, -409, 4998);
            EndBlock.position = new Vector3(50, -409,5144);
        }
        else
        {
            Goll = 4300;
            EndLine.position = new Vector3(50, 0, 4298);
            EndBlock.position = new Vector3(50, 0, 4440);
        }
    }

    void GameResult()   //점수에 따른 보상 획득 메소드
    {
        //코인
        //쉬움 난이도: 승리 - 10 패배 - 5
        //보통 난이도: 승리 - 20 패배 - 7
        //어려움 난이도: 승리 - 30 패배 - 9

        float get_exp = 10f;
        int get_coin = 0;
        string result_txt = "";

        if(difficulty == 1) //쉬움 난이도
        {
            if (is_win) //승리시
            {
                get_coin = 10;
                result_txt = "이겼다!";
            }
            else
            {
                get_coin = 5;
                result_txt = "졌다….";
            }
        }
        else if (difficulty == 2)   //보통 난이도
        {
            if (is_win) //승리시
            {
                get_coin = 20;
                result_txt = "이겼다!";
            }
            else
            {
                get_coin = 7;
                result_txt = "졌다….";
            }
        }
        else if(difficulty == 3)    //어려움 난이도
        {
            if (is_win) //승리시
            {
                get_coin = 30;
                result_txt = "이겼다!";
            }
            else
            {
                get_coin = 9;
                result_txt = "졌다….";
            }
        }

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //보상 결과를 화면에 띄움
        ResultTxt.text = result_txt;
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }
}
