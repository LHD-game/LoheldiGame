using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public static object cInstance { get; internal set; }

    GameObject[] foods;
    GameObject[] lifes;
    int score = 0;
    int highScore = 10;
    int life = 3;

    Image lifeImg;
    Color lifeColor;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text highScoreTxt;
    [SerializeField]
    private GameObject WelcomePanel;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private GameObject PausePanel;
    public GameObject HPDisablePanel;   //hp �������� ���� �˾� �г� ������Ʈ

    //��� ���
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    private float[] foodDropSec = new float[] {1.8f, 1.6f, 1.4f, 1.0f, 0.8f, 0.5f, 0.4f, 0.3f, 0.3f  }; //�߰��ܰ� �� ���� �������� ����
    private float newFoodDropSec;

    private bool stopTrigger = false;   //true�� ���� ���� ����
    private bool pauseTrigger = false;  //true�� ��� �Ͻ�����

    public GameObject SoundManager;

    private void Start()
    {
        if(PlayerPrefs.HasKey("FoodHighScore"))
        {
            highScore =  PlayerPrefs.GetInt("FoodHighScore");
        }
        highScoreTxt.text = "�ְ�����: " + highScore;
        Welcome();
    }

    IEnumerator CreateFoodRoutine()
    {
        
        while (stopTrigger)
        {
            CreateFood();
            yield return new WaitForSeconds(newFoodDropSec);
        }
        Debug.Log("stoptrigger" + stopTrigger);

    }

    public void Welcome()
    {
        GameOverPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        WelcomePanel.SetActive(true);

        pauseTrigger = false;

        //Debug.Log("food.Length: " + foods.Length);
        GameObject playerPos = GameObject.FindWithTag("Player");
        playerPos.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        playerPos.transform.rotation = new Quaternion(0, 0, 0, 0);
        CreateLife();
        InitScore();
        newFoodDropSec = foodDropSec[0];
        foods = Resources.LoadAll<GameObject>("Prefabs/Foods/");

    }
    public void GameStart()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        if (now_hp > 0)  //���� hp�� 0���� ũ�ٸ�
        {
            //hp 1 ����
            PlayInfoManager.GetHP(-1);
            stopTrigger = true;

            //foods = GameObject.FindGameObjectsWithTag("Food");  //Ȱ��ȭ�� ������Ʈ��!
            StopCoroutine(CreateFoodRoutine());
            StartCoroutine(CreateFoodRoutine());
            WelcomePanel.SetActive(false);
            Timer.instance.StartTimer();
        }
        else    //0 ���϶��: ���� �÷��� �Ұ�
        {
            // hp�� �����մϴ�! �˾� ����
            HPDisablePanel.SetActive(true);
        }

    }


    public void GameOver()
    {
        Time.timeScale = 1f;
        stopTrigger = false;
        StopCoroutine(CreateFoodRoutine());
        GameObject[] disGfoods = GameObject.FindGameObjectsWithTag("GoodFood");
        for (int i = 0; i < disGfoods.Length; i++)
        {
            Destroy(disGfoods[i]);
        }        
        GameObject[] disBfoods = GameObject.FindGameObjectsWithTag("BadFood");
        for (int i = 0; i < disBfoods.Length; i++)
        {
            Destroy(disBfoods[i]);
        }
        GameOverPanel.SetActive(true);
        GameResult();

        //�ְ� ������ prefs�� ����
        PlayerPrefs.SetInt("FoodHighScore", highScore);
    }

    void GameResult()   //������ ���� ���� ȹ�� �޼ҵ�
    {
        //0�� �̻� 10�� �̸�: ����ġ 10, ���� 5
        //10�� �̻� 20�� �̸�: ����ġ 10, ���� 10
        //20�� �̻� 30�� �̸�: ����ġ 10, ���� 15
        //30�� �̻� 40�� �̸�: ����ġ 10, ���� 20
        //40�� �̻�: ����ġ 10, ���� (����/2)

        float get_exp = 10f;
        int get_coin = 0;
        
        if(score >= 0 && score < 10)
        {
            get_coin = 5;
        }
        else if (score >= 10 && score < 20)
        {
            get_coin = 10;
        }
        else if (score >= 20 && score < 30)
        {
            get_coin = 15;
        }
        else if (score >= 30 && score < 40)
        {
            get_coin = 20;
        }
        else if (score >= 40)
        {
            get_coin = score / 2;
        }

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //���� ����� ȭ�鿡 ���
        ResultTxt.text = score + " ��";
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    public void PauseGame()
    {
        if (!pauseTrigger)  //���� �Ͻ�����
        {
            pauseTrigger = true;
            Timer.instance.PauseTimer();
            PausePanel.SetActive(true);
        }
        else    //���� �簳
        {
            pauseTrigger = false;   //Ʈ���� ����
            Timer.instance.PauseTimer();
            PausePanel.SetActive(false);    //�Ͻ����� �г� ��Ȱ��ȭ
        }
        

    }

    public bool Blocker()
    {
        return stopTrigger;
    }
    public int ReturnLife()
    {
        return life;
    }


    private void CreateFood()
    {
        float randf;
        if (!pauseTrigger)
        {
            while (true)
            {

                randf = Random.Range(-40.0f, 40.0f);
                //food�� ��Ÿ�� ��ǥ��
                Vector3 randpos = Camera.main.WorldToViewportPoint(new Vector3(randf, 0.0f, 0.0f));

                if (randpos.x < 0.05f || randpos.x > 0.95f)
                {
                    continue;
                }
                else
                {
                    randpos = Camera.main.ViewportToWorldPoint(randpos);
                    randpos.y = 15.0f;
                    randpos.z = 0.0f;
                    int randFood = Random.Range(0, foods.Length);
                    GameObject Tempfood =  Instantiate(foods[randFood], randpos, Quaternion.Euler(0, 0, 0));
                    //��Ÿ�� ������Ʈ, ��ǥ��, ȸ������

                    Tempfood.transform.GetComponent<FoodsManager>().SoundManager = this.gameObject;

                    break;
                }
            }
        }
        
        //Vector3 randpos = Camera.main.ViewportToWorldPoint(new Vector3(randf, 0.0f, 0.0f));
        
    }

    private void CreateLife()
    {
        life = 3;
        lifes = GameObject.FindGameObjectsWithTag("Life");

        for (int i=0; i < lifes.Length; i++)
        {
            lifeImg = lifes[i].GetComponent<Image>();
            lifeColor = lifeImg.color;
            lifeColor.a = 1.0f;
            lifeImg.color = lifeColor;
            //lifes[i].SetActive(true);
        }

    }

    private void InitScore()
    {
        score = 0;
        scoreTxt.text = "���� ����: " + score;
    }

    
    public void ScoreCnt()
    {
        score++;
        if(score > highScore)
        {
            highScore = score;
        }
        scoreTxt.text = "���� ����: " + score;
        highScoreTxt.text = "�ְ�����: " + highScore;
        //Debug.Log("score: "+score);
    }
    public void LifeCnt()
    {
        life--;
        lifeImg = lifes[life].GetComponent<Image>();
        lifeColor = lifeImg.color;
        lifeColor.a = 0.0f;
        lifeImg.color = lifeColor;

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void FoodDropsec(int gLevel)
    {
        newFoodDropSec = foodDropSec[gLevel];
        Debug.Log("FoodDropSec: " + foodDropSec[gLevel]);
    }

}
